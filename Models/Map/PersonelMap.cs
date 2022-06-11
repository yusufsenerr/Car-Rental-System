using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class PersonelMap: EntityTypeConfiguration<Personel>
    {
        public PersonelMap()
        {
            this.ToTable("Personel");
            this.Property(p => p.IDPersonel).HasColumnType("int");
            this.Property(p => p.IDPersonel).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Ad).HasColumnType("nvarchar").HasMaxLength(30);
            this.Property(p => p.Soyad).HasColumnType("nvarchar").HasMaxLength(250);
            this.Property(p => p.KayitTarihi).HasColumnType("date");
            this.Property(p => p.Mail).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.SifreHash).HasColumnType("varbinary").HasMaxLength(500);
            this.Property(p => p.SifreSalt).HasColumnType("varbinary").HasMaxLength(500); 
            this.Property(p => p.SonGirisTarihi).HasColumnType("date");
            this.Property(p => p.Telefon).HasColumnType("nvarchar").HasMaxLength(15);
            this.Property(p => p.YetkiSeviyesi).HasColumnType("smallint");
        }
    }
}