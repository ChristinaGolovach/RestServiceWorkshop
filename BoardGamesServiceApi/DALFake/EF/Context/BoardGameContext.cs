using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesServiceApi.DAL.EF.Configurations;
using BoardGamesServiceApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesServiceApi.DAL.EF.Context
{
    public class BoardGameContext : DbContext
    {
        public DbSet<BoardGame> BoardGames { get; set; }

        public BoardGameContext(DbContextOptions<BoardGameContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardGameConfiguration());

            modelBuilder.Entity<BoardGame>().HasData(
                new BoardGame[]
                {
                    new BoardGame {Id = 1, Description = "Test game 1", Price = 11M},
                    new BoardGame {Id = 2, Description = "Test game 2", Price = 12M},
                    new BoardGame {Id = 3, Description = "Test game 3", Price = 13M}
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
