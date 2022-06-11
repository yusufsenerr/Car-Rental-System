using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class KullaniciMap : EntityTypeConfiguration<Kullanici>
    {
        public KullaniciMap()
        {
            this.ToTable("Kullanici");
            this.Property(p => p.IDKullanici).HasColumnType("int");
            this.Property(p => p.IDKullanici).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.YetkiSeviye).HasColumnType("int");
            this.Property(p => p.ProfilFotoUrl).HasColumnType("nvarchar").HasMaxLength(100);
            this.Property(p => p.Ad).HasColumnType("nvarchar").HasMaxLength(30);
            this.Property(p => p.Adres).HasColumnType("nvarchar").HasMaxLength(250);
            this.Property(p => p.Cinsiyet).HasColumnType("bit");
            this.Property(p => p.DogumTarihi).HasColumnType("date");
            this.Property(p => p.Eposta).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.KayitTarihi).HasColumnType("date");
            this.Property(p => p.KullaniciAd).HasColumnType("nvarchar").HasMaxLength(30);
            this.Property(p => p.SifreHash).HasColumnType("varbinary").HasMaxLength(500);
            this.Property(p => p.SifreSalt).HasColumnType("varbinary").HasMaxLength(500);
            this.Property(p => p.SonGirisTarihi).HasColumnType("date");
            this.Property(p => p.Soyad).HasColumnType("nvarchar").HasMaxLength(30);
            this.Property(p => p.TCKimlik).HasColumnType("nvarchar").HasMaxLength(11);
            this.Property(p => p.Dogrulama).HasColumnType("bit");
            this.Property(p => p.Telefon).HasColumnType("nvarchar").HasMaxLength(15);
        }
    }
}