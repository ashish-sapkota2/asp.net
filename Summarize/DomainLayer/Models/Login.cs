using System;
using System.Collections.Generic;

namespace DomainLayer.Models;

public partial class Login
{
    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? PasswordSalt { get; set; }

    public int Id { get; set; }
}
