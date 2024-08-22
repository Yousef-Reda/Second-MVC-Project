using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final.Models;

public partial class Medicine
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Medicine name is required.")]
    [StringLength(100, ErrorMessage = "The Medicine name cannot exceed 100 characters.")]
    public string? Mname { get; set; }

    [Required(ErrorMessage = "The Ingredient is required.")]
    [StringLength(500, ErrorMessage = "The Ingredient cannot exceed 500 characters.")]
    public string? Ingredient { get; set; }

    [Required(ErrorMessage = "The Price is required.")]
    [Range(0, 1000, ErrorMessage = "The Price must be between 0 and 1000.")]
    public int? Price { get; set; }

    public int? Bid { get; set; }

    public virtual Branch? BidNavigation { get; set; }
}
