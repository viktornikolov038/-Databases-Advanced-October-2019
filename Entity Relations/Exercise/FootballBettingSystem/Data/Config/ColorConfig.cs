﻿namespace P03_FootballBetting.Data.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.ColorId);

            builder.HasMany(x => x.PrimaryKitTeams)
                .WithOne(x => x.PrimaryKitColor)
                .HasForeignKey(x => x.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.SecondaryKitTeams)
                .WithOne(x => x.SecondaryKitColor)
                .HasForeignKey(x => x.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name)
                .HasMaxLength(15)
                .IsRequired(true);
        }
    }
}
