using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class AracDisDonanimMap : EntityTypeConfiguration<AracDisDonanim>
    {
        public AracDisDonanimMap()
        {
            this.ToTable("AracDisDonanim");
            this.Property(p => p.IDAracDisDonanim).HasColumnType("int");
            this.Property(p => p.IDAracDisDonanim).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.IDIlan).HasColumnType("int");
            this.Property(p => p.FarLed).HasColumnType("bit");
            this.Property(p => p.ElektrikliAyna).HasColumnType("bit");
            this.Property(p => p.FarSis).HasColumnType("bit");
            this.Property(p => p.OtomatikKatlananAyna).HasColumnType("bit");
            this.Property(p => p.ParkAsistani).HasColumnType("bit");
            this.Property(p => p.ParkSensoru).HasColumnType("bit");
            this.Property(p => p.RomorkDemiri).HasColumnType("bit");
            this.Property(p => p.Sunroof).HasColumnType("bit");
        }
    }
}