using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs.Attendance.Responses;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.DTOs.Expense.Requests;
using ArtStudio.Application.DTOs.Expense.Responses;
using ArtStudio.Application.DTOs.PaymentHistory.Requests;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Application.Features.Services;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ArtStudio.Application.DTOs.Expense.Response;

namespace ArtStudio.Web.Controllers
{
    public class ExpensesController : BaseController
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("SaveExpenses")]
        public async Task<Result<ExpenseHistory>> Save(ExpenseRequest expenseRequest)
        {
            var result = await _expenseService.SaveExpenseAsync(expenseRequest);
            return Result<ExpenseHistory>.Success(result);
        }

        [HttpPost("DeleteExpense")]
        public async Task<Result<ExpenseHistory>> Delete(int expenseId)
        {
            var result = await _expenseService.DeleteExpenseHistoryAsync(expenseId);
            return Result<ExpenseHistory>.Success(result);
        }

        [HttpPost("GetPaginationExpensesHistory")]
        public async Task<JsonResult> GetPaginationExpensesHistory()
        {
            var result = await _expenseService.GetPaginationExpensesHistoryAsync(base.PaginationRequest);
            var returnObj = new
            {
                draw = Draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = result.Items
            };

            return Json(returnObj);
        }

        ///////////////////////////////// Report ////////////////////////////////////////
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost("GetExpensesReport")]
        public async Task<Result<IEnumerable<ExpensesReportResponse>>> GetExpensesReport(ReportRequest reportRequest)
        {
            var result = await _expenseService.GetExpensesReportAsync(reportRequest);

            return Result<IEnumerable<ExpensesReportResponse>>.Success(result);
        }
    }
}
