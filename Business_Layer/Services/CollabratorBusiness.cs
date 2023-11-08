using Business_Layer.Interfaces;
using Common_Layer.Models;
using Microsoft.Extensions.Logging;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class CollabratorBusiness : ICollabratorBusiness
    {
        private readonly ICollabratorRepo collabratorRepo;
        //private readonly ILogger<NoteController> log;
        public CollabratorBusiness(ICollabratorRepo collabratorRepo)
        {
            this.collabratorRepo = collabratorRepo;
            
        }

        public CollabratorEntity AddCollabrator(int userid, long noteid, string Email)
        {
            return collabratorRepo.AddCollabrator(userid,noteid,Email);
        }

        public CollabratorEntity DeleteCollabrator(int userid, string email)
        {
            return collabratorRepo.DeleteCollabrator(userid, email);
        }

        public List<CollabratorEntity> GetCollabrator()
        {
            return collabratorRepo.GetCollabrator();
        }

      
    }
}
