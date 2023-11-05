//using Business_Layer.Interfaces;
//using Common_Layer.Models;
//using Repository_Layer.Entity;
//using Repository_Layer.Services;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Business_Layer.Services
//{
//    public class LabelBusiness: ILabelBusiness
//    {
//        private readonly LabelRepo labelRepo;
        
//        public LabelBusiness(LabelRepo labelRepo)
//        {
//            this.labelRepo = labelRepo;
//        }

//        public LabelEntity GetDetails(int UserId)
//        {
//            return labelRepo.GetDetails(UserId);
//        }

//        public bool UpdateDetails(int userId, labelModel labelModel)
//        {
//            return labelRepo.UpdateDetails(userId, labelModel);
//        }

//    }
//}
