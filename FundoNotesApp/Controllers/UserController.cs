﻿using Business_Layer.Interfaces;
using Common_Layer.Models;
using GreenPipes.Internals.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

namespace FundoNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly ILogger<UserController> log;
        public UserController(IUserBusiness userBusiness, ILogger<UserController> log)
        {
            this.userBusiness = userBusiness;
            this.log = log;
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
                log.LogInformation("Registration Started");

                var result = userBusiness.UserRegistration(model);
                if (result != null)
                {
                    log.LogInformation("Registration was successfull");
                    return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Registration Successfull", Data = result });
                }
                else
                {
                    log.LogError("Registration failed...");
                    return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Registration Failed" });
                }
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginmodel)
        {
            var result = userBusiness.UserLogin(loginmodel);
            if (result != null)
            {
                log.LogInformation("login successfull");
                return Ok(new ResponseModel<string> { Status = true, Message = "login Seccessfull", Data = result });
            }
            else
            {
                log.LogInformation("login failed...");

                return BadRequest(new ResponseModel<string> { Status = false, Message = "login Failed" });
            }
        }

        [HttpPost]
        [Route("CheckEmail")]
        public IActionResult CheckEmailPresent(string email)
        {
            var result = userBusiness.CheckEmailPresent(email);
            if (result != null)
            {
                log.LogInformation("Email Exist");
                return Ok(new ResponseModel<UserEntity> { Status = true, Message = "Email Exist", Data = result });
            }
            else
            {
                log.LogInformation("Email Not Exist");
                return BadRequest(new ResponseModel<UserEntity> { Status = false, Message = "Email Does Not Exist", Data = result });
            };
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            var result = userBusiness.GetList();
            if (result != null)
            {
                log.LogInformation("Details");

                return Ok(new ResponseModel<List<UserEntity>> { Status = true, Message = "Details", Data = result });
            }
            else
            {
                log.LogInformation("Failed to fetch details ");

                return BadRequest(new ResponseModel<List<UserEntity>> { Status = false, Message = "Failed To Fetch Details" });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string Email)
        {
            var result = userBusiness.ForgotPassword(Email);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Status = true, Message = "forgot password", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Status = false, Message = "password forgot failed" });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(resetPassword reset)
        {
            string email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            var result = userBusiness.ResetPassword(email, reset);
            if (result != null)
            {
                return Ok(new ResponseModel<resetPassword> { Status = true, Message = "Password Reset successfully" });
            }
            else
            {
                return this.BadRequest(new ResponseModel<resetPassword> { Status = false, Message = "Password Reset Failed" });
            }
        }
    }
}

    
