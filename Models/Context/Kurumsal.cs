using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Context
{
    public class Kurumsal
    {
        [Key]
        public int ID{ get; set; }
        public string Eposta { get; set; }
        public byte[] SifreHash { get; set; }
        public byte[] SifreSalt { get; set; }

        public string Ad { get; set; }

        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string LogoUrl { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public System.DateTime SonGirisTarihi { get; set; }

        public virtual Kullanici Kullanici { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}