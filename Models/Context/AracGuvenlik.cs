using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class AracGuvenlik
    {
        [Key]
        public int IDAracGuvenlik { get; set; }
        public int IDIlan { get; set; }
        public bool ABS { get; set; }
        public bool YokusDestegi { get; set; }
        public bool HavaYastigi { get; set; }
        public bool CocukKilidi { get; set; }
        public bool LastikAriza { get; set; }
        public bool MerkeziKilit { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}