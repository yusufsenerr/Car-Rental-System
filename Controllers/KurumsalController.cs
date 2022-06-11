using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Models.Class;
using AracKiralamaOtomasyonu.Models.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AracKiralamaOtomasyonu.Filtre.GirisFiltre;

namespace AracKiralamaOtomasyonu.Controllers
{

    public class KurumsalController : Controller
    {
        // GET: Kuurmsal
        public ActionResult Liste(string ara)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                
                KurumsalListe model = new KurumsalListe();
                var kurum = db.Kurumsal.OrderBy(s => s.Ad).ToList();
                
                if (ara != "" && ara != null)
                {
                    ara = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ara);
                    kurum = kurum.Where(x => x.Ad.Contains(ara)).ToList();
                    
                }
                model.Kurumsal = kurum;
                model.arama = ara;
                return View(model);
            }
        }
        public ActionResult Detay(int ID, string ara, string marka, string modeli, string vites, string kasa, string yakit, string cekis, int? minfiyat, int? maxfiyat)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {

                IlanListeViewModel model = new IlanListeViewModel();

                var sorgu = db.Ilanlar.Include("AracMarka").Include("AracModel").Include("AracGuvenlik").Include("AracDisDonanim").Include("AracIcDonanim").Include("AracMultiMedya").Include("Kullanici").Include("Dosyalar").Where(x=>x.IDKurumsal == ID).ToList();

                if (ara != "" && ara != null)
                {
                    ara = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ara);
                    sorgu = sorgu.Where(x => x.Baslik.Contains(ara) || x.AracMarka.Ad.Contains(ara) || x.AracModel.ModelAd.Contains(ara)).ToList();
                }
                if (marka != null)
                {
                    sorgu = sorgu.Where(x => x.AracMarka.Ad == marka).ToList();
                }
                if (modeli != null)
                {
                    sorgu = sorgu.Where(x => x.AracModel.ModelAd == modeli).ToList();
                }
                if (vites != null)
                {
                    sorgu = sorgu.Where(x => x.Vites == vites).ToList();
                }
                if (kasa != null)
                {
                    sorgu = sorgu.Where(x => x.Kasa == kasa).ToList();
                }
                if (yakit != null)
                {
                    sorgu = sorgu.Where(x => x.Yakit == yakit).ToList();
                }
                if (cekis != null)
                {
                    sorgu = sorgu.Where(x => x.Cekis == cekis).ToList();
                }
                if (minfiyat != null && maxfiyat != null && minfiyat != 0 && maxfiyat != 0)
                {
                    sorgu = sorgu.Where(x => x.Fiyat <= maxfiyat && x.Fiyat >= minfiyat).ToList();
                }

                model.Ilanlars = sorgu.ToList();
                model.Dosyalar = db.Dosyalar.Where(x => x.tip == "ilan" && x.IlkFoto == true).FirstOrDefault();
                model.AracMarka = db.AracMarka.ToList();
                model.marka = marka;
                model.modeli = modeli;
                model.kasa = kasa;
                model.vites = vites;
                model.yakit = yakit;
                model.cekis = cekis;
                model.maxfiyati = maxfiyat;
                model.minfiyati = minfiyat;
                model.arama = ara;

                return View(model);
            }
        }
        
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();
        }

        [AcceptVerbs("Post")]
        public ActionResult EkleMethod(Kurumsal kurumsal)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var httpPostedFile = System.Web.HttpContext.Current.Request.Files["FileUpload"];
                if (httpPostedFile != null)
                {
                    try
                    {

                        //Dosyayı kayıt ederken karışılık olmaması için bir guid oluşturuyorum
                        string FileName = httpPostedFile.FileName;
                        string ext = System.IO.Path.GetExtension(FileName).ToLower();
                        string guid = Guid.NewGuid().ToString();
                        guid = guid.Replace("-", string.Empty);
                        guid = guid.Substring(0, 16) + ext;

                        //Resimleri verdiğim uzantıya kayıt ediyorum
                        string filepath = System.Web.HttpContext.Current.Server.MapPath("~") + "/Dosyalar/Public/" + guid;

                        httpPostedFile.SaveAs(filepath);

                        //Dosyaya yüklediğim resmi yeniden boyutlandırmak için farkı bir isimle kayıt etmek için yeni bir guid oluşturuyorum
                        string FileName2 = httpPostedFile.FileName;
                        string ext2 = System.IO.Path.GetExtension(FileName2).ToLower();
                        string guid2 = Guid.NewGuid().ToString();
                        guid2 = guid.Replace("-", string.Empty);
                        guid2 = guid.Substring(0, 16) + ext2;
                        string filepath2 = System.Web.HttpContext.Current.Server.MapPath("~") + "/Dosyalar/Public/"+"b" + guid2;

                        //Burada yüklenen fotoğrafın boyutunu 555x500 şeklinde kırpıyorum ve dosyayı kayıt ediyorum
                        Image imgPhoto = Image.FromFile(filepath);
                        Bitmap image = ImageResize.ResizeImage(imgPhoto, 500, 500);
                        image.Save(filepath2);

                        kurumsal.LogoUrl = "b"+ guid2;
                        db.Kurumsal.Add(kurumsal);
                        
                        db.SaveChanges();
                        image.Dispose();
                        imgPhoto.Dispose();

                        System.IO.File.Delete(filepath);
                        return RedirectToAction("Liste");

                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Ekle");
                    }

                }
                return RedirectToAction("Ekle");
            }
        }

        public ActionResult FirmaListe()
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var firma = db.Kurumsal.ToList();
                return View(firma);
            }
            
        }

        [LoginFilter]
        public ActionResult FirmaSil(int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var firma = db.Kurumsal.Where(x => x.ID == id).FirstOrDefault();
                var ilanlar = db.Ilanlar.Where(x => x.IDMusteri == id).ToList();
                var dosyalar = db.Dosyalar.Where(x => x.IDKullanici == id).ToList();
                db.Kurumsal.Remove(firma);
                db.Ilanlar.RemoveRange(ilanlar);
                db.Dosyalar.RemoveRange(dosyalar);
                db.SaveChanges();

                return RedirectToAction("FirmaListe");
            }
        }
        [LoginFilter]
        public ActionResult Guncelle (int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                //Session'dan gelen id numarası ile kullanıcı tablosunda aratma yapıyorum
                var firma = db.Kurumsal.Where(x => x.ID == id).FirstOrDefault();
                return View(firma);
            }
        }

        public ActionResult GuncelleMethod(Kurumsal kurumsal)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var firma = db.Kurumsal.Where(x => x.ID == kurumsal.ID).FirstOrDefault();
                firma.Ad = kurumsal.Ad;
                firma.Adres = kurumsal.Adres;
                firma.Telefon = kurumsal.Telefon;
                db.SaveChanges();
                return RedirectToAction("Liste");

            }
        }
    }
}