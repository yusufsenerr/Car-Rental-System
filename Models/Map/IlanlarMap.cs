    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using AracKiralamaOtomasyonu.Models;

namespace AracKiralamaOtomasyonu.Models.Map
{
    public class IlanlarMap : EntityTypeConfiguration<Ilanlar>
    {
        public IlanlarMap()
        {
            this.ToTable("Ilanlar");
            this.Property(p => p.IDIlan).HasColumnType("int");
            this.Property(p => p.IDIlan).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Durum).HasColumnType("nvarchar").HasMaxLength(10);
            this.Property(p => p.Baslik).HasColumnType("nvarchar").HasMaxLength(250);
            this.Property(p => p.Cekis).HasColumnType("nvarchar").HasMaxLength(30);
            this.Property(p => p.Fiyat).HasColumnType("nvarchar").HasColumnType("int");
            this.Property(p => p.IlanTarihi).HasColumnType("date");
            this.Property(p => p.Kasa).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.Kilometre).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.MotorGucu).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.MotorHacmi).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.Vites).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.Yakit).HasColumnType("nvarchar").HasMaxLength(50);
            this.Property(p => p.Yil).HasColumnType("date");
            this.Property(p => p.Aciklama).HasColumnType("nvarchar").HasMaxLength(500);


            this.HasRequired(p => p.Kullanici).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDMusteri);
            this.HasRequired(p => p.Dosyalar).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDDosya);
            this.HasRequired(p => p.AracMarka).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDAracMarka);
            this.HasRequired(p => p.AracModel).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDAracModel);
            this.HasRequired(p => p.AracGuvenlik).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDAracGuvenlik);
            this.HasRequired(p => p.AracIcDonanim).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDAracIcDonanim);
            this.HasRequired(p => p.AracDisDonanim).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDAracDisDonanim);
            this.HasRequired(p => p.AracMultiMedya).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDAracMultiMedya);
            this.HasRequired(p => p.Kurumsal).WithMany(p => p.Ilanlars).HasForeignKey(p => p.IDKurumsal);




        }
    }
}