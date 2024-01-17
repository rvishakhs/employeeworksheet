using ServiceContracts.DTO;
namespace ServiceContracts;

public interface ICountriesService
{
    /// <summary>
    /// This method adds a new country from the request and return a response as CountryResponse
    /// </summary>
    /// <param name="countryAddRequest">Accept addrequest as parameter</param>
    /// <returns>After adding Country Response with including Guid</returns>
    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        return new CountryResponse();
    }
    /// <summary>
    /// Method to get all countries from the database
    /// </summary>
    /// <returns>All Countries in the list format </returns>
    List<CountryResponse> GetAllCountries();

    /// <summary>
    /// Method to get country by country id
    /// </summary>
    /// <param name="countryId"></param>
    /// <returns>Specific Country with the supplied CountryId</returns>
    CountryResponse? GetCountryByCountryId(Guid? countryId);
}