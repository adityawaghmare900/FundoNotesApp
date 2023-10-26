using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Repository_Layer.Entity;
using System;
using System.Net.Http;

namespace FundoNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Registration(RegisterModel model)
        {
           var result=userBusiness.UserRegistration(model);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Registration Successfull", Data = result});
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Registration Failed" });
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginmodel)
        {
            var result = userBusiness.UserLogin(loginmodel);
            if (result == "login Successful")
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Registration Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "Registration Failed" });
            }
        }



    }
}
