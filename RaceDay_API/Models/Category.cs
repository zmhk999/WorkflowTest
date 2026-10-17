using System;
using System.Collections.Generic;

namespace RaceDay_API.Models;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string EventsId { get; set; } = null!;

    public string AgeCategory { get; set; } = null!;

    public string DistanceCategory { get; set; } = null!;

    public virtual ICollection<EventEnrolment> EventEnrolments { get; set; } = new List<EventEnrolment>();

    public virtual Event Events { get; set; } = null!;
}
