//using Business_Layer.Interfaces;
//using Common_Layer.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Repository_Layer.Entity;

//namespace FundoNotesApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LabelController : ControllerBase
//    {
//        private readonly ILabelBusiness labelBusiness;

//        public LabelController(ILabelBusiness labelBusiness)
//        {
//            this.labelBusiness = labelBusiness;
//        }


//        [HttpGet]
//        [Route("Get")]
//        public IActionResult GetDetails(int userId)
//        {
//            var details=labelBusiness.GetDetails(userId);
//            if(details !=null)
//            {
//                return Ok(new ResponseModel<LabelEntity> { Status = true, Message="Details Fetech",Data = details });
//            }
//            else
//            {
//                return BadRequest(new ResponseModel<LabelEntity> { Status = true, Message="Details not fetch", Data = details });
//            }

//        }

//        [HttpPut]
//        [Route("UpdateDetails")]
//        public IActionResult UpdateDetails(int userId, labelModel labelModel)
//        {
//            bool updatedetails = labelBusiness.UpdateDetails(userId, labelModel);
//            if (updatedetails ==true)
//            {
//                return Ok(new ResponseModel<bool> { Status = true, Message = "Details update",Data=updatedetails });
//            }
//            else
//            {
//                return BadRequest(new ResponseModel<bool> { Status = true, Message = "Details updated" });
//            }
//        }
//        }
//}
