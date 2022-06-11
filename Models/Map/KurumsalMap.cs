using AracKiralamaOtomasyonu.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class KurumsalMap : EntityTypeConfiguration<Kurumsal>
    {
        public KurumsalMap()
        {
            this.ToTable("Kurumsal");
            this.Property(p => p.ID).HasColumnType("int");
            this.Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Ad).HasColumnType("nvarchar").HasMaxLength(30);
            
            this.Property(p => p.Adres).HasColumnType("nvarchar").HasMaxLength(120);
            this.Property(p => p.Telefon).HasColumnType("nvarchar").HasMaxLength(15);
            this.Property(p => p.LogoUrl).HasColumnType("nvarchar").HasMaxLength(30);


        }
    }
}