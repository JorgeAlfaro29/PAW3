using Microsoft.AspNetCore.Mvc;
using PAW3.Data.DTOs;

namespace PAW3.Mvc.Models;

public class HomeViewModel
{
    public string Title { get; set; } = "My App";
    public IEnumerable<PersonDTO> People { get; set; } = [];
    //public object Dog { get; set; }
}
