using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PAW3.Data.Models;

public partial class Component
{

    public decimal Id { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;
}
