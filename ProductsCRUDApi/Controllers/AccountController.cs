using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Login([FromBody]UserDTO user)
        {
            user.Password = _accountService.HashPassword(user.Password);
            var jwtToken = _accountService.Login(user);

            if (jwtToken == null)
            {
                return Unauthorized();
            }

            return Ok(jwtToken);
        }

        [Authorize]
        [Route("Create")]
        [HttpPost]
        public void Create([FromBody]UserDTO user)
        {
            _accountService.Create(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]UserDTO user)
        {
            _accountService.Update(id, user);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _accountService.Delete(id);
        }
    }
}
