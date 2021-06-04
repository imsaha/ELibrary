﻿// <auto-generated />
using System;
using ELibrary.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ELibrary.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210604064428_addIdentityTables2")]
    partial class addIdentityTables2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ELibrary.Domain.Models.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProhibited")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Our earliest experiences shape our lives far down the road, and What Happened to You? provides powerful scientific and emotional insights into the behavioral patterns so many of us struggle to understand.",
                            IsProhibited = false,
                            Title = "What Happened to You?: Conversations on Trauma, Resilience, and Healing"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "People unfamiliar with Scripture often assume that women play a small, secondary role in the Bible. But in fact, they were central figures in numerous Biblical tales. It was Queen Esther’s bravery at a vital point in history which saved her entire people. The Bible contains warriors like Jael, judges like Deborah, and prophets like Miriam. The first person to witness Jesus’ resurrection was Mary Magdalene, who promptly became the first Christian evangelist, eager to share the news which would change the world forever.",
                            IsProhibited = false,
                            Title = "The Women of the Bible Speak: The Wisdom of 16 Women and Their Lessons for Today"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "In The Four Agreements, bestselling author don Miguel Ruiz reveals the source of self-limiting beliefs that rob us of joy and create needless suffering. Based on ancient Toltec wisdom, The Four Agreements offer a powerful code of conduct that can rapidly transform our lives to a new experience of freedom, true happiness, and love.",
                            IsProhibited = false,
                            Title = "The Four Agreements: A Practical Guide to Personal Freedom (A Toltec Wisdom Book)"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Somewhere out beyond the edge of the universe there is a library that contains an infinite number of books, each one the story of another reality. One tells the story of your life as it is, along with another book for the other life you could have lived if you had made a different choice at any point in your life. While we all wonder how our lives might have been, what if you had the chance to go to the library and see for yourself? Would any of these other lives truly be better?",
                            IsProhibited = false,
                            Title = "The Midnight Library: A Novel"
                        });
                });

            modelBuilder.Entity("ELibrary.Domain.Models.CountryBookRentFee", b =>
                {
                    b.Property<long>("OperationCountryId")
                        .HasColumnType("bigint");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<double>("RentFee")
                        .HasColumnType("float");

                    b.HasKey("OperationCountryId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("CountryWiseBookRentFees");

                    b.HasData(
                        new
                        {
                            OperationCountryId = 1L,
                            BookId = 1L,
                            RentFee = 10.0
                        },
                        new
                        {
                            OperationCountryId = 2L,
                            BookId = 1L,
                            RentFee = 200.0
                        },
                        new
                        {
                            OperationCountryId = 1L,
                            BookId = 2L,
                            RentFee = 13.0
                        },
                        new
                        {
                            OperationCountryId = 2L,
                            BookId = 2L,
                            RentFee = 260.0
                        },
                        new
                        {
                            OperationCountryId = 1L,
                            BookId = 3L,
                            RentFee = 11.0
                        },
                        new
                        {
                            OperationCountryId = 2L,
                            BookId = 3L,
                            RentFee = 240.0
                        },
                        new
                        {
                            OperationCountryId = 1L,
                            BookId = 4L,
                            RentFee = 11.0
                        },
                        new
                        {
                            OperationCountryId = 2L,
                            BookId = 4L,
                            RentFee = 240.0
                        });
                });

            modelBuilder.Entity("ELibrary.Domain.Models.OperationCountry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LateFees")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeekDays")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationCountries");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Alias = "UAE",
                            Currency = "AED",
                            LateFees = 5.0,
                            Name = "United Arab Emirates",
                            WeekDays = "Friday,Saturday"
                        },
                        new
                        {
                            Id = 2L,
                            Alias = "IN",
                            Currency = "INR",
                            LateFees = 100.0,
                            Name = "India",
                            WeekDays = "Sunday"
                        });
                });

            modelBuilder.Entity("ELibrary.Domain.Models.OperationCountryHoliday", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("HolidayDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OccationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OperationCountryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OperationCountryId");

                    b.ToTable("OperationCountryHoliday");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.OperationCountrySupportedLanguage", b =>
                {
                    b.Property<long>("OperationCountryId")
                        .HasColumnType("bigint");

                    b.Property<long>("SupportedLanaguageId")
                        .HasColumnType("bigint");

                    b.HasKey("OperationCountryId", "SupportedLanaguageId");

                    b.HasIndex("SupportedLanaguageId");

                    b.ToTable("OperationCountrySupportedLanguage");

                    b.HasData(
                        new
                        {
                            OperationCountryId = 1L,
                            SupportedLanaguageId = 1L
                        },
                        new
                        {
                            OperationCountryId = 1L,
                            SupportedLanaguageId = 2L
                        },
                        new
                        {
                            OperationCountryId = 2L,
                            SupportedLanaguageId = 1L
                        },
                        new
                        {
                            OperationCountryId = 2L,
                            SupportedLanaguageId = 2L
                        });
                });

            modelBuilder.Entity("ELibrary.Domain.Models.Rent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ActualReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<long>("OperationCountryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ScheduledReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("OperationCountryId");

                    b.HasIndex("TenantId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.SupportedLanguage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("SupportedLanguages");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "ar-AE",
                            Name = "Arabic"
                        },
                        new
                        {
                            Id = 2L,
                            Code = "en-US",
                            Name = "English"
                        });
                });

            modelBuilder.Entity("ELibrary.Domain.Models.Tenant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MobileNumber")
                        .IsUnique()
                        .HasFilter("[MobileNumber] IS NOT NULL");

                    b.ToTable("Tenents");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ConcurrencyStamp = "3d04005e-7d68-4e22-aef0-b99749886d59",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2L,
                            ConcurrencyStamp = "f2317df2-a6a8-4fe2-97a1-d21b0a763b35",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.CountryBookRentFee", b =>
                {
                    b.HasOne("ELibrary.Domain.Models.Book", "Book")
                        .WithMany("Fees")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELibrary.Domain.Models.OperationCountry", "OperationCountry")
                        .WithMany()
                        .HasForeignKey("OperationCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("OperationCountry");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.OperationCountryHoliday", b =>
                {
                    b.HasOne("ELibrary.Domain.Models.OperationCountry", "OperationCountry")
                        .WithMany("Holidays")
                        .HasForeignKey("OperationCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperationCountry");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.OperationCountrySupportedLanguage", b =>
                {
                    b.HasOne("ELibrary.Domain.Models.OperationCountry", "OperationCountry")
                        .WithMany("SupportedLanguages")
                        .HasForeignKey("OperationCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELibrary.Domain.Models.SupportedLanguage", "SupportedLanguage")
                        .WithMany()
                        .HasForeignKey("SupportedLanaguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperationCountry");

                    b.Navigation("SupportedLanguage");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.Rent", b =>
                {
                    b.HasOne("ELibrary.Domain.Models.Book", "Book")
                        .WithMany("Rents")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELibrary.Domain.Models.OperationCountry", "OperationCountry")
                        .WithMany()
                        .HasForeignKey("OperationCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ELibrary.Domain.Models.Tenant", "Tenant")
                        .WithMany("Rents")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("OperationCountry");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<long>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<long>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<long>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<long>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<long>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<long>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ELibrary.Domain.Models.Book", b =>
                {
                    b.Navigation("Fees");

                    b.Navigation("Rents");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.OperationCountry", b =>
                {
                    b.Navigation("Holidays");

                    b.Navigation("SupportedLanguages");
                });

            modelBuilder.Entity("ELibrary.Domain.Models.Tenant", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}