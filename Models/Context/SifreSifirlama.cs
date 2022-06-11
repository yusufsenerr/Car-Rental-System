using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models
{
    public class SifreSifirlama
    {
        [Key]
        public int IDSifreSifirlama { get; set; }
        public int KullaniciID { get; set; }
        public string Eposta { get; set; }
        public bool Dogrulama { get; set; }
        public string DogrulamaLinki { get; set; }
        public System.DateTime KayitTarihi { get; set; }
    }
}