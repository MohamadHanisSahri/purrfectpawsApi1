using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurrfectpawsApi.Models;

public partial class TShippingAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShippingAddressId { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public TUser User { get; set; } = null!;

    public string Street1 { get; set; } = null!;

    public string? Street2 { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public int Postcode { get; set; }

}

public class ShippingAddress
{
    public string Street1 { get; set; }

    public string? Street2 { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public int Postcode { get; set; }
}