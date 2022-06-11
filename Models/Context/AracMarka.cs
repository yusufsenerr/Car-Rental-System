using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AracKiralamaOtomasyonu.Models
{
    public class AracMarka
    {
        [Key]
        public int IDAracMarka { get; set; }
        public string Ad { get; set; }

        public virtual ICollection<AracModel> AracModels { get;set; }
        public virtual ICollection<Ilanlar> Ilanlars { get; set; }
    }
}