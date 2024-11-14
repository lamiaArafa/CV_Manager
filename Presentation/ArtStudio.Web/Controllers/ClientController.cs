using ArtStudio.Application.Common.Wrapper;
using ArtStudio.Application.DTOs;
using ArtStudio.Application.DTOs.Expense.Requests;
using ArtStudio.Application.DTOs.Expense.Responses;
using ArtStudio.Application.Features.Interfaces;
using ArtStudio.Application.Features.Services;
using ArtStudio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArtStudio.Web.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SaveClient")]
        public async Task<Result<Client>> Save(ClientRequest clientRequest)
        {
            var result = await _clientService.SaveClientAsync(clientRequest);
            return Result<Client>.Success(result);
        }

        //[HttpPost("DeleteClient")]
        //public async Task<Result<Client>> Delete(int clientId)
        //{
        //    var result = await _clientService.DeleteClientAsync(clientId);
        //    return Result<Client>.Success(result);
        //}

        [HttpPost("GetClientById")]
        public async Task<Result<ClientsResponse>> GetClientById(int clientId)
        {
            var result = await _clientService.GetClientByIdAsync(clientId);
            return Result<ClientsResponse>.Success(result);
        }

        [HttpPost("GetPaginationClients")]
        public async Task<JsonResult> GetPaginationClients()
        {
            var result = await _clientService.GetPaginationCliensAsync(base.PaginationRequest);
            var returnObj = new
            {
                draw = Draw,
                recordsTotal = result.TotalRecords,
                recordsFiltered = result.TotalRecords,
                data = result.Items
            };

            return Json(returnObj);
        }

        [HttpPost("GetClients")]
        public async Task<Result<List<DropDownResponse>>> GetClients()
        {
            var result = await _clientService.GetClientsAsync();

            return Result<List<DropDownResponse>>.Success(result);
        }
    }
}
