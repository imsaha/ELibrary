using ELibrary.Application;
using ELibrary.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ELibrary.Infrastructure
{
    public class ApplicationDbContext :DbContext,  IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<Tenant> Tenents { get; set; }

        public DbSet<Book> Books { get; set; }


        public DbSet<OperationCountry> OperationCountries { get; set; }
        public DbSet<CountryBookRentFee> CountryWiseBookRentFees { get; set; }
        public DbSet<SupportedLanguage> SupportedLanguages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = "user", NormalizedId = "USER" },
                new Role { Id = "admin", NormalizedId = "ADMIN" });


            modelBuilder.Entity<OperationCountry>()
                .HasData(
                new OperationCountry
                {
                    Id = 1,
                    Name = "United Arab Emirates",
                    Alias = "UAE",
                    Currency = "AED",
                    LateFees = 5.00d,
                    WeekDays = new[] { DayOfWeek.Friday, DayOfWeek.Saturday },
                },
                new OperationCountry
                {
                    Id = 2,
                    Name = "India",
                    Alias = "IN",
                    Currency = "INR",
                    LateFees = 100.00d,
                    WeekDays = new[] { DayOfWeek.Sunday },
                });

            modelBuilder.Entity<Book>()
                .HasData(
                new Book
                {
                    Id = 1,
                    Title = "What Happened to You?: Conversations on Trauma, Resilience, and Healing",
                    Description = "Our earliest experiences shape our lives far down the road, and What Happened to You? provides powerful scientific and emotional insights into the behavioral patterns so many of us struggle to understand.",
                    IsProhibited = false,
                },
                new Book
                {
                    Id = 2,
                    Title = "The Women of the Bible Speak: The Wisdom of 16 Women and Their Lessons for Today",
                    Description = "People unfamiliar with Scripture often assume that women play a small, secondary role in the Bible. But in fact, they were central figures in numerous Biblical tales. It was Queen Esther’s bravery at a vital point in history which saved her entire people. The Bible contains warriors like Jael, judges like Deborah, and prophets like Miriam. The first person to witness Jesus’ resurrection was Mary Magdalene, who promptly became the first Christian evangelist, eager to share the news which would change the world forever.",
                    IsProhibited = false,
                },
                new Book
                {
                    Id = 3,
                    Title = "The Four Agreements: A Practical Guide to Personal Freedom (A Toltec Wisdom Book)",
                    Description = "In The Four Agreements, bestselling author don Miguel Ruiz reveals the source of self-limiting beliefs that rob us of joy and create needless suffering. Based on ancient Toltec wisdom, The Four Agreements offer a powerful code of conduct that can rapidly transform our lives to a new experience of freedom, true happiness, and love.",
                    IsProhibited = false,
                },
                new Book
                {
                    Id = 4,
                    Title = "The Midnight Library: A Novel",
                    Description = "Somewhere out beyond the edge of the universe there is a library that contains an infinite number of books, each one the story of another reality. One tells the story of your life as it is, along with another book for the other life you could have lived if you had made a different choice at any point in your life. While we all wonder how our lives might have been, what if you had the chance to go to the library and see for yourself? Would any of these other lives truly be better?",
                    IsProhibited = false,
                });


            modelBuilder.Entity<CountryBookRentFee>()
                .HasData(
                new CountryBookRentFee
                {
                    BookId = 1,
                    OperationCountryId = 1,
                    RentFee = 10d
                },
                new CountryBookRentFee
                {
                    BookId = 1,
                    OperationCountryId = 2,
                    RentFee = 200d
                },

                new CountryBookRentFee
                {
                    BookId = 2,
                    OperationCountryId = 1,
                    RentFee = 13d
                },
                new CountryBookRentFee
                {
                    BookId = 2,
                    OperationCountryId = 2,
                    RentFee = 260d
                },

                new CountryBookRentFee
                {
                    BookId = 3,
                    OperationCountryId = 1,
                    RentFee = 11d
                },
                new CountryBookRentFee
                {
                    BookId = 3,
                    OperationCountryId = 2,
                    RentFee = 240d
                },

                new CountryBookRentFee
                {
                    BookId = 4,
                    OperationCountryId = 1,
                    RentFee = 11d
                },
                new CountryBookRentFee
                {
                    BookId = 4,
                    OperationCountryId = 2,
                    RentFee = 240d
                });


            modelBuilder.Entity<SupportedLanguage>()
                .HasData(
                new SupportedLanguage()
                {
                    Id = 1,
                    Code = "ar-AE",
                    Name = "Arabic"
                },
                new SupportedLanguage()
                {
                    Id = 2,
                    Code = "en-US",
                    Name = "English"
                });


            modelBuilder.Entity<OperationCountrySupportedLanguage>()
                .HasData(
                new OperationCountrySupportedLanguage()
                {
                    OperationCountryId = 1,
                    SupportedLanaguageId = 1,
                },
                new OperationCountrySupportedLanguage()
                {
                    OperationCountryId = 1,
                    SupportedLanaguageId = 2,
                },
                new OperationCountrySupportedLanguage()
                {
                    OperationCountryId = 2,
                    SupportedLanaguageId = 1,
                },
                new OperationCountrySupportedLanguage()
                {
                    OperationCountryId = 2,
                    SupportedLanaguageId = 2,
                });
        }
    }
}
