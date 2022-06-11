using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class AracGuvenlikMap : EntityTypeConfiguration<AracGuvenlik>
    {
        public AracGuvenlikMap()
        {
            this.ToTable("AracGuvenlik");
            this.Property(p => p.IDAracGuvenlik).HasColumnType("int");
            this.Property(p => p.IDAracGuvenlik).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.IDIlan).HasColumnType("int");
            this.Property(p => p.ABS).HasColumnType("bit");
            this.Property(p => p.CocukKilidi).HasColumnType("bit");
            this.Property(p => p.HavaYastigi).HasColumnType("bit");
            this.Property(p => p.YokusDestegi).HasColumnType("bit");
            this.Property(p => p.MerkeziKilit).HasColumnType("bit");
            this.Property(p => p.LastikAriza).HasColumnType("bit");
        }
    }
}