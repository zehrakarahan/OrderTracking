using AutoMapper.Internal;
using Database.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderTrackingService.Service;
using System;
using System.Net;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Ok();
        }
       [HttpPost("Login")]
       public async Task<IActionResult> Login(LoginRequestModel model)
        {
            return Ok(await _accountService.PasswordLoginAsync(model,
                Request.HttpContext.Connection.RemoteIpAddress ?? IPAddress.None
               ));
        }
       [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(ForgotPasswordModel model)
        {
            return Ok(_accountService.ForgetPassword(model));
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(RegisterRequestModel model)
        {

            return Ok(await _accountService.CreateUser(model));
          
        }
        [HttpPost("AddRole")]
        [AllowAnonymous]
        public IActionResult AddRole(string roleName)
        {
            return Ok(_accountService.AddRole(roleName));
        }


    }
}
