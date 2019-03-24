﻿// <auto-generated />
using BoardGamesServiceApi.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoardGamesServiceApi.Migrations
{
    [DbContext(typeof(BoardGameContext))]
    partial class BoardGameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoardGamesServiceApi.DAL.Entities.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GameID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description");

                    b.Property<decimal>("Price")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.ToTable("BoardGame");

                    b.HasData(
                        new { Id = 1, Description = "Test game 1", Price = 11m },
                        new { Id = 2, Description = "Test game 2", Price = 12m },
                        new { Id = 3, Description = "Test game 3", Price = 13m }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
