using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final.Models;

public partial class Branch
{
    public int Bid { get; set; }

    [Required(ErrorMessage = "The Phone number is required.")]
    [Range(0000, 99999, ErrorMessage = "The Phone number must be a valid 10-digit number.")]
    public int? Phone { get; set; }

    [Required(ErrorMessage = "The Branch address is required.")]
    [StringLength(200, ErrorMessage = "The Branch address cannot exceed 200 characters.")]
    public string? Baddress { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
