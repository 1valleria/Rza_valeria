using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? TicketType { get; set; }

    public float? Price { get; set; }

    public DateOnly? BookingDate { get; set; }

    public int? CustomerId { get; set; }

    public string AttractionId { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Educationalvisit> Educationalvisits { get; set; } = new List<Educationalvisit>();
}
