
using Common_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Business_Layer.Interfaces
{
    public interface ICollabratorBusiness
    {
        public CollabratorEntity AddCollabrator(int userid, long noteid, string Email);

        public CollabratorEntity DeleteCollabrator(int userid, string email);

        public List<CollabratorEntity> GetCollabrator();


    }
}
