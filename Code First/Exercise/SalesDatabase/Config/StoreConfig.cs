﻿namespace P03_SalesDatabase.Config
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;

    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(x => x.StoreId);

            builder.HasMany(x => x.Sales)
                .WithOne(x => x.Store)
                .HasForeignKey(x => x.StoreId);

            builder.Property(x => x.Name)
                .HasMaxLength(80)
                .IsUnicode(true);
        }
    }
}
