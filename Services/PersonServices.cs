using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using Xunit.Abstractions;

namespace Services;

public class PersonServices : IPersonService
{
    private readonly List<Person> _persons;
    private readonly ICountriesService _countries;


    public PersonServices(bool initializePersons = true)
    {
        _persons = new List<Person>();
        _countries = new  CountriesServices();

        if (initializePersons)
        {

            
            _persons.AddRange(new List<Person>()
            {
                new Person()
                {
                    PersonId = Guid.Parse("642441B0-BDED-4967-B8A3-C9E28462A8E1"),
                    PersonName = "Augy",
                    Address = "28875 Lien Center",
                    DateOfBirth = DateTime.Parse("1996-11-03"),
                    Email = "akimblen0@walmart.com",
                    Gender = "Male",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("94065F9A-2F61-43E2-8AD2-66C4359B6039"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("642441B0-BDED-4967-B8A3-C9E28462A8E1"),
                    PersonName = "Ruy",
                    Address = "8082 Sunfield Crossing",
                    DateOfBirth = DateTime.Parse("1990-12-31"),
                    Email = "rhart1@foxnews.com",
                    Gender = "Female",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("FBC6553F-DB1D-4DA9-916A-1B72CFB9FA37"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("0FD965D7-7DFA-47A1-948E-E99D4F816DAD"),
                    PersonName = "Walther",
                    Address = "478 Badeau Point",
                    DateOfBirth = DateTime.Parse("1996-09-21"),
                    Email = "weastmond2@barnesandnoble.com",
                    Gender = "Male",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("4BD9E793-D478-4972-A0CA-C1B9D7D8A215"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("3692F875-00F9-42B1-80FD-401A6A1CC5B9"),
                    PersonName = "Lionel",
                    Address = "2556 Cody Alley",
                    DateOfBirth = DateTime.Parse("1995-08-31"),
                    Email = "lswynfen3@timesonline.co.uk",
                    Gender = "Female",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("4BD9E793-D478-4972-A0CA-C1B9D7D8A215"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("DB546D44-72AF-409C-A03C-6392E08BE45D"),
                    PersonName = "Bale",
                    Address = "191 Hanover Way",
                    DateOfBirth = DateTime.Parse("1990-09-12"),
                    Email = "bgudgeon4@gizmodo.com",
                    Gender = "Female",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("FBC6553F-DB1D-4DA9-916A-1B72CFB9FA37"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("21D24197-B821-4641-8C2D-2EDFE83DFCEF"),
                    PersonName = "Cletis",
                    Address = "0678 Waxwing Point",
                    DateOfBirth = DateTime.Parse("1997-07-05"),
                    Email = "chazeldean5@pbs.org",
                    Gender = "Male",
                    ReceiveNewsLetters = true,
                    CountryId = Guid.Parse("FBC6553F-DB1D-4DA9-916A-1B72CFB9FA37"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("1E83D1CB-9EBB-43AB-9858-59538039ED07"),
                    PersonName = "Rowney",
                    Address = "458 Mariners Cove Circle",
                    DateOfBirth = DateTime.Parse("1995-08-23"),
                    Email = "rdanihelka6@ow.ly",
                    Gender = "Female",
                    ReceiveNewsLetters =false,
                    CountryId = Guid.Parse("4BD9E793-D478-4972-A0CA-C1B9D7D8A215"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("1E80C2BB-361F-47BA-BE96-9EE67089F911"),
                    PersonName = "Bendix",
                    Address = "18186 Dunning Road",
                    DateOfBirth = DateTime.Parse("1994-08-14"),
                    Email = "brourke7@narod.ru",
                    Gender = "Female",
                    ReceiveNewsLetters = false ,
                    CountryId = Guid.Parse("1B05FE76-71D5-4F4F-89BC-231527A13E85"),
                }, 
                new Person()
                {
                    PersonId = Guid.Parse("DC91B7D2-A3E9-46C2-85F3-D09616889962"),
                    PersonName = "Edwin",
                    Address = "466 Roth Way",
                    DateOfBirth = DateTime.Parse("1991-05-10"),
                    Email = "ebeckson8@biglobe.ne.jp",
                    Gender = "Male",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("1B05FE76-71D5-4F4F-89BC-231527A13E85"),
                },
                new Person()
                {
                    PersonId = Guid.Parse("39A7E0DA-1101-47D0-A72C-1124EC80FAFB"),
                    PersonName = "Bart",
                    Address = "48 Lakeland Road",
                    DateOfBirth = DateTime.Parse("1995-04-15"),
                    Email = "bbussey9@purevolume.com",
                    Gender = "Female",
                    ReceiveNewsLetters = false,
                    CountryId = Guid.Parse("3F48DF3C-555E-4D60-918F-F7A53651D431"),
                },
            });
        }
        
    }

    private PersonResponse ToPersonintoPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.CountryName = _countries.GetCountryByCountryId(person.CountryId)?.CountryName;
        return personResponse;
    }
    public PersonResponse AddPerson(PersonAddRequest personAddRequest)
    {
        //check if PersonAddRequest is null
        if (personAddRequest == null)
        {
            throw new ArgumentNullException(nameof(personAddRequest));
        }
        //check if PersonName in PersonAddRequest is null
        if (string.IsNullOrEmpty(personAddRequest.PersonName))
        {
            throw new ArgumentException("PersonName can't be blank");
        }
        
        //Model Validations
        ValidationHelpers.ModelValidation(personAddRequest);
        
        
        //Convert PersonAddRequest to Person
        Person person = personAddRequest.ToPerson();
        //Generate a personID
        person.PersonId = Guid.NewGuid();
        //Convert Person to PersonResponse
        _persons.Add(person);
        
         
        //Convert Person Class to PersonResponse
        return ToPersonintoPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
       return _persons.Select(temp => ToPersonintoPersonResponse(temp)).ToList();
    }

    public PersonResponse? GetPersonByPersonId(Guid? PersonId)
    {
        if (PersonId == null) return null;

        Person? person_with_id = _persons.FirstOrDefault(temp => temp.PersonId == PersonId);
       
        if (person_with_id == null) return null;
 
        return ToPersonintoPersonResponse(person_with_id);
    }

    public List<PersonResponse> getFilteredPersonResponses(string searchBy, string? searchString)
    {
        List<PersonResponse> allPersons = GetAllPersons();
        List<PersonResponse> matchingPersons = allPersons;

        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
        {
            return matchingPersons;
        }

        switch (searchBy)
        {
            case nameof(PersonResponse.PersonName):
                matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PersonName)
                        ? temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            case nameof(PersonResponse.Email):
                matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email)
                        ? temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            case nameof(PersonResponse.Address):
                matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address)
                        ? temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            default: matchingPersons = allPersons;
                break; 
        }
        
        return matchingPersons;
    }

    public List<PersonResponse> getSortedPersons(List<PersonResponse> allpersons, string sortby, SortOrderOptions sortOrder)
    {
        if (string.IsNullOrEmpty(sortby))
        {
            return allpersons;
        }

        List<PersonResponse> sortedPersons = (sortby, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.Asc) => allpersons
                    .OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.PersonName), SortOrderOptions.Desc) => allpersons
                    .OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.Asc) => allpersons
                    .OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.Desc) => allpersons
                    .OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                _ => allpersons
            };

        return sortedPersons;
    }

    public PersonResponse getUpdatedPerson(PersonUpdateRequest? personUpdateRequest)
    {
        //check if PersonUpdateRequest is null
        if (personUpdateRequest == null)
        {
            throw new ArgumentNullException();
        }
        //Model Validations
        ValidationHelpers.ModelValidation(personUpdateRequest);
        
        //Get the matching Person from updatedPerson
        Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonId == personUpdateRequest.PersonId);

        if (matchingPerson == null)
        {
            throw new ArgumentException("Person not found");
        }
        
        //Updating Person
        
        matchingPerson.PersonName = personUpdateRequest.PersonName;
        matchingPerson.CountryId = personUpdateRequest.CountryId;
        matchingPerson.Email = personUpdateRequest.Email;
        matchingPerson.Address = personUpdateRequest.Address;
        matchingPerson.CountryId = personUpdateRequest.CountryId;
        matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetter;
        matchingPerson.Gender = personUpdateRequest.Gender.ToString();
        matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;

        return ToPersonintoPersonResponse(matchingPerson);
    }

    public bool DeletePerson(Guid? personId)
    {
        if (personId == null)
        {
            throw new ArgumentNullException();
        }
        
        Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        if (matchingPerson == null)
        {
            return false;
        }
        
        _persons.RemoveAll(temp => temp.PersonId == personId);
        
        return true;
    }
}

