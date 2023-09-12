using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurrfectpawsApi.Models;

public partial class Login
{
    public string? Email { get; set; } = null!;

    public string? Password { get; set; } = null!;
}
