// Importing necessary namespaces for basic C# functionality and collections.
using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;

// Defines the `Customer` class, representing a customer in the application.
public partial class Customer
{
    public int CustomerId { get; set; }               // Unique identifier for each customer.

    public string? Username { get; set; }             // Username of the customer; nullable.

    public string? Password { get; set; }             // Hashed password of the customer; nullable.

    public string? FirstName { get; set; }            // First name of the customer; nullable.

    public string? LastName { get; set; }             // Last name of the customer; nullable.

    public string? Email { get; set; }                // Email address of the customer; nullable.

    public string? Postcode { get; set; }             // Postal code of the customer's address; nullable.

    public string? PhoneNumber { get; set; }          // Phone number of the customer; nullable.

    public DateOnly? DateOfBirth { get; set; }        // Date of birth of the customer; nullable.

    public int? LoyaltyPoints { get; set; }           // Loyalty points associated with the customer; nullable.

    // Navigation properties to related entities.
    public virtual ICollection<Roombooking> Roombookings { get; set; } = new List<Roombooking>();
    // Collection of `Roombooking` objects related to this customer.

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    // Collection of `Ticket` objects related to this customer.
}