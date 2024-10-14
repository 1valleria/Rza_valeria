using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;

public partial class Hotelroom
{
    public int RoomNumber { get; set; }

    public int? Capacity { get; set; }

    public string? RoomType { get; set; }

    public float? PricePerNight { get; set; }

    public int? Floor { get; set; }

    public virtual ICollection<Roombooking> Roombookings { get; set; } = new List<Roombooking>();
}
