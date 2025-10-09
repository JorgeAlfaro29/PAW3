using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PAW3.Data.Models;

public partial class Component
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public decimal Id { get; set; }




    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;
}
