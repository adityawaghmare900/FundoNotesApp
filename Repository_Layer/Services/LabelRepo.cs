using Common_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundoDbContext fundoDbContext;

        public LabelRepo(FundoDbContext fundoDbContext)
        {
            this.fundoDbContext = fundoDbContext;
        }


        public LabelEntity createLabel(int userid, long noteid, labelModel labelModel)
        {
            try
            {
                LabelEntity label = new LabelEntity();
                label.Label = labelModel.Label;
                label.NoteId = noteid;
                label.UserId = userid;
                fundoDbContext.Labels.Add(label);
                var result = fundoDbContext.SaveChanges();
                if (result > 0)
                {
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public LabelEntity GetLabel(int userid, long noteid)
        {
            LabelEntity get = fundoDbContext.Labels.Where(x => x.UserId == userid && x.NoteId == noteid).FirstOrDefault();
            if (get != null)
            {
                return get;
            }
            else
            {
                return null;
            }
        }


        public bool UpdateDetails(int userId, long noteid,labelModel labelModel)
        {
            try
            {
                var update = fundoDbContext.Labels.FirstOrDefault(y => y.UserId == userId && y.NoteId == noteid);
                if (update != null)
                {
                    update.Label = labelModel.Label;
                    fundoDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDetails(int userId, long noteid)
        {
            try
            {
                var update = fundoDbContext.Labels.FirstOrDefault(y => y.UserId == userId && y.NoteId == noteid);
                if (update != null)
                {
                    fundoDbContext.Labels.Remove(update);
                    fundoDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}



