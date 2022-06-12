using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AracKiralamaOtomasyonu.Models.Context;

namespace AracKiralamaOtomasyonu.Models
{
    public class Kullanici
    {
        [Key]
        public int IDKullanici { get; set; }
        public int YetkiSeviye { get; set; }
        public string ProfilFotoUrl { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAd { get; set; }
        public string Telefon { get; set; }
        public string TCKimlik { get; set; }
        public string Adres { get; set; }
        public System.DateTime DogumTarihi { get; set; }
        public bool Cinsiyet { get; set; }
        public string Eposta { get; set; }
        public byte[] SifreHash { get; set; }
        public byte[] SifreSalt { get; set; }   
        public bool Dogrulama { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public System.DateTime SonGirisTarihi { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }

        public virtual ICollection<Kurumsal> Kurumsal { get; set; }

    }
}