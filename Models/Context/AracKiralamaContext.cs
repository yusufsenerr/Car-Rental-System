using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AracKiralamaOtomasyonu.Models.Map;
using AracKiralamaOtomasyonu.Models.Context;

namespace AracKiralamaOtomasyonu.Models
{

    public class AracKiralamaContext: DbContext
    {
        public AracKiralamaContext() : base("name=AracKiralama")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AracDisDonanimMap());
            modelBuilder.Configurations.Add(new AracGuvenlikMap());
            modelBuilder.Configurations.Add(new AracIcDonanimMap());
            modelBuilder.Configurations.Add(new AracMarkaMap());
            modelBuilder.Configurations.Add(new AracModelMap());
            modelBuilder.Configurations.Add(new AracMultiMedyaMap());
            modelBuilder.Configurations.Add(new IlanlarMap());
            modelBuilder.Configurations.Add(new KullaniciMap());
            modelBuilder.Configurations.Add(new PersonelMap());
            modelBuilder.Configurations.Add(new RezervasyonMap());
            modelBuilder.Configurations.Add(new EpostaDogrulamaMap());
            modelBuilder.Configurations.Add(new SifreSifirlamaMap());
            modelBuilder.Configurations.Add(new DosyaMap());
            modelBuilder.Configurations.Add(new KurumsalMap());
        }

        public DbSet<AracDisDonanim> AracDisDonanim { get; set; }
        public DbSet<AracGuvenlik> AracGuvenlik { get; set; }
        public DbSet<AracIcDonanim> AracIcDonanim { get; set; }
        public DbSet<AracMarka> AracMarka { get; set; }
        public DbSet<Ilanlar> Ilanlar { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<AracMultiMedya> AracMultiMedya { get; set; }
        public DbSet<Rezervasyon> Rezervasyon { get; set; }
        public DbSet<AracModel> AracModel { get; set; }
        public DbSet<EpostaDogrulama> EpostaDogrulama { get; set; }
        public DbSet<SifreSifirlama> SifreSifirlama { get; set; }
        public DbSet<Dosyalar> Dosyalar { get; set; }
        public DbSet<Kurumsal> Kurumsal {get;set; }
    }
}