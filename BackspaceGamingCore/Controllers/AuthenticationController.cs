using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackspaceGaming.Entity.Model;
using BackspaceGaming.Service;
using BackspaceGaming.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackspaceGamingCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        public AuthenticationController(IAuthenticationService service)
        {
            this._service = service; 
        }

        [HttpPost("GetToken")]
        
        public async Task<IActionResult> GetJWTToken([FromBody]AuthenticationBodyModel model)
        {
            var result = await _service.GetJWTToken(model);
            if (result != null)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}