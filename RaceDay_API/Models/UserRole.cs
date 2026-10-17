using System;
using System.Collections.Generic;

namespace RaceDay_API.Models;

public partial class UserRole
{
    public string UserRoleId { get; set; } = null!;

    public string UserRoleName { get; set; } = null!;

    public string UserFirstName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
