using AracKiralamaOtomasyonu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracKiralamaOtomasyonu.Controllers
{
    public class RezervasyonController : Controller
    {
        // GET: Rezervasyon
        public ActionResult Ekle(string AlisTarih, string BitisTarih, int IDIlan, int IDSatici, double Fiyat)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var Userid = Convert.ToInt16(Session["KullaniciId"]);
                Rezervasyon result = new Rezervasyon();

                var ATarhi = Convert.ToDateTime(AlisTarih);
                var BTarhi = Convert.ToDateTime(BitisTarih);

                TimeSpan GunSayisi = BTarhi - ATarhi;
                double toplamGun = GunSayisi.TotalDays;
                var FiyatHesap = Fiyat * toplamGun;

                result.BaslangicTarihi = ATarhi;
                result.BitisTarihi = BTarhi;
                result.Fiyat = Convert.ToInt32(FiyatHesap);
                result.Durum = true;
                result.IDAlici = Userid;
                result.IDSatici = IDSatici;
                result.IDIlan = IDIlan;

                db.Rezervasyon.Add(result);
                db.SaveChanges();

                var ilan = db.Ilanlar.Where(x => x.IDIlan == IDIlan).FirstOrDefault();
                ilan.Durum = "4";
                db.SaveChanges();
            }
            return RedirectToAction("IlanListesi", "Ilan");
        }
        public ActionResult Liste()
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var Userid = Convert.ToInt16(Session["KullaniciId"]);

                var rezv = db.Rezervasyon.Where(x => x.IDSatici == Userid).ToList();
                return View(rezv);
            }


        }
    }
}