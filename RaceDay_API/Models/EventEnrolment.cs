using System;
using System.Collections.Generic;

namespace RaceDay_API.Models;

public partial class EventEnrolment
{
    public string EventEnrolmentId { get; set; } = null!;

    public string EventsId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string UserProfileId { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Event Events { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual UserProfile UserProfile { get; set; } = null!;
}
