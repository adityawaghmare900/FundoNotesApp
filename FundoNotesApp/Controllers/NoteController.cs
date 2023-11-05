using Business_Layer.Interfaces;
using Business_Layer.Services;
using Common_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FundoNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        readonly INoteBusiness noteBusiness;
        public NoteController(INoteBusiness noteBusiness)
        {
            this.noteBusiness = noteBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("addNote")]
        public IActionResult AddNote(NoteAddModel note)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var result = noteBusiness.AddNote(note,userId);
            if (result !=null)
            {
                return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "User Details", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Note Add Failed" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            var result = noteBusiness.GetList();
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "Details", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "Failed To Fetch Details" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNoteModel(long noteid, long userid, UpdateNoteModel model)
        {
            var result = noteBusiness.UpdateNoteModel(noteid, userid, model);
            if (result ==true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Updated successfull" });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message ="Wrong User id or NoteId" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Delete")]
        public IActionResult deleteNote(long noteId, long userId)
        {
            var result=noteBusiness.deleteNote(noteId, userId);
            if(result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Message = "Note Delete Successfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message = "Wrong User id or NoteId" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("IsPin")]
        public IActionResult IsPin(long noteId, long userId)
        {
            var result = noteBusiness.IsPin(noteId, userId);
            if (result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false});
            }
        }

        [Authorize]
        [HttpGet]
        [Route("IsArchieve")]
        public IActionResult IsArchieve(long noteId, long userId)
        {
            var result = noteBusiness.IsArchieve(noteId, userId);
            if (result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("IsTrash")]
        public IActionResult IsTrash(long noteId, long userId)
        {
            var result = noteBusiness.IsTrash(noteId, userId);
            if (result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Delete")]
        public IActionResult DeleteForever(long noteId, long userId)
        {
            var result = noteBusiness.DeleteForever(noteId, userId);
            if (result == true)
            {
                return Ok(new ResponseModel<bool> { Status = true, Data=result, Message="Deleted" });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Status = false, Message="Note deleted" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("color")]
        public IActionResult Color(long noteId, long userId, string color)
        {
            NoteEntity result = noteBusiness.Color(noteId, userId, color);
            if (result !=null)
            {
                return Ok(new ResponseModel<NoteEntity> { Status = true,  Message = "color change",Data=result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "not change" });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Reminder")]
        public IActionResult Reminder(long noteId, long userId, DateTime reminder)
        {
            NoteEntity result = noteBusiness.Reminder(noteId, userId, reminder);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "reminder has set",Data=result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Reminder Not set" });
            }
        }
    }
}
