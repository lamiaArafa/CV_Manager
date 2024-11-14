using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs.Expense.Requests;
using ArtStudio.Application.DTOs.Expense.Responses;
using ArtStudio.Application.DTOs.Enrollment.Requests;
using ArtStudio.Application.DTOs.Enrollment.Responses;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArtStudio.Web.Controllers
{
    public class EnrollmentController : BaseController
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SaveEnrollment")]
        public async Task<Result<Enrollment>> Save(EnrollmentRequest enrollmentRequest)
        {
            var result = await _enrollmentService.SaveEnrollmentAsync(enrollmentRequest);
            return Result<Enrollment>.Success(result);
        }

        [HttpPost("DeleteEnrollment")]
        public async Task<Result<Enrollment>> Delete(int enrollmentId)
        {
            var result = await _enrollmentService.DeleteEnrollmentAsync(enrollmentId);
            return Result<Enrollment>.Success(result);
        }

        [HttpPost("GetEnrollmentById")]
        public async Task<Result<EnrollmentResponse>> GetEnrollmentById(int enrollmentId)
        {
            var result = await _enrollmentService.GetEnrollmentByIdAsync(enrollmentId);
            return Result<EnrollmentResponse>.Success(result);
        }

        [HttpPost("GetPaginationActiveEnrollments")]
        public async Task<JsonResult> GetPaginationActiveEnrollments()
        {

            var result = await _enrollmentService.GetPaginationActiveEnrollmentsAsync(base.PaginationRequest);
            var returnObj = new
            {
                draw = Draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = result.Items
            };

            return Json(returnObj);
        }
    }
}
