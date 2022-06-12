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

using AracKiralamaOtomasyonu.PasswordHash;
using static AracKiralamaOtomasyonu.Filtre.GirisFiltre;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace AracKiralamaOtomasyonu.Controllers
{

    public class KurumsalController : Controller
    {
        // GET: Kuurmsal

        public ActionResult Giris(int? eror)
        {
            if (eror != null)
            {
                ViewBag.eror = eror;
            }
            else
            {
                ViewBag.eror = 3;
            }

            return View();
        }


        //Giriş yap butonuna basılınca bu metoda düşer burada parametre olarak eposta ve sifre gönderilir
        public ActionResult GirisMethod(string eposta, string sifre)


        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                //Sistemde girilen E-posta kullanıcıya ait mi diye kontrol edilir
                var kurumsal = db.Kurumsal.FirstOrDefault(x => x.Eposta == eposta);
                //Eğer kullanıcıya ait değilse bu kodun içine girer
                if (kurumsal == null)
                {
                        int eror = 1;
                        return RedirectToAction("Giris", new { eror });
                }
                //Kullanıcının şifresi yanlış ise hatalı şifre şeklinde mesaj gönderilir
                else if (!HashingHelper.VerifyPasswordHash(sifre, kurumsal.SifreHash, kurumsal.SifreSalt))
                {
                    int eror = 1;
                    return RedirectToAction("Giris", new { eror });
                }
                else
                {
                    //E-mail ve şifre doğru ise id,ad ve e posta session'a kaydedilri.
                    Session.Add("KurumsalId", kurumsal.ID);
                    Session.Add("KullaniciAd", kurumsal.Ad);

                    Session.Add("Email", kurumsal.Eposta);
                    //profil fotoğrafı sistemde kayıtlı ise kullanıcıc profil fotoğrafı session'a eklenir
                    if (kurumsal.LogoUrl != null)
                    {
                        Session.Add("ProfilFoto", kurumsal.LogoUrl);
                    }
                    else
                    {
                        //profil fotoğrafı sistemde kayıtlı değil ise kullanıcının profil fotoğrafı default olarak verilir ve session'a eklenir
                        Session.Add("ProfilFoto", "default-profile.jpg");
                    }

                    //İşemler sonucunda Kullanici paneline yönlendirilir
                    return RedirectToAction("Panel");
                }

            }
        }

        public ActionResult Panel()
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var ilan = db.Ilanlar;
                var Userid = Convert.ToInt16(Session["KullaniciId"]);

                PanelViewModel model = new PanelViewModel();
                model.Ilanlars = ilan.Include("AracMarka").Include("AracModel").Where(x => x.IDKurumsal == Userid).OrderByDescending(x => x.IlanTarihi).ToList();

                return View(model);
            }
        }

        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        public ActionResult SifremiUnuttumMethod(string Eposta)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                //Girilen E-posta sistemde kayıtlı mı diye kontrol ediliyor
                var kullanici = db.Kurumsal.Where(x => x.Eposta == Eposta).FirstOrDefault();

                string guid = Guid.NewGuid().ToString();
                guid = guid.Replace("-", string.Empty);
                guid = guid.Substring(0, 30);
                if (kullanici != null)
                {
                    //Şifre sıfırlayabilmesi için sifreSifirlama tablosuna kayıt ekliyorum ki buradan URL'e ulaşabilsin
                    SifreSifirlama sifreSifirlama = new SifreSifirlama();
                    sifreSifirlama.KayitTarihi = DateTime.Now;
                    sifreSifirlama.Eposta = Eposta;
                    sifreSifirlama.KullaniciID = kullanici.ID;
                    sifreSifirlama.Dogrulama = true;
                    sifreSifirlama.DogrulamaLinki = guid;
                    db.SifreSifirlama.Add(sifreSifirlama);
                    db.SaveChanges();
                }

                //Eğer girilen E-posta bir Personele ait ise bu if koşulunun içine girer
                //Şifre sıfırlama için gönderdiğim E-posta içeriğini mail'in bodysine ekliyorum
                string filename = Server.MapPath("~/Views/Mail/SifreSifirlama.cshtml");
                string mailbody = System.IO.File.ReadAllText(filename);

                //Mail'de yer alan butona bağlantı ekleyebilmek için guid'yi maile içeriğine ekliyorum
                mailbody = mailbody.Replace("##guid##", guid);
                SmtpClient client = new SmtpClient();

                //Alttaki kodlar ise mail'in gönderilmesi için gerekli olan kodlar
                client.Port = 587;
                client.Host = "smtp-mail.outlook.com";
                client.EnableSsl = true;

                client.Credentials = new NetworkCredential("yusufsenerproje@outlook.com", "DfnDuw7p159*C");

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("yusufsenerproje@outlook.com", "Rent A Car");

                mail.To.Add(Eposta);
                mail.Subject = "Şifre Sıfırlama";
                mail.IsBodyHtml = true;
                mail.Body = mailbody;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.Send(mail);
                TempData["mailyok"] = "mailvar";
                TempData["mail"] = Eposta;
                //Eğer bu girilen mail sistemde kayıtlı değil ise SifremiUnuttum ekranına yönlendiriyorum

                return RedirectToAction("SifremiUnuttum");


            }

        }

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
        [LoginFilter]
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

        public String EpostaKontrol(string eposta)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var kullanici = db.Kurumsal.Where(x => x.Eposta == eposta).FirstOrDefault();
                if (kullanici != null)
                {
                    var data = "basarili";
                    return data;
                }
                else
                {
                    var data = "basarisiz";
                    return data;
                }

            }
        }
    }
}