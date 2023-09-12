using System;
using System.Collections.Generic;

namespace PurrfectpawsApi.Models;

public partial class TProduct
{
    public int ProductId { get; set; }

    public int ProductDetailsId { get; set; }

    public int? SizeId { get; set; }

    public int? LeadLengthId { get; set; }

    public int VariationId { get; set; }

    public int ProductQuantity { get; set; }
}
