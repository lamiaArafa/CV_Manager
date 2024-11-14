using ArtStudio.Application.Common.Wrapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace ArtStudio.Web.Controllers
{
    public class BaseController : Controller
    {
        public PaginationRequest PaginationRequest
        {
            get =>
                new()
                {
                    Skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0"),
                    PageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0"),
                    SearchBy = Request.Form["search[value]"].FirstOrDefault(),
                    SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(),
                    SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault()
                };
        }
        public string? Draw { get => Request.Form["draw"].FirstOrDefault(); }
    }
}