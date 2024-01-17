using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Person
{
    [Key]
    public Guid PersonId { get; set; }
    [StringLength(40)]
    public String? PersonName { get; set; }
    [StringLength(40)]
    public String? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    [StringLength(10)]
    public String? Gender { get; set; }
    public Guid? CountryId { get; set; }
    [StringLength(200)]
    public String? Address { get; set; } 
    public bool ReceiveNewsLetters { get; set; }
}