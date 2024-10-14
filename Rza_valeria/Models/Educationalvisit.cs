using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;

public partial class Educationalvisit
{
    public int MaterialiD { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Category { get; set; }

    public int? TicketId { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
