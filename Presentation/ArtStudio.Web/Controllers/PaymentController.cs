using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.DTOs.Expense.Requests;
using ArtStudio.Application.DTOs.Expense.Responses;
using ArtStudio.Application.DTOs.PaymentHistory.Requests;
using ArtStudio.Application.DTOs.PaymentHistory.Responses;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Application.Features.Services;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ArtStudio.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SavePayment")]
        public async Task<Result<PaymentHistory>> Save(PaymentRequest paymentRequest)
        {
            var result = await _paymentService.SavePaymentAsync(paymentRequest);
            return Result<PaymentHistory>.Success(result);
        }

        ///////////////////////////////// Report ////////////////////////////////////////
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost("GetPaymentReport")]
        public async Task<Result<IEnumerable<PaymentReportResponse>>> GetPaymentReport(ReportRequest reportRequest)
        {
            var result = await _paymentService.GetPaymentReportAsync(reportRequest);

            return Result<IEnumerable<PaymentReportResponse>>.Success(result);
        }
    }
}
