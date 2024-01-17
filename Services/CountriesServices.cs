using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesServices : ICountriesService
{
    private readonly List<Country> _countries;

    //Constructor
    public CountriesServices(bool initializeCountries = true)
    {
        _countries = new List<Country>();
        if (initializeCountries)
        {
            _countries.AddRange(new List<Country>()
            {
                new Country()
                {
                    CountryId = Guid.Parse("94065F9A-2F61-43E2-8AD2-66C4359B6039"),
                    CountryName = "India"
                },
                new Country()    
                {
                    CountryId = Guid.Parse("FBC6553F-DB1D-4DA9-916A-1B72CFB9FA37"),
                    CountryName = "USA"
                },
                new Country()
                {
                    CountryId = Guid.Parse("3F48DF3C-555E-4D60-918F-F7A53651D431"),
                    CountryName = "UK"
                },
                new Country()
                {
                    CountryId = Guid.Parse("4BD9E793-D478-4972-A0CA-C1B9D7D8A215"),
                    CountryName = "Canada"
                },
                new Country()
                {
                    CountryId = Guid.Parse("1B05FE76-71D5-4F4F-89BC-231527A13E85"),
                    CountryName = "Australia"
                },   
            });
        }
    }
    
    #region Add Country
    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        
        //Validation: If countryAddRequest is null then throw ArgumentNullExeception
        if(countryAddRequest == null)
        {
            throw new ArgumentNullException(nameof(countryAddRequest));
        }
        //Validation: If CountryAddRequest Provided with null Country name then have to return Argument Exeception
        if (countryAddRequest.CountryName == null)
        {
            throw new ArgumentException(countryAddRequest.CountryName);
        }
        //Validation: If CountryAddRequest Provided with duplicate CountryName Then have to return Argument Exeception
        if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
        {
            throw new ArgumentException("CountryName Already Exists");
        }
        
        //After validation adding country into List 
        //convert object from CountryAddRequest to Country
       Country country = countryAddRequest.ToCountry();
       
       //Add Guid for the country 
       country.CountryId = Guid.NewGuid();
      
        //Add new country to private list 
       _countries.Add(country);
       
       //convert object from Country to CountryResponse
       return country.ToCountryResponse();
    }
    #endregion

    #region Get All Countries

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(country => country.ToCountryResponse()).ToList();
    }
    
    #endregion
 
    #region Get Country By Country Id
    public CountryResponse? GetCountryByCountryId(Guid? countryId)
    {
        if (countryId == null)
        {
            return null;
        }

        Country? country_from_List = _countries.FirstOrDefault(temp => temp.CountryId == countryId);
        
        if (country_from_List == null)
        {
            return null;
        }

        return  country_from_List.ToCountryResponse();

    }
    #endregion
}