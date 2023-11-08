using Common_Layer.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class CollabratorRepo : ICollabratorRepo
    {
        private readonly FundoDbContext fundoDbContext;
        public CollabratorRepo(FundoDbContext fundoDbContext)
        {
            this.fundoDbContext = fundoDbContext;
        }

        public CollabratorEntity AddCollabrator(int userid,long noteid,  string Email)
        {
            try
            {
                CollabratorEntity col = new CollabratorEntity();
                col.UserId = userid;
                col.NoteId = noteid;
                var result = fundoDbContext.Users.FirstOrDefault(x => x.Email == Email);
                if (result != null)
                {
                    col.email = Email;
                    fundoDbContext.Add(col);
                    var check = fundoDbContext.SaveChanges();
                    if (check > 0)
                    {
                        return col;
                    }
                    else
                    {
                        return null;
                    }
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

        public CollabratorEntity DeleteCollabrator(int userid,string email)
        {
            try
            {
                var result = fundoDbContext.Collaborators.FirstOrDefault(x => x.UserId == userid && x.email==x.email);
                if(result !=null)
                {
                    fundoDbContext.Collaborators.Remove(result);
                    var remove=fundoDbContext.SaveChanges();
                    if (remove > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CollabratorEntity> GetCollabrator()
        {
            try
            {
                List<CollabratorEntity> list = (List<CollabratorEntity>)fundoDbContext.Collaborators.ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
