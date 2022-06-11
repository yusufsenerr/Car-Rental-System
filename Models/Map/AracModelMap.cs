using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class AracModelMap : EntityTypeConfiguration<AracModel>
    {
        public AracModelMap()
        {
            this.ToTable("AracModel");
            this.Property(p => p.IDAracModel).HasColumnType("int");
            this.Property(p => p.IDAracModel).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.ModelAd).HasColumnType("nvarchar").HasMaxLength(30);

            this.HasRequired(p => p.AracMarka).WithMany(p => p.AracModels).HasForeignKey(x=>x.IDAracMarka);

        }
    }
}