using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;

public partial class Attraction
{
    public int AttractionId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? OpeningHours { get; set; }
}
