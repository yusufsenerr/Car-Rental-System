using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Models.Class;
using AracKiralamaOtomasyonu.Models.Context;
using static AracKiralamaOtomasyonu.Filtre.GirisFiltre;

namespace AracKiralamaOtomasyonu.Controllers
{

    //-DEVELOPER BY YUSUF ŞENER
    //-https://yusufsener.com.tr/
    //  -https://github.com/yusufsenerr
    [LoginFilter]
    public class KullaniciController : Controller
    {

        private AracKiralamaContext db = new AracKiralamaContext();
        public ActionResult Panel()
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var ilan = db.Ilanlar;
                var Userid = Convert.ToInt16(Session["KullaniciId"]);

                PanelViewModel model = new PanelViewModel();
                model.Ilanlars = ilan.Include("AracMarka").Include("AracModel").Where(x => x.IDMusteri == Userid).OrderByDescending(x => x.IlanTarihi).ToList();
                model.Rezervasyon = db.Rezervasyon.Include("Ilanlar").Where(x => x.IDSatici == Userid).OrderBy(x => x.BitisTarihi).ToList();

                return View(model);
            }
        }
        public JsonResult EpostaKontrol(string eposta, int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var kullanici = db.Kullanici.Where(x => x.Eposta == eposta && x.IDKullanici == id).FirstOrDefault();
                var kullaniciKontrol = db.Kullanici.Where(x => x.Eposta == eposta).FirstOrDefault();
                var personel = db.Personel.Where(x => x.Mail == eposta).FirstOrDefault();
                if (kullanici != null)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else if (kullaniciKontrol == null)
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }


            }
        }
        public ActionResult Profil()
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var Userid = Convert.ToInt16(Session["KullaniciId"]);
                //Session'dan gelen id numarası ile kullanıcı tablosunda aratma yapıyorum
                var profil = db.Kullanici.Where(x => x.IDKullanici == Userid).FirstOrDefault();

                return View(profil);
            }

        }

        public ActionResult ProfilGuncelle(Kullanici kullanici)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                //Profilden gelen bilgileri güncellemek için kullanıcı tablosuna id dumarasıyla sorgu atıyorum
                var kullanicis = db.Kullanici.Where(x => x.IDKullanici == kullanici.IDKullanici).FirstOrDefault();

                //Kullanıcı bilgilerini güncelliyorum
                kullanicis.Ad = kullanici.Ad;
                kullanicis.Soyad = kullanici.Soyad;
                kullanicis.Telefon = kullanici.Telefon;
                kullanicis.Eposta = kullanici.Eposta;
                kullanicis.Adres = kullanici.Adres;
                kullanicis.DogumTarihi = kullanici.DogumTarihi;
                db.SaveChanges();
                return RedirectToAction("Profil");

            }
        }

        [AcceptVerbs("Post")]
        public ActionResult ProfilFotoYukle(HttpPostedFileBase UploadFiles, int ID)
        {
            //session'dan gelen kullanıcı id numarasını ve post ettiğim görseli değişkene atıyorum
            var httpPostedFile = UploadFiles;
            var KullaniciID = ID;

            //eğer görsel boş değilse koşulun içine giriyor
            if (UploadFiles != null)
            {
                //görseli kaydetmek için bir guid oluşturuyorum ve görselin adını onla değiştiriyorum
                string FileName = UploadFiles.FileName;
                string ext = System.IO.Path.GetExtension(FileName).ToLower();
                string guid = Guid.NewGuid().ToString();
                guid = guid.Replace("-", string.Empty);
                guid = guid.Substring(0, 16) + ext;

                //görseli kayıt edileceği klaskör adresini belirtiyorum
                string filepath = System.Web.HttpContext.Current.Server.MapPath("~") + "Dosyalar/Profil/" + guid;

                //görseli belirttiğim dosyaya kayıt ediyorum
                httpPostedFile.SaveAs(filepath);

                //Yüklediğim resmi yeniden boyutlandırmak için farkı bir isimle kayıt edebilmek için guid oluşturuyorum
                string FileName2 = httpPostedFile.FileName;
                string ext2 = System.IO.Path.GetExtension(FileName2).ToLower();
                string guid2 = Guid.NewGuid().ToString();
                guid2 = guid.Replace("-", string.Empty);
                guid2 = guid.Substring(0, 16) + ext2;
                //görselin kayıt edileceği adresi belirtiyorum
                string filepath2 = System.Web.HttpContext.Current.Server.MapPath("~") + "Dosyalar/Profil/" + "K" + guid2;

                //burada  fotoğrafı kırpan classı kullanarak 500x500 kare bir görüntü haline getiriyorum
                Image imgPhoto = Image.FromFile(filepath);
                Bitmap image = ImageResize.ResizeImage(imgPhoto, 500, 500);
                image.Save(filepath2);

                if (System.IO.File.Exists(filepath2))
                {
                    using (AracKiralamaContext db = new AracKiralamaContext())
                    {
                        //Kullanıcının profil fotoğrafı olup olmadığını kontrol ediyorum
                        var fotosil = db.Dosyalar.Where(s => s.tip == "kare").FirstOrDefault();
                        //profil fotoğrafı varsa onu siliyorum
                        if (fotosil != null)
                        {
                            db.Dosyalar.Remove(fotosil);
                        }
                        //dosyalara tablosuna profil fotoğrafı için bir kayıt ekliyorum
                        Dosyalar dosyalar = new Dosyalar();
                        dosyalar.IDKullanici = Convert.ToInt16(KullaniciID);
                        dosyalar.tip = "ProfilFotoKare";
                        dosyalar.Url = "K" + guid2;
                        db.Dosyalar.Add(dosyalar);
                        db.SaveChanges();
                        var kullaniciID = Convert.ToInt16(KullaniciID);
                        var kullanici = db.Kullanici.Where(z => z.IDKullanici == kullaniciID).FirstOrDefault();
                        kullanici.ProfilFotoUrl = dosyalar.Url;
                        db.SaveChanges();
                    }
                }

                image.Dispose();
                imgPhoto.Dispose();
                //Fotoğrafı kırptığım için, kırpılmamış olan fotoğrafı belirlediğim klasörden siliyorum
                System.IO.File.Delete(filepath);


            }
            return RedirectToAction("Profil");
        }
        public ActionResult HesapSil(int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var kullanici = db.Kullanici.Where(x => x.IDKullanici == id).FirstOrDefault();
                var ilanlar = db.Ilanlar.Where(x => x.IDMusteri == id).ToList();
                var dosyalar = db.Dosyalar.Where(x => x.IDKullanici == id).ToList();
                db.Kullanici.Remove(kullanici);
                db.Ilanlar.RemoveRange(ilanlar);
                db.Dosyalar.RemoveRange(dosyalar);
                db.SaveChanges();

                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session["KullaniciId"] = null;
            Session["YetkiSeviye"] = null;
            Session.Abandon();
            return RedirectToAction("Giris", "Home");
        }


    }
}
