using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
namespace AracKiralamaOtomasyonu.Models.Map
{
    public class RezervasyonMap:EntityTypeConfiguration<Rezervasyon>
    {
        public RezervasyonMap()
        {
            this.ToTable("Rezervasyon");
            this.Property(p => p.IDRezervasyon).HasColumnType("int");
            this.Property(p => p.IDRezervasyon).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.IDAlici).HasColumnType("int");
            this.Property(p => p.IDSatici).HasColumnType("int");
            this.Property(p => p.IDIlan).HasColumnType("int");
            this.Property(p => p.BaslangicTarihi).HasColumnType("date");
            this.Property(p => p.BitisTarihi).HasColumnType("date");
            this.Property(p => p.Durum).HasColumnType("bit");

            this.HasRequired(p => p.Ilanlar).WithMany(p => p.Rezervasyon).HasForeignKey(p => p.IDIlan);

        }
    }
}