    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
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
    public class IlanController : Controller
    {
        private AracKiralamaContext db = new AracKiralamaContext();
        public ActionResult Ilan(int id)
        {
            var Userid = Convert.ToInt16(Session["KullaniciId"]);
            var kontrol = db.Dosyalar.Where(x => x.IDIlan == id && x.tip == "ilan").FirstOrDefault();
                if (kontrol != null)
                {
                    IlanViewModel model = new IlanViewModel();
                var KullaniciIlan = db.Ilanlar.Include("AracMarka").Include("Dosyalar").Include("AracModel").Include("AracGuvenlik").Include("Dosyalar").Include("AracDisDonanim").Include("AracIcDonanim").Include("AracMultiMedya").Include("Kullanici").Where(x => x.IDIlan == id  && x.IDMusteri == Userid).FirstOrDefault();
                var ilan = db.Ilanlar.Include("AracMarka").Include("Dosyalar").Include("AracModel").Include("AracGuvenlik").Include("Dosyalar").Include("AracDisDonanim").Include("AracIcDonanim").Include("AracMultiMedya").Include("Kullanici").Where(x => x.IDIlan == id && x.Durum == "3").FirstOrDefault();

                if (KullaniciIlan != null)
                {
                    model.Ilanlars = KullaniciIlan;
                    model.Dosyalars = db.Dosyalar.Where(x => x.IDIlan == id && x.tip == "ilan" && x.IDDosya != kontrol.IDDosya).ToList();
                    model.Dosyalar = db.Dosyalar.Where(x => x.IDIlan == id && x.tip == "ilan" && x.IlkFoto == true).FirstOrDefault();
                    return View(model);
                }

                if (ilan == null)
                    {
                        return RedirectToAction("IlanListesi", "Ilan");
                    }
                    model.Ilanlars = ilan;
                    model.Dosyalars = db.Dosyalar.Where(x => x.IDIlan == id && x.tip == "ilan" && x.IDDosya != kontrol.IDDosya).ToList();
                    model.Dosyalar = db.Dosyalar.Where(x => x.IDIlan == id && x.tip == "ilan" && x.IlkFoto == true).FirstOrDefault();
                    return View(model);
                }
                return RedirectToAction("IlanListesi","Ilan");
        }
        [LoginFilter]
        public ActionResult Ilanlarim()
        {
            var Userid = Convert.ToInt16(Session["KullaniciId"]);
            var ilan = db.Ilanlar.Where(x => x.IDMusteri == Userid).FirstOrDefault();
            var kontrol = db.Dosyalar.Where(x => x.IDKullanici == Userid&& x.tip == "ilan").ToList();

            if (kontrol != null)
            {
                IlanListeViewModel model = new IlanListeViewModel();
                model.Ilanlars = db.Ilanlar.Include("AracMarka").Include("AracModel").Include("AracGuvenlik").Include("AracDisDonanim").Include("AracIcDonanim").Include("AracMultiMedya").Include("Kullanici").Include("Dosyalar").OrderByDescending(x => x.IlanTarihi).ToList();
                model.Dosyalar = db.Dosyalar.Where(x => x.tip == "ilan" && x.IlkFoto == true).FirstOrDefault();
                

                return View(model);
            }
            return RedirectToAction("IlanListesi", "Ilan");
        }
        [LoginFilter]
        public ActionResult IlanEkle()
        {
            IlanEkleViewModel model = new IlanEkleViewModel();
            model.AracMarka = db.AracMarka.OrderBy(x=>x.Ad).ToList();
            model.AracModel = db.AracModel.OrderBy(x => x.ModelAd).ToList();
            return View(model);
        }
        [LoginFilter]
        [AcceptVerbs("Post")]
        public ActionResult IlanEkleMethod(Ilanlar ilanlars, AracGuvenlik aracGuvenliks, AracDisDonanim aracDisDonanims, AracIcDonanim aracIcDonanims, AracMultiMedya aracMultiMedyas, IEnumerable<HttpPostedFileBase> UploadFiles)
        {
            var UserID = Convert.ToInt16(Session["KullaniciId"]);
            var KurumsalID = Convert.ToInt16(Session["KurumsalId"]);

            //Formdan gelen arac güvenlik bilgilerini tabloya ekliyorum
            AracGuvenlik aracGuvenlik = new AracGuvenlik();
            aracGuvenlik.ABS = aracGuvenliks.ABS;
            aracGuvenlik.CocukKilidi = aracGuvenliks.CocukKilidi;
            aracGuvenlik.HavaYastigi = aracGuvenliks.HavaYastigi;
            aracGuvenlik.YokusDestegi = aracGuvenliks.YokusDestegi;
            aracGuvenlik.LastikAriza = aracGuvenliks.LastikAriza;
            aracGuvenlik.MerkeziKilit = aracGuvenliks.MerkeziKilit;

            db.AracGuvenlik.Add(aracGuvenlik);

            //Formdan gelen Arac Dış Donanim bilgilerini tabloya ekliyorum
            AracDisDonanim aracDisDonanim = new AracDisDonanim();

            aracDisDonanim.ElektrikliAyna = aracDisDonanims.ElektrikliAyna;
            aracDisDonanim.FarLed = aracDisDonanims.FarLed;
            aracDisDonanim.FarSis = aracDisDonanims.FarSis;
            aracDisDonanim.OtomatikKatlananAyna = aracDisDonanims.OtomatikKatlananAyna;
            aracDisDonanim.ParkAsistani = aracDisDonanims.ParkAsistani;
            aracDisDonanim.ParkSensoru = aracDisDonanims.ParkSensoru;
            aracDisDonanim.RomorkDemiri = aracDisDonanims.RomorkDemiri;
            aracDisDonanim.Sunroof = aracDisDonanim.Sunroof;

            db.AracDisDonanim.Add(aracDisDonanim);

            //Formdan gelen Arac iç Donanim bilgilerini tabloya ekliyorum
            AracIcDonanim aracIcDonanim = new AracIcDonanim();

            aracIcDonanim.DeriKoltuk = aracIcDonanims.DeriKoltuk;
            aracIcDonanim.GeriGorusKamera = aracIcDonanims.GeriGorusKamera;
            aracIcDonanim.HidrolikDireksiyon = aracIcDonanims.HidrolikDireksiyon;
            aracIcDonanim.HizSabitleyici = aracIcDonanims.HizSabitleyici;

            aracIcDonanim.Klima = aracIcDonanims.Klima;
            aracIcDonanim.KumasKoltuk = aracIcDonanims.KumasKoltuk;
            aracIcDonanims.SogutmaliKoltuk = aracIcDonanims.SogutmaliKoltuk;
            aracIcDonanims.YolBilgisayarı = aracIcDonanims.YolBilgisayarı;
            db.AracIcDonanim.Add(aracIcDonanim);



            //Formdan gelen Arac Multimedya bilgilerini tabloya ekliyorum
            AracMultiMedya aracMultiMedya = new AracMultiMedya();

            aracMultiMedya.AUX = aracMultiMedyas.AUX;
            aracMultiMedya.Bluetooth = aracMultiMedyas.Bluetooth;
            aracMultiMedya.Navigasyon = aracMultiMedyas.Navigasyon;
            aracMultiMedya.Radyo = aracMultiMedyas.Radyo;
            aracMultiMedya.TV = aracMultiMedyas.TV;
            aracMultiMedyas.USB = aracMultiMedyas.USB;
            db.AracMultiMedya.Add(aracMultiMedya);

            db.SaveChanges();

            //Formdan gelen Arac hakkındaki bilgilerini tabloya ekliyorum
            Ilanlar ilanlar = new Ilanlar();
            //Durumu 2 yapıyorum çünkü ilan yayınlanmadan önce onaya gitmeli
            ilanlar.Durum = "2";
            ilanlar.IDAracDisDonanim = aracDisDonanim.IDAracDisDonanim;
            ilanlar.IDAracGuvenlik = aracGuvenlik.IDAracGuvenlik;
            ilanlar.IDAracIcDonanim = aracIcDonanim.IDAracIcDonanim;
            ilanlar.IDAracMultiMedya = aracMultiMedya.IDAracMultiMedya;
            ilanlar.Aciklama = ilanlars.Aciklama;
            ilanlar.Baslik = ilanlars.Baslik;
            ilanlar.IDAracMarka = ilanlars.IDAracMarka;
            ilanlar.IDAracModel = ilanlars.IDAracModel;
            ilanlar.Fiyat = ilanlars.Fiyat;
            ilanlar.Yil = ilanlars.Yil;
            ilanlar.Kilometre = ilanlars.Kilometre;
            ilanlar.Yakit = ilanlars.Yakit;
            ilanlar.Vites = ilanlars.Vites;
            ilanlar.Kasa = ilanlars.Kasa;
            ilanlar.MotorGucu = ilanlars.MotorGucu;
            ilanlar.MotorHacmi = ilanlars.MotorHacmi;
            ilanlar.Cekis = ilanlars.Cekis;
            if (UserID != null)
            {
                ilanlar.IDMusteri = UserID;
            }
            else
            {
                ilanlar.IDKurumsal = KurumsalID;
            }

            ilanlar.IDDosya = 1;

            db.Ilanlar.Add(ilanlar);
            db.SaveChanges();
            aracGuvenlik.IDIlan = ilanlar.IDIlan;
            aracIcDonanim.IDIlan = ilanlar.IDIlan;
            aracDisDonanim.IDIlan = ilanlar.IDIlan;
            aracMultiMedya.IDIlan = ilanlar.IDIlan;
            db.SaveChanges();
            //Burada yüklenilen fotoğrafları dosyalar tablosuna kayıt ediyorum

            try
            {
                foreach (var item in UploadFiles)
                {
                    //Dosyayı kayıt ederken karışılık olmaması için bir guid oluşturuyorum
                    string FileName = item.FileName;
                    string ext = System.IO.Path.GetExtension(FileName).ToLower();
                    string guid = Guid.NewGuid().ToString();
                    guid = guid.Replace("-", string.Empty);
                    guid = guid.Substring(0, 16) + ext;

                    //Resimleri verdiğim uzantıya kayıt ediyorum
                    string filepath = System.Web.HttpContext.Current.Server.MapPath("~") + "Dosyalar/Public/" + guid;

                    item.SaveAs(filepath);

                    //Dosyaya yüklediğim resmi yeniden boyutlandırmak için farkı bir isimle kayıt etmek için yeni bir guid oluşturuyorum
                    string FileName2 = item.FileName;
                    string ext2 = System.IO.Path.GetExtension(FileName2).ToLower();
                    string guid2 = Guid.NewGuid().ToString();
                    guid2 = guid.Replace("-", string.Empty);
                    guid2 = guid.Substring(0, 16) + ext2;
                    string filepath2 = System.Web.HttpContext.Current.Server.MapPath("~") + "Dosyalar/Public/a" + guid2;

                    //Burada yüklenen fotoğrafın boyutunu 555x500 şeklinde kırpıyorum ve dosyayı kayıt ediyorum
                    Image imgPhoto = Image.FromFile(filepath);
                    Bitmap image = ImageResize.ResizeImage(imgPhoto, 555, 500);
                    image.Save(filepath2);

                    //Burada ise yüklenen fotoğrafı dosyalar tablosuna ekliyorum
                    if (System.IO.File.Exists(filepath2))
                    {
                        Dosyalar dosyalars = new Dosyalar();

                        dosyalars.IDIlan = ilanlar.IDIlan;
                        dosyalars.IDKullanici = UserID;
                        dosyalars.tip = "ilan";
                        dosyalars.Url = "a" + guid2;

                        //Ilanın ilk fotoğrafını belirmek için buradaki koşulu kullanıyorum.
                        var ilankontrol = db.Dosyalar.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();
                        if (ilankontrol == null)
                        {
                            dosyalars.IlkFoto = true;
                        }
                        else
                        {
                            dosyalars.IlkFoto = false;
                        }
                        db.Dosyalar.Add(dosyalars);
                        ilanlar.IDDosya = dosyalars.IDDosya;
                        db.SaveChanges();

                    }

                    image.Dispose();
                    imgPhoto.Dispose();

                    System.IO.File.Delete(filepath);

                }
                int id = ilanlar.IDIlan;
                return RedirectToAction("Ilan", "Ilan", new { id });
            }
            catch (Exception)
            {
                return RedirectToAction("Panel", "Admin");
            }

        }
        [LoginFilter]
        public ActionResult IlanDuzenleMethod(Ilanlar ilanlar, AracGuvenlik aracGuvenliks, AracDisDonanim aracDisDonanims, AracIcDonanim aracIcDonanims, AracMultiMedya aracMultiMedyas, IEnumerable<HttpPostedFileBase> UploadFiles)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var Userid = Convert.ToInt16(Session["KullaniciId"]);
                //Formdan gelen arac güvenlik bilgilerini tabloya ekliyorum
                var aracguvenlik = db.AracGuvenlik.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();
                aracguvenlik.ABS = aracGuvenliks.ABS;
                aracguvenlik.CocukKilidi = aracGuvenliks.CocukKilidi;
                aracguvenlik.HavaYastigi = aracGuvenliks.HavaYastigi;
                aracguvenlik.YokusDestegi = aracGuvenliks.YokusDestegi;
                aracguvenlik.LastikAriza = aracGuvenliks.LastikAriza;
                aracguvenlik.MerkeziKilit = aracGuvenliks.MerkeziKilit;
                db.SaveChanges();


                //Formdan gelen Arac Dış Donanim bilgilerini tabloya ekliyorum

                var aracDisDonanim = db.AracDisDonanim.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();

                aracDisDonanim.ElektrikliAyna = aracDisDonanims.ElektrikliAyna;
                aracDisDonanim.FarLed = aracDisDonanims.FarLed;
                aracDisDonanim.FarSis = aracDisDonanims.FarSis;
                aracDisDonanim.OtomatikKatlananAyna = aracDisDonanims.OtomatikKatlananAyna;
                aracDisDonanim.ParkAsistani = aracDisDonanims.ParkAsistani;
                aracDisDonanim.ParkSensoru = aracDisDonanims.ParkSensoru;
                aracDisDonanim.RomorkDemiri = aracDisDonanims.RomorkDemiri;
                aracDisDonanim.Sunroof = aracDisDonanims.Sunroof;

                db.SaveChanges();
                var aracIcDonanim = db.AracIcDonanim.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();
                //Formdan gelen Arac iç Donanim bilgilerini tabloya ekliyorum

                aracIcDonanim.DeriKoltuk = aracIcDonanims.DeriKoltuk;
                aracIcDonanim.GeriGorusKamera = aracIcDonanims.GeriGorusKamera;
                aracIcDonanim.HidrolikDireksiyon = aracIcDonanims.HidrolikDireksiyon;
                aracIcDonanim.HizSabitleyici = aracIcDonanims.HizSabitleyici;

                aracIcDonanim.Klima = aracIcDonanims.Klima;
                aracIcDonanim.KumasKoltuk = aracIcDonanims.KumasKoltuk;
                aracIcDonanim.SogutmaliKoltuk = aracIcDonanims.SogutmaliKoltuk;
                aracIcDonanim.YolBilgisayarı = aracIcDonanims.YolBilgisayarı;
                db.SaveChanges();

                var aracMultiMedya = db.AracMultiMedya.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();
                //Formdan gelen Arac Multimedya bilgilerini tabloya ekliyorum

                aracMultiMedya.AUX = aracMultiMedyas.AUX;
                aracMultiMedya.Bluetooth = aracMultiMedyas.Bluetooth;
                aracMultiMedya.Navigasyon = aracMultiMedyas.Navigasyon;
                aracMultiMedya.Radyo = aracMultiMedyas.Radyo;
                aracMultiMedya.TV = aracMultiMedyas.TV;
                aracMultiMedyas.USB = aracMultiMedyas.USB;

                db.SaveChanges();

                var ilanlars = db.Ilanlar.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();

                ilanlars.Baslik = ilanlar.Baslik;
                ilanlars.IDAracMarka = ilanlar.IDAracMarka;
                ilanlars.IDAracModel = ilanlar.IDAracModel;
                ilanlars.Durum = "2";
                ilanlars.Aciklama = ilanlar.Aciklama;
                ilanlars.Cekis = ilanlar.Cekis;
                ilanlars.Fiyat = ilanlar.Fiyat;
                ilanlars.Kasa = ilanlar.Kasa;
                ilanlars.Kilometre = ilanlar.Kilometre;
                ilanlars.MotorGucu = ilanlar.MotorGucu;
                ilanlars.MotorHacmi = ilanlar.MotorHacmi;
                ilanlars.Vites = ilanlar.Vites;
                ilanlars.Yakit = ilanlar.Yakit;
                ilanlars.Yil = ilanlar.Yil;
                db.SaveChanges();
                var dosya = UploadFiles.FirstOrDefault();
                if (dosya!= null)
                {
                    try
                    {
                        foreach (var item in UploadFiles)
                        {
                            //Dosyayı kayıt ederken karışılık olmaması için bir guid oluşturuyorum
                            string FileName = item.FileName;
                            string ext = System.IO.Path.GetExtension(FileName).ToLower();
                            string guid = Guid.NewGuid().ToString();
                            guid = guid.Replace("-", string.Empty);
                            guid = guid.Substring(0, 16) + ext;

                            //Resimleri verdiğim uzantıya kayıt ediyorum
                            string filepath = System.Web.HttpContext.Current.Server.MapPath("~") + "Dosyalar/Public/" + guid;

                            item.SaveAs(filepath);

                            //Dosyaya yüklediğim resmi yeniden boyutlandırmak için farkı bir isimle kayıt etmek için yeni bir guid oluşturuyorum
                            string FileName2 = item.FileName;
                            string ext2 = System.IO.Path.GetExtension(FileName2).ToLower();
                            string guid2 = Guid.NewGuid().ToString();
                            guid2 = guid.Replace("-", string.Empty);
                            guid2 = guid.Substring(0, 16) + ext2;
                            string filepath2 = System.Web.HttpContext.Current.Server.MapPath("~") + "Dosyalar/Public/a" + guid2;

                            //Burada yüklenen fotoğrafın boyutunu 555x500 şeklinde kırpıyorum ve dosyayı kayıt ediyorum
                            Image imgPhoto = Image.FromFile(filepath);
                            Bitmap image = ImageResize.ResizeImage(imgPhoto, 555, 500);
                            image.Save(filepath2);

                            //Burada ise yüklenen fotoğrafı dosyalar tablosuna ekliyorum
                            if (System.IO.File.Exists(filepath2))
                            {
                                var dosyalar = db.Dosyalar.Where(x => x.IDIlan == ilanlar.IDIlan).OrderBy(x=>x.IDDosya).FirstOrDefault();
                                if (dosyalar.Url == "default-car.jpg")
                                {
                                    dosyalar.IDIlan = ilanlar.IDIlan;
                                    dosyalar.IDKullanici = Userid;
                                    dosyalar.tip = "ilan";
                                    dosyalar.Url = "a" + guid2;
                                }
                                else
                                {
                                    Dosyalar dosyalars = new Dosyalar();

                                    dosyalars.IDIlan = ilanlar.IDIlan;
                                    dosyalars.IDKullanici = Userid;
                                    dosyalars.tip = "ilan";
                                    dosyalars.Url = "a" + guid2;
                                    db.Dosyalar.Add(dosyalars);
                                }


                                //Ilanın ilk fotoğrafını belirmek için buradaki koşulu kullanıyorum.
                               
                                if (dosyalar == null)
                                {
                                    dosyalar.IlkFoto = true;
                                }
                                else
                                {
                                    dosyalar.IlkFoto = false;
                                }
                                ilanlar.IDDosya = dosyalar.IDDosya;
                                db.SaveChanges();

                            }

                            image.Dispose();
                            imgPhoto.Dispose();

                            System.IO.File.Delete(filepath);

                        }


                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Panel", "Admin");
                    }
                }
                int id = ilanlar.IDIlan;
                return RedirectToAction("IlanDuzenle", new { id });
            }
        }
        

        public ActionResult IlanListesi(string ara,string marka, string modeli, string vites,string kasa, string yakit,string cekis, int? minfiyat, int? maxfiyat)
        {

            IlanListeViewModel model = new IlanListeViewModel();

            var sorgu = db.Ilanlar.Include("AracMarka").Include("AracModel").Include("AracGuvenlik").Include("AracDisDonanim").Include("AracIcDonanim").Include("AracMultiMedya").Include("Kullanici").Include("Dosyalar").Where(x=>x.Durum =="3").ToList();

            if (ara != "" && ara != null)
            {
                ara = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ara);
                sorgu = sorgu.Where(x => x.Baslik.Contains(ara) || x.AracMarka.Ad.Contains(ara) || x.AracModel.ModelAd.Contains(ara)).ToList();
            }
            if (marka != null)
            {   
              sorgu =  sorgu.Where(x => x.AracMarka.Ad == marka).ToList();
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
                sorgu = sorgu.Where(x=>x.Fiyat <= maxfiyat && x.Fiyat >= minfiyat).ToList();
            }

            model.Ilanlars = sorgu.ToList();
            model.Dosyalar = db.Dosyalar.Where(x=>x.tip == "ilan" && x.IlkFoto == true).FirstOrDefault();
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
        [LoginFilter]
        public ActionResult IlanDuzenle(int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var Userid = Convert.ToInt16(Session["KullaniciId"]);

                ViewBag.marka = db.AracMarka.ToList();
                ViewBag.model = db.AracModel.ToList();

                var ilanlar = db.Ilanlar.Include("AracMarka").Include("AracModel").Include("Kullanici").Include("Dosyalar").Include("AracGuvenlik").Include("AracIcDonanim").Include("AracDisDonanim").Include("AracMultiMedya").Where(x => x.IDIlan == id && x.IDMusteri == Userid).FirstOrDefault();
                if (ilanlar == null)
                {
                    return RedirectToAction("IlanListesi");
                }
                ViewBag.gorseller = db.Dosyalar.Where(x => x.IDIlan == ilanlar.IDIlan && x.tip == "ilan").ToList();
                return View(ilanlar);
            }

        }

        public ActionResult IlanFotoSil(int ID)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var control = db.Dosyalar.Where(x => x.IDDosya == ID && x.tip=="ilan").FirstOrDefault();
                var fotolar = db.Dosyalar.Where(x => x.IDIlan == control.IDIlan).ToList();
                if (fotolar.Count<2 )
                {
                    control.Url = "default-car.jpg";
                    db.SaveChanges();
                }
                else
                {
                    db.Dosyalar.Remove(control);
                    db.SaveChanges();
                }
                var id = control.IDIlan;
                return RedirectToAction("IlanDuzenle", new { id });
            }

        }

        public ActionResult IlanSil(int ID)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {

                var ilanlars = db.Ilanlar.Where(x => x.IDIlan == ID).FirstOrDefault();
                var dosyalar = db.Dosyalar.Where(x => x.IDIlan == ID).ToList();
                var guvenlik = db.AracGuvenlik.Where(x => x.IDIlan == ID).FirstOrDefault();
                var icdonanim = db.AracIcDonanim.Where(x => x.IDIlan == ID).FirstOrDefault();
                var disdonanim = db.AracDisDonanim.Where(x => x.IDIlan == ID).FirstOrDefault();
                var multimedya = db.AracMultiMedya.Where(x => x.IDIlan == ID).FirstOrDefault();

                db.Ilanlar.Remove(ilanlars);
                db.AracGuvenlik.Remove(guvenlik);
                db.AracIcDonanim.Remove(icdonanim);
                db.AracDisDonanim.Remove(disdonanim);
                db.AracMultiMedya.Remove(multimedya);
                db.Dosyalar.RemoveRange(dosyalar);

                db.SaveChanges();


                return RedirectToAction("Ilanlarim");

            }
        }

        public JsonResult AracMarkaListesi(string marka, int? idmarka)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                if (idmarka != null)
                {
                    var idmodel = db.AracModel.Where(x => x.AracMarka.IDAracMarka == idmarka).ToList();
                    return Json(idmodel);
                }
                var model = db.AracModel.Where(x => x.AracMarka.Ad == marka).ToList();
                return Json(model);
            }

        }

    }
}