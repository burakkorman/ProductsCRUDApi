using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCRUDApi.Models;
using ProductsCRUDApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsCRUDApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginDTO loginDto)
        {
            var jwtToken = _accountService.Login(loginDto);

            if (jwtToken == null)
            {
                return Unauthorized();
            }

            return Ok(jwtToken);
        }
    }
}
