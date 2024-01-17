using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

public class PersonResponse
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public String? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public bool ReceiveNewsLetter { get; set; }
    public double? Age { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj.GetType() != typeof(PersonResponse))
        {
            return false;
        }

        PersonResponse person = (PersonResponse)obj;
        
        return PersonId == person.PersonId && PersonName == person.PersonName && Email == person.Email && DateOfBirth == person.DateOfBirth && Address == person.Address && CountryId == person.CountryId && CountryName == person.CountryName && ReceiveNewsLetter == person.ReceiveNewsLetter;
    }
    
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"PersonId: {PersonId}, PersonName: {PersonName}, Email: {Email}, DateOfBirth: {DateOfBirth?.ToString("MM/dd/yyyy")}, Address: {Address}, CountryId: {CountryId}, CountryName: {CountryName}, ReceiveNewsLetter: {ReceiveNewsLetter}";
    }

   public PersonUpdateRequest ToPersonUpdateRequest()
   {
       return new PersonUpdateRequest()
       {
           PersonId = PersonId,
           PersonName = PersonName,
           Email = Email,
           Gender = (Genderoptions)Enum.Parse(typeof(Genderoptions), Gender, true),
           DateOfBirth = DateOfBirth, 
           CountryId = CountryId,
           Address = Address,
           ReceiveNewsLetter = ReceiveNewsLetter 
       };
   }
}

public static class PersonExtensions
{

    /// <summary>
    /// Extension Method for Person class converts Person into PersonResponse and supply to Controller
    /// </summary>
    /// <param name="person">Person class</param>
    /// <returns>Returns PersonRespons by converting providing Person Class</returns>
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse()
        {
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            Address = person.Address,
            CountryId = person.CountryId,
            PersonId = person.PersonId,
            ReceiveNewsLetter = person.ReceiveNewsLetters,
            Gender = person.Gender,
            Age = (person.DateOfBirth != null)
                ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25)
                : null
        };
    }
}