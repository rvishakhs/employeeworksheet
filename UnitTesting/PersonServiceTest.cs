using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace UnitTesting;

public class PersonServiceTest
{
    private readonly IPersonService _personService;
    private readonly ICountriesService _countriesService;
    private readonly ITestOutputHelper _testOutputHelper;
    
    //Constructor created for PersonServiceTest
    public PersonServiceTest(ITestOutputHelper testOutputHelper)
    {
        _personService = new PersonServices();
        _countriesService = new CountriesServices(false);
        _testOutputHelper = testOutputHelper;
    }

    #region AddPerson UnitTest
    //Testcase - 1
    //When PersonAddrequest is null then the response should be null.
    [Fact]
    public void AddPerson_NullRequest()
    {
        //Arrange
        PersonAddRequest personAddRequest = null;
        
        //Act
        Assert.Throws<ArgumentNullException>(() =>
        {
            _personService.AddPerson(personAddRequest);
        });
    }
    
    //Testcase - 2
    //When PersonAddrequest provide person name as null then the return should Argument Exception.
    [Fact]
    public void AddPerson_NullPersonName()
    {
        //Arrange 
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = null
        };
        
        //Act
        Assert.Throws<ArgumentException>(() =>
        {
            _personService.AddPerson(personAddRequest);
        });
    }
    
    //Testcase - 3
    //When PersonAddrequest provided with valid data then the response should enter the data into person class and 
    //return PersonResponse class with Generated PersonID.
    [Fact]
    public void AddPerson_ValidRequest()
    {
        //Arrange 
        
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Canada"
        };
        CountryResponse countryResponse_after_adding = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "John",
            Email = "John@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding.CountryId,
            Address = "USA",
            DateOfBirth = DateTime.Parse("2000-01-01"),
            ReceiveNewsLetter = true
        };
        
        //Act
        PersonResponse personresponse_after_adding =  _personService.AddPerson(personAddRequest);
        List<PersonResponse> personList =  _personService.GetAllPersons();
        _testOutputHelper.WriteLine("Expected:");
        _testOutputHelper.WriteLine(personresponse_after_adding.ToString());
        
        //Assert
        Assert.True(personresponse_after_adding.PersonId != Guid.Empty);
        Assert.Contains(personresponse_after_adding, personList);
    }
    
    #endregion

    #region Getpersonbyid UnitTest

    //Testcase - 1 
    //When GetPersonbyId Method is called and supply null value then the method should return null
    [Fact]
    public void GetPersonById_NullCase()
    {
        //Arrange
        Guid? PersonId = null;
        
        //Act
        PersonResponse? personResponse_after_adding = _personService.GetPersonByPersonId(PersonId);
        
        //Assert
        Assert.Null(personResponse_after_adding);
    }
    
    //Testcase - 2
    //When GetPersonById supplied with valid data and the method should return the Person related to that id
    [Fact]
    public void GetPersonById_ValidCase()
    {
        //Arrange  (Country and PersonAddrequest)
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Canada"
        };
        CountryResponse countryResponse_after_adding = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "John",
            Email = "John@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding.CountryId,
            Address = "USA",
            DateOfBirth = DateTime.Parse("2000-01-01"),
            ReceiveNewsLetter = true,
        };

        //Act
        PersonResponse personResponse_after_adding = _personService.AddPerson(personAddRequest);
        PersonResponse? person_by_personId = _personService.GetPersonByPersonId(personResponse_after_adding.PersonId);
        
        //Assert
        Assert.Equal(personResponse_after_adding, person_by_personId);
        
    } 

    #endregion

    #region GetAllPersons UnitTest
    //Testcase - 1
    //When GetAllPersons method called and list is empty then the response should be empty list.
    [Fact]
    public void GetAllPersons_EmptyList()
    {
        //Act
        List<PersonResponse> getAllPersons = _personService.GetAllPersons();
        
        //Assert
        Assert.Empty(getAllPersons);
    }
    
    //Testcase - 2
    //When GetAllPersons method called and supply some persons and method should return the supplied persons
    [Fact]
    public void GetAllPersons_AddFewPersons()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Italy"
        };
        
        CountryResponse countryResponse_after_adding1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse_after_adding2 = _countriesService.AddCountry(countryAddRequest2);

        PersonAddRequest person1 = new PersonAddRequest()
        {
            PersonName = "Visakh",
            Email = "Visakh@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding1.CountryId,
            Address = "Uk",
            DateOfBirth = DateTime.Parse("1996-08-09"),
            ReceiveNewsLetter = true,
        };  
        PersonAddRequest person2 = new PersonAddRequest()
        {
            PersonName = "Sooraj",
            Email = "rsooraj@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"),
            ReceiveNewsLetter = false,
        };   
        PersonAddRequest person3 = new PersonAddRequest()
        {
            PersonName = "Anita",
            Email = "Anita@gmail.com",
            Gender = Genderoptions.Female,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "Iceland",
            DateOfBirth = DateTime.Parse("1993-11-26"),
            ReceiveNewsLetter = true,
        };

        //Act
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            person1, person2, person3
        };

        List<PersonResponse> personResponses_list = new List<PersonResponse>();

        foreach (PersonAddRequest person in personAddRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses_list.Add(personResponse);
        }
        
        //Printing the Expected 
        _testOutputHelper.WriteLine("Expected: ");
        foreach (PersonResponse person in personResponses_list)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        List<PersonResponse> personResponses_Get_allPersons =  _personService.GetAllPersons();
        
        //Printing the Actual 
        _testOutputHelper.WriteLine("Actual: ");
        foreach (PersonResponse person in personResponses_Get_allPersons)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        
        //Assert
        foreach (PersonResponse person in personResponses_list)
        {
            Assert.Contains(person, personResponses_Get_allPersons); 
        }

    }
    #endregion

    #region GetPersonFiltered UnitTest
    //Test - 1
    //Add few persons to list and search by empty string the filtered person method should return all persons 
    [Fact]
    public void GetfilteredPersons_EmptyString()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Italy"
        };

        CountryResponse countryResponse_after_adding1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse_after_adding2 = _countriesService.AddCountry(countryAddRequest2);

        PersonAddRequest person1 = new PersonAddRequest()
        {
            PersonName = "Visakh",
            Email = "Visakh@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding1.CountryId,
            Address = "Uk",
            DateOfBirth = DateTime.Parse("1996-08-09"),
            ReceiveNewsLetter = true,
        };
        PersonAddRequest person2 = new PersonAddRequest()
        {
            PersonName = "Sooraj",
            Email = "rsooraj@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"),
            ReceiveNewsLetter = false,
        };
        PersonAddRequest person3 = new PersonAddRequest()
        {
            PersonName = "Anita",
            Email = "Anita@gmail.com",
            Gender = Genderoptions.Female,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "Iceland",
            DateOfBirth = DateTime.Parse("1993-11-26"),
            ReceiveNewsLetter = true,
        };

        //Act
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            person1, person2, person3
        };

        List<PersonResponse> personResponses_list = new List<PersonResponse>();

        foreach (PersonAddRequest person in personAddRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses_list.Add(personResponse);
        }

        //Printing the Expected 
        _testOutputHelper.WriteLine("Expected: ");
        foreach (PersonResponse person in personResponses_list)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        List<PersonResponse> personResponses_Get_allPersons_filtered =
            _personService.getFilteredPersonResponses(nameof(Person.PersonName), "");

        //Printing the Actual 
        _testOutputHelper.WriteLine("Actual: ");
        foreach (PersonResponse person in personResponses_Get_allPersons_filtered)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }


        //Assert
        foreach (PersonResponse person in personResponses_list)
        {
            Assert.Contains(person, personResponses_Get_allPersons_filtered);
        }
    }
    //Test - 2
    //Add few persons to list and search by particular string the filtered person method should return persons containing that string
    [Fact]
    public void GetfilteredPersons_withString()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Italy"
        };

        CountryResponse countryResponse_after_adding1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse_after_adding2 = _countriesService.AddCountry(countryAddRequest2);

        PersonAddRequest person1 = new PersonAddRequest()
        {
            PersonName = "Visakh",
            Email = "Visakh@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding1.CountryId,
            Address = "Uk",
            DateOfBirth = DateTime.Parse("1996-08-09"),
            ReceiveNewsLetter = true,
        };
        PersonAddRequest person2 = new PersonAddRequest()
        {
            PersonName = "Sooraj",
            Email = "rsooraj@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"),
            ReceiveNewsLetter = false,
        };
        PersonAddRequest person3 = new PersonAddRequest()
        {
            PersonName = "Anita",
            Email = "Anita@gmail.com",
            Gender = Genderoptions.Female,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "Iceland",
            DateOfBirth = DateTime.Parse("1993-11-26"),
            ReceiveNewsLetter = true,
        };

        //Act
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            person1, person2, person3
        };

        List<PersonResponse> personResponses_list = new List<PersonResponse>();

        foreach (PersonAddRequest person in personAddRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses_list.Add(personResponse);
        }

        //Printing the Expected 
        _testOutputHelper.WriteLine("Expected: ");
        foreach (PersonResponse person in personResponses_list)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        List<PersonResponse> personResponses_Get_allPersons_filtered =
            _personService.getFilteredPersonResponses(nameof(Person.PersonName), "oo");

        //Printing the Actual 
        _testOutputHelper.WriteLine("Actual: ");
        foreach (PersonResponse person in personResponses_Get_allPersons_filtered)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }


        //Assert
        foreach (PersonResponse person in personResponses_list)
        {
            if (person.PersonName != null)
            {
                if (person.PersonName.Contains("oo", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(person, personResponses_Get_allPersons_filtered);
                }
            }
        }
    }

    #endregion

    #region GetSortedPerson UnitTest
    //Testcase - 1
    //Add few persons to list and sort by particular sortOption the sorted person method should return persons with sorted order
    [Fact]
    public void GetSortedPersons_withString()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Italy"
        };

        CountryResponse countryResponse_after_adding1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse_after_adding2 = _countriesService.AddCountry(countryAddRequest2);

        PersonAddRequest person1 = new PersonAddRequest()
        {
            PersonName = "Visakh",
            Email = "Visakh@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding1.CountryId,
            Address = "Uk",
            DateOfBirth = DateTime.Parse("1996-08-09"),
            ReceiveNewsLetter = true,
        };
        PersonAddRequest person2 = new PersonAddRequest()
        {
            PersonName = "Sooraj",
            Email = "rsooraj@gmail.com",
            Gender = Genderoptions.Male,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"),
            ReceiveNewsLetter = false,
        };
        PersonAddRequest person3 = new PersonAddRequest()
        {
            PersonName = "Anita",
            Email = "Anita@gmail.com",
            Gender = Genderoptions.Female,
            CountryId = countryResponse_after_adding2.CountryId,
            Address = "Iceland",
            DateOfBirth = DateTime.Parse("1993-11-26"),
            ReceiveNewsLetter = true,
        };

        //Act
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            person1, person2, person3
        };

        List<PersonResponse> personResponses_list = new List<PersonResponse>();

        foreach (PersonAddRequest person in personAddRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses_list.Add(personResponse);
        }

        //Printing the Expected 
        _testOutputHelper.WriteLine("Expected: ");
        foreach (PersonResponse person in personResponses_list)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        List<PersonResponse> allpersons = _personService.GetAllPersons();
        List<PersonResponse> personResponses_Get_allPersons_sorted =
            _personService.getSortedPersons(allpersons, nameof(Person.PersonName), SortOrderOptions.Asc);

        //Printing the Actual 
        _testOutputHelper.WriteLine("Actual: ");
        foreach (PersonResponse person in personResponses_Get_allPersons_sorted)
        {
            _testOutputHelper.WriteLine(person.ToString());
        }

        personResponses_list = personResponses_list.OrderBy(temp => temp.PersonName).ToList();

        //Assert
        for (int i = 0; i < personResponses_list.Count; i++)
        {
            Assert.Equal(personResponses_list[i],personResponses_Get_allPersons_sorted[i]);
        }
    }

    

    #endregion

    #region GetUpdatedPerson UnitTest

    // When we supply null object as updateperson request it should return null
    [Fact]
    public void GetupdatedPerson_NullObject()
    {
        //Arrange
        PersonUpdateRequest personUpdateRequest = null;
        
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            _personService.getUpdatedPerson(personUpdateRequest);
        });

    }

    // When we supply invalid person id it should return null
    [Fact]
    public void GetUpdatedPerson_InvalidPersonId()
    {
        //Arrange
        PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest()
        {
            PersonId = Guid.NewGuid(),
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _personService.getUpdatedPerson(personUpdateRequest);
        });
        
    }

    //When we supply null Person name for Update request, Method should supply null 
    [Fact]
    public void GetUpdatedPerson_NullPersonname()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryResponse countryResponse_after_adding = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Arun",
            CountryId = countryResponse_after_adding.CountryId,
            Email = "arun@gmail.com",
            Gender = Genderoptions.Male,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"), 
        };
        PersonResponse person_After_adding = _personService.AddPerson(personAddRequest);
        
        PersonUpdateRequest personUpdateRequest = person_After_adding.ToPersonUpdateRequest();
        
        personUpdateRequest.PersonName = null;
        
         //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _personService.getUpdatedPerson(personUpdateRequest);
        }); 
    }
    
    //When we supply Person details for updation, Method should supply updated person 
    [Fact]
    public void GetUpdatedPerson_ValidCase()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryResponse countryResponse_after_adding = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Arun",
            CountryId = countryResponse_after_adding.CountryId,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"),
            ReceiveNewsLetter = false,
            Email = "arun@gmail.com",
            Gender = Genderoptions.Male
        };
        PersonResponse person_After_adding = _personService.AddPerson(personAddRequest);
        
        PersonUpdateRequest personUpdateRequest = person_After_adding.ToPersonUpdateRequest();
        
        personUpdateRequest.PersonName = "Rahul";
        personUpdateRequest.Address = "India";
        
        //Act
        PersonResponse? personResponse_after_updating = _personService.getUpdatedPerson(personUpdateRequest);
        PersonResponse? personResponse_after_getPersonbyid =
            _personService.GetPersonByPersonId(personResponse_after_updating.PersonId); 
        
        //Assert
        Assert.Equal(personResponse_after_updating, personResponse_after_getPersonbyid);
    }
    

    #endregion

    #region DeletePerson UnitTest
    //Testcase when we pass an invalid PersonId, It should return false 
    [Fact]
    public void deletePerson_InvalidPersonId()
    {
        //Act
        bool result_after_deleting = _personService.DeletePerson(Guid.NewGuid());

        //Assert
        Assert.False(result_after_deleting);
    }
    
    //Testcase when we pass a valid PersonId, It should return true 
    [Fact]
    public void deletePerson_ValidPerson()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Australia"
        };
        CountryResponse countryResponse_after_adding = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Arun",
            CountryId = countryResponse_after_adding.CountryId,
            Address = "UAE",
            DateOfBirth = DateTime.Parse("1990-08-02"),
            ReceiveNewsLetter = false,
            Email = "arun@gmail.com",
            Gender = Genderoptions.Male
        };

        PersonResponse person_After_adding = _personService.AddPerson(personAddRequest);
        
        //Act
        bool result_after_deleting = _personService.DeletePerson(person_After_adding.PersonId);
        
        //Assert
        Assert.True(result_after_deleting);
    }

    #endregion
}