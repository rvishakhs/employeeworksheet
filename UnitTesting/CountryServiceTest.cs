using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;

namespace UnitTesting;

public class CountryServiceTest
{
    private readonly ICountriesService _countriesService;
    private readonly ITestOutputHelper _testOutputHelper;
    

    //Constructor created for CountryServiceTest
    public CountryServiceTest(ITestOutputHelper testOutputHelper)
    {
        _countriesService = new CountriesServices(false);
        _testOutputHelper = testOutputHelper; 
    }

    #region AddCountry UnitTest
    
    //Test Case - 1
    //When CountryAddRequest is null then the response should be null.
    [Fact]
    public void AddCountry_NullRequest()
    {
        //Arrange
        CountryAddRequest countryAddRequest = null;
        
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            _countriesService.AddCountry(countryAddRequest);
        });
        
    }
    
    //Test Case - 2
    //When Countryaddrequest provide country name as null then the response should be ArgumentException.
    [Fact]
    public void AddCountry_NullCountryName()
    {
        //Arrange 
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = null
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _countriesService.AddCountry(countryAddRequest);
        });
    }
    
    //Test Case - 3
    //When Countryaddrequest provide country name with duplicate value method must return ArgumentException.
    [Fact]
    public void AddCountry_DuplicateCountryName()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Canada"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Canada"
        };
        
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _countriesService.AddCountry(countryAddRequest1);
            _countriesService.AddCountry(countryAddRequest2);

        }); 
    }
    
    //Test Case - 4
    //When Countryaddrequest provided with proper country name the response should be CountryResponse with CountryID
    [Fact]
    public void AddCountry_ValidCountry()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        
        //Act
        CountryResponse response = _countriesService.AddCountry(countryAddRequest);
        List<CountryResponse> countryResponses = _countriesService.GetAllCountries();
        
        //Assert
        Assert.True(response.CountryId != null);
        Assert.Contains(response, countryResponses);
    }
    #endregion

    #region GetAllCountries UnitTest
     //TestCase 1
     //When GetallCountries method called and list is empty then the response should be empty list.
     [Fact]
     public void GetAllCountries_EmptyList()
     {
         //Act 
         List<CountryResponse> countryResponses = _countriesService.GetAllCountries();
         
         //Assert
         Assert.Empty(countryResponses);
     }
     
     //TestCase 2
     //When GetallCountries method called and supply some countries and method should return the supplied countries
     [Fact]
     public void GetAllCountries_AddFewCountires()
     {
         //Arrange 
         List<CountryAddRequest> countryAddRequests = new List<CountryAddRequest>()
         {
             new CountryAddRequest() { CountryName = "Canada" },
             new CountryAddRequest() { CountryName = "Ireland" },
             new CountryAddRequest() { CountryName = "Denmark" },
         };
         
         //Act 
         List<CountryResponse> countryResponse_afteradding = new List<CountryResponse>(); 
         foreach (CountryAddRequest country in countryAddRequests)
         {
            countryResponse_afteradding.Add(_countriesService.AddCountry(country));
         }
         //Printing the Expected 
         _testOutputHelper.WriteLine("Expected: ");
         foreach (CountryResponse country in countryResponse_afteradding)
         {
             _testOutputHelper.WriteLine(country.ToString());
         }
         
         List<CountryResponse> getallcountrieslist = _countriesService.GetAllCountries();

         //Printing the Expected 
         _testOutputHelper.WriteLine("Actual: ");
         foreach (CountryResponse country in getallcountrieslist)
         {
             _testOutputHelper.WriteLine(country.ToString());
         }
         
         //Assert
         foreach (CountryResponse expectedCountry in countryResponse_afteradding)
         {
             Assert.Contains(expectedCountry, getallcountrieslist);
         }
     }

    #endregion

    #region GetCountryByCountryId UnitTest

    //TestCase - 1
    //When CountryId is null then response should be null.
    [Fact]
    public void     GetCountryByCountryId_NullCase()
    {
        //Arrange
        Guid? countryId = null;
        
        //Act
        CountryResponse? response_from_get_country_ID = _countriesService.GetCountryByCountryId(countryId);
        
        //Assert
        Assert.Null(response_from_get_country_ID);
        
    }
    
    //TestCase 2
    //When A Countryadded through CountryAdd method and after we calling GetCountryByID that method must return same country Added
    [Fact]
    public void GetCountryByCountryId_ValidCase()
    {
        //Arrange
        CountryAddRequest? countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Canada"
        };
        CountryResponse country_after_adding = _countriesService.AddCountry(countryAddRequest);
        _testOutputHelper.WriteLine(country_after_adding.ToString());
        
        //Act 
        CountryResponse? country_after_getting = _countriesService.GetCountryByCountryId(country_after_adding.CountryId);
        _testOutputHelper.WriteLine(country_after_getting.ToString());
        
        //Assert
        Assert.Equal(country_after_adding, country_after_getting);
        
    }

    #endregion
} 