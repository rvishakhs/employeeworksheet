using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts;

public interface IPersonService
{
   /// <summary>
   /// This method adds a new person from the request and return a response as PersonResponse
   /// </summary>
   /// <param name="personAddRequest">Details in the form of personAddrequest</param>
   /// <returns>Returns person after adding in the form of PersonResponse</returns>
   PersonResponse AddPerson(PersonAddRequest personAddRequest);
   
   /// <summary>
   /// A Method to get all persons
   /// </summary>
   /// <returns>All Persons list </returns>
   List<PersonResponse> GetAllPersons();

   /// <summary>
   /// This method is used to get person by PersonId
   /// </summary>
   /// <param name="PersonId">Guid for the specific Person</param>
   /// <returns>Fetch the person matching with the Guid and return as Person Response</returns>
   PersonResponse? GetPersonByPersonId(Guid? PersonId);

   /// <summary>
   /// This method is used to return all filtered persons by searchstring
   /// </summary>
   /// <param name="searchBy">Tittle or column need to be search with</param>
   /// <param name="searchString">value to be searched </param>
   /// <returns>Return the filtered list based on the searching criteria</returns>
   List<PersonResponse> getFilteredPersonResponses(string searchBy, string? searchString);
   /// <summary>
   /// Method returns the sorted list based on the sorting criteria
   /// </summary>
   /// <param name="allpersons">Collection of persons to be sorted (All persons)</param>
   /// <param name="sortby">Criteria or tittle need to be sort  </param>
   /// <param name="sortOrder">Ascending or descending </param>
   /// <returns>Sorted List based on the criteria</returns>
   List<PersonResponse> getSortedPersons(List<PersonResponse> allpersons, string sortby, SortOrderOptions sortOrder);
   /// <summary>
   /// Update Person method that accepts a person update request and update the person and returns the updated person
   /// </summary>
   /// <param name="personUpdateRequest">personUpdateRequest</param>
   /// <returns>updated person in PersonResponse format</returns>
   PersonResponse getUpdatedPerson(PersonUpdateRequest? personUpdateRequest);
   /// <summary>
   /// Method for deleting a person
   /// </summary>
   /// <param name="personId">Id of the person</param>
   /// <returns>Boolean value true or false</returns>
   bool DeletePerson(Guid? personId);
} 