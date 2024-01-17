using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonAddRequest
{
    //Properties for PersonAddRequest
    [Required(ErrorMessage = "PersonName is required")]
    public string? PersonName { get; set;}
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set;}
    public Genderoptions? Gender { get; set;}
    public DateTime? DateOfBirth { get; set;}
    public Guid CountryId { get; set;}
    public string? Address { get; set;}
    public bool ReceiveNewsLetter { get; set;}

    /// <summary>
    /// Convert PersonAddRequest to Person
    /// </summary>
    /// <returns>Person Class and store in database</returns>
    public Person ToPerson()
    {
        return new Person()
        {
            PersonName = PersonName,
            Email = Email,
            Gender = Gender.ToString(),
            DateOfBirth = DateOfBirth,
            CountryId = CountryId,
            Address = Address,
            ReceiveNewsLetters = ReceiveNewsLetter
        };
    }
}