using CV_Manager.Application.DTOs;
using CV_Manager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CV_Manager.API.Controllers
{
    public class CVController : Controller
    {
        private readonly ICVService cvService;

        public CVController(ICVService _cvService)
        {
            cvService = _cvService;
        }

        [HttpPost("CreateORUpdateCV")]
        public async Task<IActionResult> CreateORUpdateCV([FromBody] CVDto cvDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCV = await cvService.CreateORUpdateCV(cvDto);
            return CreatedAtAction(nameof(GetCVById), new { id = createdCV.Id }, createdCV);
        }

        [HttpGet("GetCVById")]
        public async Task<IActionResult> GetCVById(int id)
        {
            var cvDto = await cvService.GetCVByIdAsync(id);
            if (cvDto == null)
                return NotFound();

            return Ok(cvDto);
        }

        [HttpDelete("DeleteCV")]
        public async Task<IActionResult> DeleteCV(int id)
        {
            var deletedCV = await cvService.DeleteCVAsync(id);
            if (deletedCV == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("GetAllCVs")]
        public async Task<IActionResult> GetAllCVs()
        {
            var result = await cvService.GetAllCVAsync();
            return Ok(result);
        }
    }
}
