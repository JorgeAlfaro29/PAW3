using System;
using System.Collections.Generic;

namespace PAW3.Data.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Gender { get; set; }
}
