﻿using System;
using System.Collections.Generic;

namespace Rza_valeria.Models;

public partial class Roombooking
{
    public int CustomerId { get; set; }

    public int RoomNumber { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Hotelroom RoomNumberNavigation { get; set; } = null!;
}