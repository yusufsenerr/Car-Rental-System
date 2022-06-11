using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Context
{
    public class Dosyalar
    {
        [Key]
        public int IDDosya{ get; set; }
        public int IDKullanici { get; set; }
        public Nullable<int>  IDIlan { get; set; }
        public string Url { get; set; }
        public string tip { get; set; }
        public bool IlkFoto { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}