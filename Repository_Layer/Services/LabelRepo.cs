//using Common_Layer.Models;
//using Repository_Layer.Context;
//using Repository_Layer.Entity;
//using Repository_Layer.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Repository_Layer.Services
//{
//    public class LabelRepo: ILabelRepo
//    {
//        private readonly FundoDbContext fundoDbContext;

//        public LabelRepo(FundoDbContext fundoDbContext)
//        {
//            this.fundoDbContext= fundoDbContext;
//        }


//        public LabelEntity GetDetails(int UserId)
//        {
//            try
//            {
//                LabelEntity details = fundoDbContext.Labeljs.FirstOrDefault(x => x.Id == UserId);
//                if (details != null)
//                {
//                    return details;
//                }
//                else
//                {
//                    return null;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
        
//        public bool UpdateDetails(int userId, labelModel labelModel)
//        {
//            try
//            {
//                var update = fundoDbContext.Labeljs.FirstOrDefault(y => y.Id == userId);
//                if (update != null)
//                {
//                    labelModel.labelName = update.labelName;
//                    labelModel.usersId = update.Id;
//                    labelModel.noteId = update.NoteId;
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
            

//        }


        
//    }
//}
