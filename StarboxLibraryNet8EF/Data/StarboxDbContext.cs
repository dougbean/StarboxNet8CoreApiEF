using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StarboxLibraryNet8EF.Models;

namespace StarboxLibraryNet8EF.Data;

public partial class StarboxDbContext : DbContext
{
    public StarboxDbContext()
    {
    }

    public StarboxDbContext(DbContextOptions<StarboxDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<DrinkIngredient> DrinkIngredients { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {            
            optionsBuilder.UseSqlServer("");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drinks__3214EC074F336AE1");

            entity.HasIndex(e => e.Name, "UQ_DrinkName").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<DrinkIngredient>(entity =>
        {
            entity.HasKey(e => new { e.DrinkId, e.IngredientId }).HasName("PK__DrinkIng__7B7E38CD60FE8A81");

            entity.HasOne(d => d.Drink).WithMany(p => p.DrinkIngredients)
                .HasForeignKey(d => d.DrinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drink");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.DrinkIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ingredient");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC07BA4E4C08");

            entity.HasIndex(e => e.Name, "UQ_IngredientName").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.UnitCost).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
