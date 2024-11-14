using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Rza_valeria.Models;

// DbContext class for connecting to and interacting with the database.
public partial class TlS2301890RzaContext : DbContext
{
    // Default constructor, required for some operations like migrations
    public TlS2301890RzaContext()
    {
    }

    // Constructor that accepts DbContextOptions, enabling configuration when initializing the context
    public TlS2301890RzaContext(DbContextOptions<TlS2301890RzaContext> options)
        : base(options)
    {
    }

    // DbSet properties represent tables in the database
    public virtual DbSet<Attraction> Attractions { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Educationalvisit> Educationalvisits { get; set; }
    public virtual DbSet<Hotelroom> Hotelrooms { get; set; }
    public virtual DbSet<Roombooking> Roombookings { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }

    // Database configuration for the context (e.g., MySQL connection string) is typically done here
    // This has been commented out as it might be managed by dependency injection in your startup configuration
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseMySql("name=MySqlConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    // OnModelCreating configures the schema for the database by defining the structure of each entity
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set default character set and collation for MySQL database
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // Configure each entity (table) with its properties and constraints

        // Attraction entity mapping
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

        // Customer entity mapping
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");
            entity.ToTable("customers");

            // Unique constraints on specific properties
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

        // Educationalvisit entity mapping
        modelBuilder.Entity<Educationalvisit>(entity =>
        {
            entity.HasKey(e => e.MaterialiD).HasName("PRIMARY");
            entity.ToTable("educationalvisits");

            // Foreign key to Ticket entity
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

        // Hotelroom entity mapping
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

        // Roombooking entity mapping with composite key
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

            // Define relationships with Customer and Hotelroom entities
            entity.HasOne(d => d.Customer).WithMany(p => p.Roombookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roombookings_ibfk_1");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Roombookings)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roombookings_ibfk_2");
        });

        // Ticket entity mapping
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

        // Partial method to allow further customization in a separate file if needed
        OnModelCreatingPartial(modelBuilder);
    }

     //Partial method definition, can be used for further configurations in a separate partial class
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}