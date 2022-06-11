using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class EpostaDogrulama
    {
        [Key]
        public int IDEpostaDogrulama { get; set; }
        public int KullaniciID { get; set; }
        public System.DateTime KayitTarihi { get; set; }
        public System.DateTime DogrulamaTarihi { get; set; }

        public bool DogrulamaDurumu { get; set; }
        public string Eposta { get; set; }
        public string DogrulamaLink { get; set; }
    }
}