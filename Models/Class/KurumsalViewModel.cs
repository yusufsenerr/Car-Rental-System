using AracKiralamaOtomasyonu.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Class
{
        public class KurumsalListe
        {
            public List<Kurumsal> Kurumsal { get; set; }
            public string arama { get; set; }
        }
}