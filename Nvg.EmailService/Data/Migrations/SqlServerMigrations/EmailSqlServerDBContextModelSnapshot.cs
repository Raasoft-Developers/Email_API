﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nvg.EmailService.Data;

namespace Nvg.EmailService.data.Migrations.SqlServerMigrations
{
    [DbContext(typeof(EmailSqlServerDBContext))]
    partial class EmailSqlServerDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailChannelTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailPoolID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailProviderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("EmailPoolID");

                    b.HasIndex("EmailProviderID");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasFilter("[Key] IS NOT NULL");

                    b.ToTable("EmailChannel");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailErrorLogTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailChannelID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ErrorType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recipients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmailChannelID");

                    b.ToTable("EmailErrorLogTable");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailHistoryTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<string>("EmailChannelID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailProviderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MessageSent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recipients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateVariant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmailChannelID");

                    b.HasIndex("EmailProviderID");

                    b.ToTable("EmailHistory");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailPoolTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("EmailPool");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailProviderSettingsTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Configuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailPoolID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmailPoolID");

                    b.HasIndex("Name", "EmailPoolID")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [EmailPoolID] IS NOT NULL");

                    b.ToTable("EmailProviderSettings");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailQuotaTable", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailChannelID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MonthlyConsumption")
                        .HasColumnType("int");

                    b.Property<int>("MonthlyQuota")
                        .HasColumnType("int");

                    b.Property<int>("TotalConsumption")
                        .HasColumnType("int");

                    b.Property<int>("TotalQuota")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmailChannelID");

                    b.ToTable("EmailQuota");
                });

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailTemplateTable", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailPoolID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Variant")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmailPoolID");

                    b.HasIndex("Name", "EmailPoolID")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL AND [EmailPoolID] IS NOT NULL");

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

            modelBuilder.Entity("Nvg.EmailService.Data.Entities.EmailErrorLogTable", b =>
                {
                    b.HasOne("Nvg.EmailService.Data.Entities.EmailChannelTable", "EmailChannel")
                        .WithMany()
                        .HasForeignKey("EmailChannelID");
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
