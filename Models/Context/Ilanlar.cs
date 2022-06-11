using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AracKiralamaOtomasyonu.Models.Context;

namespace AracKiralamaOtomasyonu.Models
{
    public class Ilanlar
    {
        [Key]
        public int IDIlan { get; set; }
        public int IDAracMarka { get; set; }
        public int IDAracModel { get; set; }
        public int IDMusteri { get; set; }

        public Nullable<int> IDAracIcDonanim { get; set; }
        public Nullable<int> IDAracGuvenlik { get; set; }
        public Nullable<int> IDAracDisDonanim { get; set; }
        public Nullable<int> IDAracMultiMedya { get; set; }
        public Nullable<int> IDKurumsal { get; set; }
        public int IDDosya { get; set; }

        public string Durum { get; set; }
        public string Baslik { get; set; }
        public int Fiyat { get; set; }
        public string Kilometre { get; set; }
        public System.DateTime Yil { get; set; }
        public string Yakit { get; set; }
        public string Vites { get; set; }
        public string Kasa { get; set; }
        public string MotorGucu { get; set; }
        public string MotorHacmi { get; set; }
        public string Cekis { get; set; }
        public System.DateTime IlanTarihi { get; set; }
        public string Aciklama { get; set; }

        public virtual AracMarka AracMarka { get; set; }
        public virtual AracModel AracModel { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Dosyalar Dosyalar { get; set; }
        public virtual AracGuvenlik AracGuvenlik { get; set; }
        public virtual Kurumsal Kurumsal { get; set; }
        public virtual AracDisDonanim AracDisDonanim { get; set; }
        public virtual AracIcDonanim AracIcDonanim { get; set; }
        public virtual AracMultiMedya AracMultiMedya { get; set; }

        public virtual ICollection<Rezervasyon> Rezervasyon { get; set; }
    }
    
}