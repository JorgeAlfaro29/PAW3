using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PAW3.Data.Models;

public partial class Category
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
