using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using AracKiralamaOtomasyonu.Models.Context;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class DosyaMap : EntityTypeConfiguration<Dosyalar>
    {
        public DosyaMap()
        {
            this.ToTable("Dosyalar");
            this.Property(p => p.IDDosya).HasColumnType("int");
            this.Property(p => p.IDDosya).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.IDIlan).HasColumnType("int");
            this.Property(p => p.Url).HasColumnType("nvarchar").HasMaxLength(250);
            this.Property(p => p.IlkFoto).HasColumnType("bit");
            this.Property(p => p.tip).HasColumnType("nvarchar").HasMaxLength(20);
        }

    }
}