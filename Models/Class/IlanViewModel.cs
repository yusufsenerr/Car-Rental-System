using AracKiralamaOtomasyonu.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Class
{
    public class IlanViewModel
    {
        public Ilanlar Ilanlars { get; set; }
        public Dosyalar Dosyalar { get; set; }
        public List<Dosyalar> Dosyalars { get; set; }
    }

    public class IlanListeViewModel
    {
        public List<Ilanlar> Ilanlars { get; set; }
        public Dosyalar Dosyalar { get; set; }
        public List<AracMarka> AracMarka { get; set; }

        public string arama { get; set; }
        public string marka { get; set; }
        public string modeli { get; set; }
        public string vites { get; set; }
        public string kasa { get; set; }
        public string yakit { get; set; }
        public string cekis { get; set; }
        public Nullable<int> minfiyati { get; set; }
        public Nullable<int> maxfiyati { get; set; }
    }

    public class IlanEkleViewModel
    {
        public Ilanlar Ilanlars { get; set; }
        public List<AracMarka> AracMarka { get; set; }
        public List<AracModel> AracModel { get; set; }
        public List<Dosyalar> Dosyalars { get; set; }
    }
}