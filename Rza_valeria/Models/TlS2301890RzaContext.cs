using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Rza_valeria.Models;

public partial class TlS2301890RzaContext : DbContext
{
    public TlS2301890RzaContext()
    {
    }

    public TlS2301890RzaContext(DbContextOptions<TlS2301890RzaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attraction> Attractions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Educationalvisit> Educationalvisits { get; set; }

    public virtual DbSet<Hotelroom> Hotelrooms { get; set; }

    public virtual DbSet<Roombooking> Roombookings { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseMySql("name=MySqlConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Attraction>(entity =>
        {
            entity.HasKey(e => e.AttractionId).HasName("PRIMARY");

            entity.ToTable("attraction");

            entity.Property(e => e.AttractionId).HasColumnName("attractionID");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OpeningHours)
                .HasMaxLength(11)
                .HasColumnName("openingHours");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "phoneNumber").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customerID");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("lastName");
            entity.Property(e => e.LoyaltyPoints).HasColumnName("loyaltyPoints");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Postcode)
                .HasMaxLength(8)
                .HasColumnName("postcode");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Educationalvisit>(entity =>
        {
            entity.HasKey(e => e.MaterialiD).HasName("PRIMARY");

            entity.ToTable("educationalvisits");

            entity.HasIndex(e => e.TicketId, "ticketID");

            entity.Property(e => e.MaterialiD)
                .ValueGeneratedNever()
                .HasColumnName("materialiD");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.TicketId).HasColumnName("ticketID");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Educationalvisits)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("educationalvisits_ibfk_1");
        });

        modelBuilder.Entity<Hotelroom>(entity =>
        {
            entity.HasKey(e => e.RoomNumber).HasName("PRIMARY");

            entity.ToTable("hotelrooms");

            entity.Property(e => e.RoomNumber)
                .ValueGeneratedNever()
                .HasColumnName("roomNumber");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.RoomType)
                .HasMaxLength(20)
                .HasColumnName("roomType");
        });

        modelBuilder.Entity<Roombooking>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.RoomNumber, e.StartDate })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("roombookings");

            entity.HasIndex(e => e.RoomNumber, "roomNumber");

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.RoomNumber).HasColumnName("roomNumber");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.EndDate).HasColumnName("endDate");

            entity.HasOne(d => d.Customer).WithMany(p => p.Roombookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roombookings_ibfk_1");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Roombookings)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roombookings_ibfk_2");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.CustomerId, "customerID");

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("ticketID");
            entity.Property(e => e.AttractionId)
                .HasMaxLength(45)
                .HasColumnName("attractionID");
            entity.Property(e => e.BookingDate).HasColumnName("bookingDate");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TicketType)
                .HasMaxLength(50)
                .HasColumnName("ticketType");

            entity.HasOne(d => d.Customer).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("ticket_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
