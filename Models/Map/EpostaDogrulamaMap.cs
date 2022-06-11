using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class EpostaDogrulamaMap : EntityTypeConfiguration<EpostaDogrulama>
    {
        public EpostaDogrulamaMap()
        {
            this.ToTable("EpostaDogrulama");
            this.Property(p => p.IDEpostaDogrulama).HasColumnType("int");
            this.Property(p => p.IDEpostaDogrulama).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Eposta).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.KayitTarihi).HasColumnType("date");
            this.Property(p => p.DogrulamaTarihi).HasColumnType("date");
            this.Property(p => p.KullaniciID).HasColumnType("int");
            this.Property(p => p.DogrulamaDurumu).HasColumnType("bit");
            this.Property(p => p.DogrulamaLink).HasColumnType("nvarchar").HasMaxLength(100);

        }
    }
}