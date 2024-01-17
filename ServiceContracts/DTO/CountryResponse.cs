using Entities;

namespace ServiceContracts.DTO;
/// <summary>
/// Country response class give a response for a country service after adding 
/// </summary>
public class CountryResponse
{
    public Guid CountryId { get; set; }
    public string? CountryName { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj.GetType() != typeof(CountryResponse))
        {
            return false;
        }
        CountryResponse countrytoresponse = (CountryResponse)obj;
        
        return CountryId == countrytoresponse.CountryId && CountryName == countrytoresponse.CountryName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"CountryId: {CountryId}, CountryName: {CountryName}";
    }
}

public static class CountryExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse()
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName,
        };
    }
}