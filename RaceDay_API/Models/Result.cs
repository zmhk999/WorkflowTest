using System;
using System.Collections.Generic;

namespace RaceDay_API.Models;

public partial class Result
{
    public string ResultsId { get; set; } = null!;

    public string FinishTimes { get; set; } = null!;

    public string FinishingPositions { get; set; } = null!;

    public string EventEnrolmentId { get; set; } = null!;

    public virtual EventEnrolment EventEnrolment { get; set; } = null!;
}
