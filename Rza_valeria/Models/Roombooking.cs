//Import necessary namespaces for basic functionalities and collections.
using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;
//Defines the `Roombooking` class, representing a booking made by a customer for a hotel room
public partial class Roombooking
{
    public int CustomerId { get; set; } // ID of the customer making the booking.

    public int RoomNumber { get; set; }//Number of the hotel room being booked.

    public DateOnly StartDate { get; set; }//Start date of the room booking.

    public DateOnly? EndDate { get; set; }//Optional end date for the booking; nullable for open-ended bookings.
   
    // Navigation property representing the customer who made the booking.
    public virtual Customer Customer { get; set; } = null!;


    // Navigation property to the `Hotelroom` associated with this booking, mapped by `RoomNumber`.
    public virtual Hotelroom RoomNumberNavigation { get; set; } = null!;
}
