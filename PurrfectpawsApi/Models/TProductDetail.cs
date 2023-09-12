using System;
using System.Collections.Generic;

namespace PurrfectpawsApi.Models;

public partial class TProductDetail
{
    public int ProductDetailsId { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal ProductPrice { get; set; }

    public decimal? ProductCost { get; set; }

    public decimal? ProductRevenue { get; set; }

    public decimal? ProductProfit { get; set; }
}
