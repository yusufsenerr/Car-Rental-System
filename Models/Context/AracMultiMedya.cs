using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
        using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class AracMultiMedya
    {
        [Key]
        public int IDAracMultiMedya { get; set; }
        public int IDIlan { get; set; }
        public bool Radyo { get; set; }
        public bool TV { get; set; }
        public bool Bluetooth { get; set; }
        public bool USB { get; set; }
        public bool AUX { get; set; }
        public bool Navigasyon { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}