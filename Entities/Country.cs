using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Country
{
    /// <summary>
    /// Entities class for storing country data 
    /// </summary>
    [Key]
    public Guid CountryId { get; set; } 
    public string? CountryName { get; set; }
}