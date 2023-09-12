using System;
using System.Collections.Generic;

namespace PurrfectpawsApi.Models;

public partial class MOrderStatus
{
    public int OrderStatusId { get; set; }

    public string Status { get; set; } = null!;
}
