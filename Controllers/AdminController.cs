using System.Linq;
using System.Web.Mvc;
using static AracKiralamaOtomasyonu.Filtre.GirisFiltre;
using AracKiralamaOtomasyonu.Models;
using AracKiralamaOtomasyonu.Models.Class;
using System;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using AracKiralamaOtomasyonu.Models.Context;

namespace AracKiralamaOtomasyonu.Controllers
{
    [LoginFilter]
    public class AdminController : Controller
    {

        public ActionResult Panel()
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var ilan = db.Ilanlar;

                ViewBag.ToplamOnaylanmayanIlan = ilan.Where(x => x.Durum == "2").Count();
                ViewBag.ToplamIlan = ilan.ToList().Count();
                PanelViewModel model = new PanelViewModel();
                model.Ilanlars = ilan.Include("AracMarka").Include("AracModel").Where(x=>x.Durum=="2").OrderByDescending(x => x.IlanTarihi).ToList();
                model.Kullanicis = db.Kullanici.Where(x => x.Dogrulama == true).ToList();
                model.Personels = db.Personel.OrderBy(x => x.Ad).ToList();

                return View(model);
            }   

        }

        public ActionResult  IlanSil(int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                var ilanlar = db.Ilanlar.Where(x => x.IDIlan == id).FirstOrDefault();
                db.Ilanlar.Remove(ilanlar);
                db.SaveChanges();

                return RedirectToAction("Panel");
            }
        }

        public ActionResult IlanDuzenle(int id)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {
                
                ViewBag.marka = db.AracMarka.ToList();
                ViewBag.model = db.AracModel.ToList();

                var ilanlar =  db.Ilanlar.Include("AracMarka").Include("AracModel").Include("Kullanici").Where(x => x.IDIlan == id).FirstOrDefault();
                ViewBag.gorseller = db.Dosyalar.Where(x => x.IDIlan == ilanlar.IDIlan && x.tip=="ilan").ToList();
                return View(ilanlar);
            }
                
        }

        public ActionResult IlanOnay(Ilanlar ilanlar)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {

                var ilanlars = db.Ilanlar.Where(x => x.IDIlan == ilanlar.IDIlan).FirstOrDefault();

                ilanlars.Baslik = ilanlar.Baslik;
                ilanlars.IDAracMarka = ilanlar.IDAracMarka;
                ilanlars.IDAracModel = ilanlar.IDAracModel;
                ilanlars.Durum = "3";
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
                return RedirectToAction("Panel");

            }
        }

        public ActionResult IlanReddet(int IlanID)
        {
            using (AracKiralamaContext db = new AracKiralamaContext())
            {

                var ilanlars = db.Ilanlar.Where(x => x.IDIlan == IlanID).FirstOrDefault();

                ilanlars.Durum = "1";
                db.SaveChanges();


                return RedirectToAction("Panel");

            }
        }
    }
}