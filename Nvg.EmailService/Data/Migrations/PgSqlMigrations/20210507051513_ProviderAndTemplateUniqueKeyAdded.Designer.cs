﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Nvg.EmailService.Data;

namespace Nvg.EmailService.data.Migrations.PgSqlMigrations
{
    [DbContext(typeof(EmailPgSqlDBContext))]
    [Migration("20210507051513_ProviderAndTemplateUniqueKeyAdded")]
    partial class ProviderAndTemplateUniqueKeyAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Email")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailChannelTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("EmailPoolID")
                        .HasColumnType("text");

                    b.Property<string>("EmailProviderID")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EmailPoolID");

                    b.HasIndex("EmailProviderID");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("EmailChannel");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailHistoryTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<int>("Attempts")
                        .HasColumnType("integer");

                    b.Property<string>("EmailChannelID")
                        .HasColumnType("text");

                    b.Property<string>("EmailProviderID")
                        .HasColumnType("text");

                    b.Property<string>("MessageSent")
                        .HasColumnType("text");

                    b.Property<string>("Recipients")
                        .HasColumnType("text");

                    b.Property<string>("Sender")
                        .HasColumnType("text");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<string>("TemplateName")
                        .HasColumnType("text");

                    b.Property<string>("TemplateVariant")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EmailChannelID");

                    b.HasIndex("EmailProviderID");

                    b.ToTable("EmailHistory");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailPoolTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("EmailPool");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailProviderSettingsTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Configuration")
                        .HasColumnType("text");

                    b.Property<string>("EmailPoolID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EmailPoolID");

                    b.HasIndex("Name", "EmailPoolID")
                        .IsUnique();

                    b.ToTable("EmailProviderSettings");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailQuotaTable", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("EmailChannelID")
                        .HasColumnType("text");

                    b.Property<int>("MonthlyConsumption")
                        .HasColumnType("integer");

                    b.Property<int>("MonthlyQuota")
                        .HasColumnType("integer");

                    b.Property<int>("TotalConsumption")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("EmailChannelID");

                    b.ToTable("EmailQuota");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailTemplateTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("EmailPoolID")
                        .HasColumnType("text");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Sender")
                        .HasColumnType("text");

                    b.Property<string>("Variant")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("EmailPoolID");

                    b.HasIndex("Name", "EmailPoolID")
                        .IsUnique();

                    b.ToTable("EmailTemplate");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailChannelTable", b =>
                {
                    b.HasOne("Nvg.EmailService.Data.Entities.EmailPoolTable", "EmailPool")
                        .WithMany()
                        .HasForeignKey("EmailPoolID");

                    b.HasOne("Nvg.EmailService.Data.Entities.EmailProviderSettingsTable", "EmailProvider")
                        .WithMany()
                        .HasForeignKey("EmailProviderID");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailHistoryTable", b =>
                {
                    b.HasOne("Nvg.EmailService.Data.Entities.EmailChannelTable", "EmailChannel")
                        .WithMany()
                        .HasForeignKey("EmailChannelID");

                    b.HasOne("Nvg.EmailService.Data.Entities.EmailProviderSettingsTable", "EmailProvider")
                        .WithMany()
                        .HasForeignKey("EmailProviderID");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailProviderSettingsTable", b =>
                {
                    b.HasOne("Nvg.EmailService.Data.Entities.EmailPoolTable", "EmailPool")
                        .WithMany()
                        .HasForeignKey("EmailPoolID");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailQuotaTable", b =>
                {
                    b.HasOne("Nvg.EmailService.Data.Entities.EmailChannelTable", "EmailChannel")
                        .WithMany()
                        .HasForeignKey("EmailChannelID");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailTemplateTable", b =>
                {
                    b.HasOne("Nvg.EmailService.Data.Entities.EmailPoolTable", "EmailPool")
                        .WithMany()
                        .HasForeignKey("EmailPoolID");
                });
#pragma warning restore 612, 618
        }
    }
}
