using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class AracIcDonanim
    {
        [Key]
        public int IDAracIcDonanim { get; set; }
        public int IDIlan { get; set; }
        public bool DeriKoltuk { get; set; }
        public bool SogutmaliKoltuk { get; set; }
        public bool YolBilgisayarı { get; set; }
        public bool KumasKoltuk { get; set; }
        public bool Klima { get; set; }
        public bool HidrolikDireksiyon { get; set; }
        public bool HizSabitleyici { get; set; }
        public bool GeriGorusKamera { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}