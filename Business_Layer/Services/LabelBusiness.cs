using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using Repository_Layer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepo labelRepo;

        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        public LabelEntity createLabel(int userid, long noteid, labelModel labelModel)
        {
            return labelRepo.createLabel(userid, noteid, labelModel);
        }

        public LabelEntity GetLabel(int userid, long noteid)
        {
            return labelRepo.GetLabel(userid, noteid);
        }

        public bool UpdateDetails(int userId, long noteid, labelModel labelModel)
        {
            return labelRepo.UpdateDetails(userId, noteid, labelModel);
        }

        public bool DeleteDetails(int userId, long noteid)
        {
            return labelRepo.DeleteDetails(userId, noteid);
        }


    }
}
