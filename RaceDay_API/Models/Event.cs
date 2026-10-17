using System;
using System.Collections.Generic;

namespace RaceDay_API.Models;

public partial class Event
{
    public string EventsId { get; set; } = null!;

    public string EventName { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public string EventLocation { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public string EventDistance { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<EventEnrolment> EventEnrolments { get; set; } = new List<EventEnrolment>();
}
