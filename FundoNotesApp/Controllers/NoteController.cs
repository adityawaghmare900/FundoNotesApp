using Business_Layer.Interfaces;
using Business_Layer.Services;
using Common_Layer.Models;
using Experimental.System.Messaging;
using GreenPipes.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBusiness noteBusiness,IDistributedCache distributedCache)
        {
            this.noteBusiness = noteBusiness;
            this.distributedCache= distributedCache;
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
        public IActionResult UpdateNoteModel(long noteid,  UpdateNoteModel model)
        {
            var result = noteBusiness.UpdateNoteModel(noteid, model);
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

        [HttpGet]
        [Route("GetAllNotes")]
        public async Task<IActionResult> AllNotes()
        {
            try
            {
                var CacheKey = "NotesList";
                List<NoteEntity> NoteList;
                byte[] RedisNoteList = await distributedCache.GetAsync(CacheKey);
                if(RedisNoteList != null)
                {
                   // log.logDebug("Getting the list from Redis Cache");
                    var SerilizedListList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(SerilizedListList);
                }
                else
                {
                   // Logger.LogDebug("Setting the list to cache which is requested for the first time");
                    NoteList = (List<NoteEntity>)noteBusiness.GetList();
                    var SerilizedNoteList = JsonConvert.SerializeObject(NoteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(SerilizedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache. SetAsync(CacheKey, redisNoteList, options);
                }
               // Log.LogInformation("Got the notes list successfully from Redis");
                return Ok(NoteList);
            }
            catch(Exception ex)
            {
               // Log.LogCritical(ex, "Exception thorn");
                return BadRequest(new {Status=false,Message=ex.Message});
            }

        }


        [Authorize]
        [HttpGet]
        [Route("Date")]
        public IActionResult SerachNoteByDate(DateTime date)
        {

            List<NoteEntity> result = noteBusiness.SerachNoteByDate(date);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status=true, Message="Note Details", Data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "No such note exist" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Title")]
        public IActionResult SerachNoteByTitle(string title)
        {
            List<NoteEntity> result = noteBusiness.SerachNoteByTitle(title);
            if (result != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message="No such title exist" });
            }
        }
    }
}
