using System;
using System.Collections.Generic;

namespace PurrfectpawsApi.Models;

public partial class TOrder
{
    public int OrderId { get; set; }

    public int OrderStatusId { get; set; }

    public int ProductId { get; set; }

    public int ShippingAddressId { get; set; }

    public int BillingAddressId { get; set; }

    public int OrderMasterId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}
