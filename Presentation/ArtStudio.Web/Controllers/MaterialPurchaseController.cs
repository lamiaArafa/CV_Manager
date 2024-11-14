using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs.Expense.Response;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.DTOs.PaymentHistory.Requests;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Application.Features.Services;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ArtStudio.Application.DTOs.MaterialPurchase.Responses;

namespace ArtStudio.Web.Controllers
{
    public class MaterialPurchaseController : BaseController
    {
        private readonly IMaterialPurchaseService _materialPurchaseService;

        public MaterialPurchaseController(IMaterialPurchaseService materialPurchaseService)
        {
            _materialPurchaseService = materialPurchaseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SaveMaterialPurchase")]
        public async Task<Result<MaterialPurchase>> Save(MaterialPurchaseRequest materialPurchaseRequest)
        {
            var result = await _materialPurchaseService.SaveMaterialPurchaseAsync(materialPurchaseRequest);
            return Result<MaterialPurchase>.Success(result);
        }

        ///////////////////////////////// Report ////////////////////////////////////////
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost("GetMaterialsReport")]
        public async Task<Result<IEnumerable<MaterialsReportResponse>>> GetMaterialsReport(ReportRequest reportRequest)
        {
            var result = await _materialPurchaseService.GetMaterialsReportAsync(reportRequest);

            return Result<IEnumerable<MaterialsReportResponse>>.Success(result);
        }
    }
}
