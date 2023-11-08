using Common_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface ILabelBusiness
    {
        public LabelEntity createLabel(int userid, long noteid, labelModel labelModel);
        public LabelEntity GetLabel(int userid, long noteid);

        public bool UpdateDetails(int userId, long noteid, labelModel labelModel);
        public bool DeleteDetails(int userId, long noteid);


    }
}
