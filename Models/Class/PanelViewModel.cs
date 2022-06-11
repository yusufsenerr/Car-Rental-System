using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AracKiralamaOtomasyonu.Models.Class
{
    public class PanelViewModel
    {
        public List<Ilanlar> Ilanlars { get; set; }
        public List<Personel> Personels { get; set; }
        public List<Kullanici> Kullanicis { get; set; }
        public List <Rezervasyon> Rezervasyon { get; set; }

    }
}