using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
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
            var EmailExist = userBusiness.CheckExist(model.Email);
            if (EmailExist)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Email already exist" });
            }
            else
            {
                var result = userBusiness.UserRegistration(model);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Registration Successfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Registration Failed" });
                }
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginmodel)
        {
            var result = userBusiness.UserLogin(loginmodel);
            if (result !=null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "login Seccessfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "login Failed" });
            }
        }

        [HttpPost]
        [Route("CheckEmail")]
        public IActionResult CheckEmailPresent(string email)
        {
            var result = userBusiness.CheckEmailPresent(email);
            if (result != null )
            {
                return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Email Exist", Data = result });
            }
            else {
                return BadRequest(new ResponseModel<UserEntity> { Status = true, Message = "Email Does Not Exist", Data = result });
            };
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            var result = userBusiness.GetList();
            if(result != null)
            {
                return Ok(new ResponseModel<List<UserEntity>> { Status=true, Message="Details",Data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<List<UserEntity>> { Status = false, Message = "Failed To Fetch Details" });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string Email)
        {
            var result=userBusiness.ForgotPassword(Email);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true,Message="forgot password",Data=result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "password forgot failed" });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string email, string password, string confirmPassword)
        {
            var result= userBusiness.ResetPassword(email, password, confirmPassword);
            if(result)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Password Has Reset" });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Password Reset Failed" });
            }
        }

        [HttpPost]
        [Route("ResetNewPassword")]
        public IActionResult ResetnewPassword(string email, resetPassword reset)
        {
            var result = userBusiness.ResetnewPassword(email, reset);
            if (result)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "Reset New Password" });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Failed" });
            }
        }
    }
}
