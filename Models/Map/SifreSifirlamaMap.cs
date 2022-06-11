using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models
{
    public class SifreSifirlamaMap : EntityTypeConfiguration<SifreSifirlama>
    {
        public SifreSifirlamaMap()
        {
            this.ToTable("SifreSifirlama");
            this.Property(p => p.IDSifreSifirlama).HasColumnType("int");
            this.Property(p => p.IDSifreSifirlama).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.KullaniciID).HasColumnType("int");
            this.Property(p => p.Eposta).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.KayitTarihi).HasColumnType("date");
            this.Property(p => p.Dogrulama).HasColumnType("bit");
            this.Property(p => p.DogrulamaLinki).HasColumnType("nvarchar").HasMaxLength(50);

        }
    }
}