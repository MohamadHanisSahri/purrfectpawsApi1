using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurrfectpawsApi.Models;

public partial class TCart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }
    //public IEnumerable<object> TCarts { get; internal set; }
}

public class TCartPostDto
{
    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

}
