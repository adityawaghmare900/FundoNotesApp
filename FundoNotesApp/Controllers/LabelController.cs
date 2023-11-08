

using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace FundoNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;

        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(int userid, long noteid, labelModel labelModel)
        {
            
            var details =labelBusiness.createLabel(userid, noteid, labelModel);
            if (details != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status = true, Message = "Label Created", Data = details });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Status = false, Message = "Label not Created", Data = details });
            }

        }
        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult GetLabel(int userid, long noteid)
        {
            LabelEntity get = labelBusiness.GetLabel(userid, noteid);
            if(get != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Status=true,Message="Data",Data=get});
            }
            else
            {
                return Ok(new ResponseModel<LabelEntity> { Status = false, Message = "Does not get"});
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateDetails(int userId, long noteid, labelModel labelModel)

        {
            bool updatedetails = labelBusiness.UpdateDetails(userId,noteid, labelModel);
            if (updatedetails == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Details update", Data = updatedetails });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = "Details Does Not updated" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Delete")]
        public IActionResult DeleteDetails(int userId, long noteid)

        {
            bool updatedetails = labelBusiness.DeleteDetails(userId, noteid);
            if (updatedetails == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Details Deleted" });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = "Details Does Not updated" });
            }
        }
    }
}
