using ArtStudio.Application.Common.Exceptions;
using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.DTOs.Expense.Requests;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Application.Features.Services;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArtStudio.Web.Controllers
{
    public class LookupController : BaseController
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("GetAgeGroups")]
        public async Task<Result<List<DropDownResponse>>> GetAgeGroups()
        {
            var result = await _lookupService.GetAgeGroupsAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }

        [HttpPost("GetCourses")]
        public async Task<Result<List<DropDownResponse>>> GetCourses()
        {
            var result = await _lookupService.GetCoursesAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }

        [HttpPost("GetEmployees")]
        public async Task<Result<List<DropDownResponse>>> GetEmployees()
        {
            var result = await _lookupService.GetEmployeesAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }

        [HttpPost("GetExpenseTypes")]
        public async Task<Result<List<DropDownResponse>>> GetExpenseTypes()
        {
            var result = await _lookupService.GetExpenseTypesAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }

        [HttpPost("GetMaterials")]
        public async Task<Result<List<DropDownResponse>>> GetMaterials()
        {
            var result = await _lookupService.GetMaterialsAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }

        ///  the main endpoint for the controller
        [HttpPost("GetLookupTypes")]
        public async Task<Result<List<DropDownResponse>>> GetLookupTypes()
        {
            var result = await _lookupService.GetLookupTypesAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }

        [HttpPost("SaveLookup")]
        public async Task<Result<Lookup>> Save(LookupRequest lookupRequest)
        {
            var result = await _lookupService.SaveLookupAsync(lookupRequest);
            return Result<Lookup>.Success(result);
        }

        [HttpPost("GetPaginationLookups")]
        public async Task<JsonResult> GetPaginationLookups()
        {

            var result = await _lookupService.GetPaginationLookupsAsync(base.PaginationRequest);
            var returnObj = new
            {
                draw = Draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = result.Items
            };

            return Json(returnObj);
        }

        [HttpPost("DeleteLookups")]
        public async Task<Result<Lookup>> Delete(int lookupId)
        {
            var result = await _lookupService.DeleteLookupAsync(lookupId);
            return Result<Lookup>.Success(result);
        }
    }
}
