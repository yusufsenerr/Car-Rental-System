using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class Rezervasyon
    {
        [Key]
        public int IDRezervasyon { get; set; }
        public int IDIlan { get; set; }
        public int IDSatici { get; set; }
        public int IDAlici { get; set; }
        public int Fiyat { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Durum { get; set; }

        public Ilanlar Ilanlar { get; set; }

    }
}