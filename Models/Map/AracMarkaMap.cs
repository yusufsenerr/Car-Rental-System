using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class AracMarkaMap: EntityTypeConfiguration<AracMarka>
    {
        public AracMarkaMap()
        {
            this.ToTable("AracMarka");
            this.Property(p => p.IDAracMarka).HasColumnType("int");
            this.Property(p => p.IDAracMarka).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Ad).HasColumnType("nvarchar").HasMaxLength(30);

        }
    }
}