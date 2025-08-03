using System;
using System.Collections.Generic;

namespace storeapi.TempModels;

public partial class Account
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
