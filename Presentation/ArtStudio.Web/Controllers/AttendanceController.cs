using ArtStudio.Web.Controllers;
using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.DTOs.Attendance.Requests;
using ArtStudio.Application.DTOs.Expense.Requests;
using ArtStudio.Application.DTOs.Expense.Responses;
using ArtStudio.Application.DTOs.PaymentHistory.Requests;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Application.Features.Services;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ArtStudio.Application.DTOs.PaymentHistory.Responses;
using ArtStudio.Application.DTOs.Attendance.Responses;

namespace ArtStudio.Web.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SaveAttendance")]
        public async Task<Result<Attendance>> Save(AttendanceRequest attendanceRequest)
        {
            var result = await _attendanceService.SaveAttendanceAsync(attendanceRequest);
            return Result<Attendance>.Success(result);
        }
        [HttpPost("GetAllowedCoursesByClientId")]
        public async Task<Result<List<DropDownResponse>>> GetAllowedCoursesByClientId(int clientId)
        {
            var result = await _attendanceService.GetAllowedCoursesByClientId(clientId);

            return Result<List<DropDownResponse>>.Success(result);
        }

        ///////////////////////////////// Report ////////////////////////////////////////
        public IActionResult Report()
        {
            return View();
        }

        [HttpPost("GetAttendanceReport")]
        public async Task<Result<IEnumerable<AttendanceReportResponse>>> GetAttendanceReport(ReportRequest reportRequest)
        {
            var result = await _attendanceService.GetAttendanceReportAsync(reportRequest);

            return Result<IEnumerable<AttendanceReportResponse>>.Success(result);
        }
    }
}
