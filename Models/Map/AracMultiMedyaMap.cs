using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class AracMultiMedyaMap: EntityTypeConfiguration<AracMultiMedya>
    {
        public AracMultiMedyaMap()
        {
            this.ToTable("AracMultiMedya");
            this.Property(p => p.IDAracMultiMedya).HasColumnType("int");
            this.Property(p => p.IDAracMultiMedya).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.IDIlan).HasColumnType("int");
            this.Property(p => p.AUX).HasColumnType("bit");
            this.Property(p => p.Bluetooth).HasColumnType("bit");
            this.Property(p => p.Navigasyon).HasColumnType("bit");
            this.Property(p => p.Radyo).HasColumnType("bit");
            this.Property(p => p.TV).HasColumnType("bit");
            this.Property(p => p.USB).HasColumnType("bit");
        }
    }
}