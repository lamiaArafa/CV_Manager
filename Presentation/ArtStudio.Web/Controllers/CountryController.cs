using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.Features.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArtStudio.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("GetCountries")]
        public async Task<Result<List<DropDownResponse>>> GetCountries()
        {
            var result = await _countryService.GetCountriesAsync();

            return Result<List<DropDownResponse>>.Success(result);

        }
    }
}
