using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class Personel
    {
        [Key]
        public int IDPersonel { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public byte[] SifreHash { get; set; }
        public byte[] SifreSalt { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public System.DateTime SonGirisTarihi { get; set; }
        public short YetkiSeviyesi { get; set; }
    }
}