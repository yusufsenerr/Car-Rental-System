using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class AracModel
    {
        [Key]
        public int IDAracModel { get; set; }

        public string ModelAd { get; set; }
        public int IDAracMarka { get; set; }
        public virtual AracMarka AracMarka { get; set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }

    }
}