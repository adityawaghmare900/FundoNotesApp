using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;

namespace FundoNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabratorController : ControllerBase
    {
        private readonly ICollabratorBusiness collabratorBusiness;

        public CollabratorController(ICollabratorBusiness collabratorBusiness)
        {
            this.collabratorBusiness = collabratorBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("Save")]
        public IActionResult AddCollabrator(int userid, long noteid, string Email)
        {
            try
            {
                var result = collabratorBusiness.AddCollabrator(userid, noteid,Email);
                if (result != null)
                {
                    return Ok(new ResponseModel<CollabratorEntity> { Status = true, Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<CollabratorEntity> { Status = false, Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteCollabrator(int userid, string email)
        {
            var result=collabratorBusiness.DeleteCollabrator(userid,email);
            if(result !=null )
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Deleted Successfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = " failed" });
            }
        }


        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult GetCollabrator()
        {
            var get=collabratorBusiness.GetCollabrator();
            if(get!=null)
            {
                return Ok(new ResponseModel<List<CollabratorEntity>> { Status = true, Message = "Data",Data=get });
            }
            else
            {
                return BadRequest(new ResponseModel<List<CollabratorEntity>> { Status = false, Message = "Data failed"});

            }
        }
    }
}
