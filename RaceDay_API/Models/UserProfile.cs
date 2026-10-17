using System;
using System.Collections.Generic;

namespace RaceDay_API.Models;

public partial class UserProfile
{
    public string UserProfileId { get; set; } = null!;

    public string UserRoleId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserAge { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<EventEnrolment> EventEnrolments { get; set; } = new List<EventEnrolment>();

    public virtual UserRole UserRole { get; set; } = null!;
}
