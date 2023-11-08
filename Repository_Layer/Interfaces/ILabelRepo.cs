using Common_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface ILabelRepo
    {
        public LabelEntity createLabel(int userid, long noteid, labelModel labelModel);
        public LabelEntity GetLabel(int userid, long noteid);
        public bool UpdateDetails(int userId, long noteid, labelModel labelModel);
        public bool DeleteDetails(int userId, long noteid);


    }
}
