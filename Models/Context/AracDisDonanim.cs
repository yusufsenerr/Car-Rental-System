using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models
{
    public class AracDisDonanim
    {
        [Key]
        public int IDAracDisDonanim { get; set; }
        public int IDIlan { get; set; }
        public bool FarLed { get; set; }
        public bool FarSis { get; set; }
        public bool Sunroof { get; set; }
        public bool ElektrikliAyna { get; set; }
        public bool ParkSensoru { get; set; }
        public bool OtomatikKatlananAyna { get; set; }
        public bool RomorkDemiri { get; set; }
        public bool ParkAsistani { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}