using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackspaceGaming.Entity.Model;
using BackspaceGaming.Service;
using BackspaceGaming.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URF.Core.Abstractions;

namespace BackspaceGamingCore.Controllers
{
    [Route("api/login"), /*Authorize*/]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(ILoginService service, IUnitOfWork unitOfWork)
        {
            this._service = service;
            this._unitOfWork = unitOfWork;
    }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            if (result.Any())
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users users)
        {
            var result = await _service.Add(users, _unitOfWork);
            if (result)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}