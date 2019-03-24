using BoardGamesServiceApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGamesServiceApi.DAL.EF.Configurations
{
    public class BoardGameConfiguration : IEntityTypeConfiguration<BoardGame>
    {
        public void Configure(EntityTypeBuilder<BoardGame> builder)
        {
            builder.ToTable("BoardGame").HasKey(item => item.Id);
            builder.Property(game => game.Id).HasColumnName("GameID").IsRequired();
            builder.Property(game => game.Description).HasColumnName("Description").IsRequired();
            builder.Property(game => game.Price).HasColumnName("Price").IsRequired();
        }
    }
}
