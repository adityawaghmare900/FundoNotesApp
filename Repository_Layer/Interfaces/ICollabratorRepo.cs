using Common_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface ICollabratorRepo
    {
        public CollabratorEntity AddCollabrator(int userid, long noteid, string Email);

        public CollabratorEntity DeleteCollabrator(int userid, string email);

        public List<CollabratorEntity> GetCollabrator();

    }
}
