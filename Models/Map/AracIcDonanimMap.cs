using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class AracIcDonanimMap: EntityTypeConfiguration<AracIcDonanim>
    {
            public AracIcDonanimMap()
            {
                this.ToTable("AracIcDonanim");
                this.Property(p => p.IDAracIcDonanim).HasColumnType("int");
                this.Property(p => p.IDAracIcDonanim).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                this.Property(p => p.IDIlan).HasColumnType("int");
                this.Property(p => p.DeriKoltuk).HasColumnType("bit");
                this.Property(p => p.GeriGorusKamera).HasColumnType("bit");
                this.Property(p => p.HidrolikDireksiyon).HasColumnType("bit");
                this.Property(p => p.HizSabitleyici).HasColumnType("bit");
                this.Property(p => p.HizSabitleyici).HasColumnType("bit");
                this.Property(p => p.Klima).HasColumnType("bit");
                this.Property(p => p.KumasKoltuk).HasColumnType("bit");
                this.Property(p => p.SogutmaliKoltuk).HasColumnType("bit");
                this.Property(p => p.YolBilgisayarı).HasColumnType("bit");
        }
    }
}