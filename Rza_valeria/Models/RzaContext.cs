﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Rza_valeria.Models;

public partial class RzaContext : DbContext
{
    public RzaContext()
    {
    }

    public RzaContext(DbContextOptions<RzaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attraction> Attractions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Ticketbooking> Ticketbookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=offlineConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Attraction>(entity =>
        {
            entity.HasKey(e => e.AttractionId).HasName("PRIMARY");

            entity.ToTable("attraction");

            entity.Property(e => e.AttractionId).HasColumnName("attractionId");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "phoneNumber").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerID");
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

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.AttractionId, "fk1_idx");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.AttractionId).HasColumnName("attractionId");
            //entity.Property(e => e.Date).HasColumnName("date");

            entity.HasOne(d => d.Attraction).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AttractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_fk1");
        });

        modelBuilder.Entity<Ticketbooking>(entity =>
        {
            //entity.HasKey(e => new { e.CustomerId, e.TicketId })
            //    .HasName("PRIMARY")
            //    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ticketbooking");

            //entity.HasIndex(e => e.TicketId, "fk2_idx");

            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            //entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.DateBooked).HasColumnName("dateBooked");

            entity.HasOne(d => d.Customer).WithMany(p => p.Ticketbookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticketBooking_fk1");

            //entity.HasOne(d => d.Ticket).WithMany(p => p.Ticketbookings)
            //    .HasForeignKey(d => d.TicketId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("ticketBooking_fk2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
