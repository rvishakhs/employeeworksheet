using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using static ServiceContracts.Enums.SortOrderOptions;

namespace CrudApplication.Controllers;

public class Person : Controller
{
    //private fields 
    private readonly IPersonService _personService;
    private readonly ICountriesService _countriesService;
    public Person(IPersonService personService, ICountriesService countriesService)
    {
        _personService = personService;
        _countriesService = countriesService;
    }
    
    // Route for index page
    [Route("/persons/index")]
    [Route("/")]
    public IActionResult Index(string searchBy, string searchString, 
        string sortby = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.Asc)
    {
        ViewBag.search = new Dictionary<string, string>()
        {
            { nameof(PersonResponse.PersonName), "Person Name" },
            { nameof(PersonResponse.Email), "Email" },
            { nameof(PersonResponse.Address), "Address" },

        };
        //To get all persons from database and with search facility
        List<PersonResponse> persons = _personService.getFilteredPersonResponses(searchBy,searchString );
        
        //To sort the persons
        List<PersonResponse> sortedPersons = _personService.getSortedPersons(persons, sortby, sortOrder);
        
        //View Bag items 
        ViewBag.CurrentSort = searchBy;
        ViewBag.CurrentSearch = searchString;
        ViewBag.CurrentSortby = sortby;
        ViewBag.CurrentSortOrder = sortOrder.ToString();
        
        //View 
        return View(sortedPersons);
    }

    //Route for create page viewing
    [Route("/persons/create")]
    [HttpGet]
    public IActionResult Create()
    {
        //To get all countries
        List<CountryResponse> countries = _countriesService.GetAllCountries();
        
        
        ViewBag.countries = countries;
        return View();
    }
    //Sepreate Route for create page submission
    [Route("/persons/create")]
    [HttpPost]
    public IActionResult Create(PersonAddRequest person)
    {
        if (!ModelState.IsValid)
        {
            List<CountryResponse> countries = _countriesService.GetAllCountries();
            
            ViewBag.countries = countries; 
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }

        PersonResponse adddedPerson = _personService.AddPerson(person);
        
        return RedirectToAction("Index", "Person");
    }
    
    //Route for edit page viewing
    [HttpGet]
    [Route("~/persons/edit/{personId}")]
    public IActionResult Edit(Guid personId)
    {
        PersonResponse person = _personService.GetPersonByPersonId(personId);
        if (person == null)
        {
            return RedirectToAction("Index", "Person");
        }

        PersonUpdateRequest personUpdateRequest = person.ToPersonUpdateRequest();
        
        //Getting countries in viewBag
        List<CountryResponse> countries = _countriesService.GetAllCountries();
        ViewBag.countries = countries; 
        return View(personUpdateRequest);
    } 
}