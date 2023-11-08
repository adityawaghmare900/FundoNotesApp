using Common_Layer.Models;
using Microsoft.VisualBasic;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository_Layer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly FundoDbContext fundoDbContext;

        public NoteRepo(FundoDbContext fundoDbContext)
        {
            this.fundoDbContext = fundoDbContext;
        }

        public NoteEntity AddNote(NoteAddModel note, int userId)
        {
            NoteEntity entity = new NoteEntity();
            try
            {
                entity.title = note.title;
                entity.Note = note.Note;
                entity.Reminder = note.Reminder;
                entity.color = note.color;
                entity.Image = note.Image;
                entity.IsArchive = note.IsArchive;
                entity.IsPin = note.IsPin;
                entity.IsTrash = note.IsTrash;
                entity.Createat = DateTime.Now;
                entity.Updateat = DateTime.Now;
                entity.UserId=userId;

                fundoDbContext.Notes.Add(entity);
                var result = fundoDbContext.SaveChanges();
                if (result > 0)
                {
                    return entity;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<NoteEntity> GetList()
        {
            try
            {
                List<NoteEntity> result = (List<NoteEntity>)fundoDbContext.Notes.ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdateNoteModel(long noteid, UpdateNoteModel model)
        {
            try
            {
                var Check = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteid);
                if (Check != null)
                {
                    Check.title = model.title;
                    Check.Note = model.Note;
                    Check.Reminder = model.Reminder;
                    Check.color = model.color;
                    Check.Image = model.Image;
                    Check.IsArchive = model.IsArchive;
                    Check.IsPin = model.IsPin;
                    Check.IsTrash = model.IsTrash;
                    
                    Check.Updateat = DateTime.Now;
                    fundoDbContext.SaveChanges();
                    return true;                   
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool deleteNote(long noteId, long userId)
        {
            try
            {
                var check = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (check != null)
                {
                    fundoDbContext.Notes.Remove(check);
                    fundoDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool IsPin(long noteId, long userId)
        {
            try
            {
                var check=fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
                if (check.IsPin == false)
                {
                    check.IsPin = true;
                    fundoDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    check.IsPin = false;
                    fundoDbContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsArchieve(long noteId, long userId)
        {
            var check = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
            try
            {
                    if (check != null)
                    {
                        if (check.IsArchive == false)
                        {
                            check.IsArchive = true;
                            fundoDbContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            check.IsArchive = false;
                            fundoDbContext.SaveChanges();
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
            }
                
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsTrash(long noteId, long userId)
        {
            var check = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
            try
            {
                    if (check != null)
                    {
                        if (check.IsTrash == false)
                        {
                            check.IsTrash = true;
                            fundoDbContext.SaveChanges();
                            return true;
                        }
                        else
                        {
                            check.IsTrash = false;
                            fundoDbContext.SaveChanges();
                            return false;
                        }
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

        public bool DeleteForever(long noteId,long userid)
        {
            try
            {
                NoteEntity note = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userid);
                if (note != null)
                {
                    if(note.IsTrash == true)
                    {
                        fundoDbContext.Remove(note);
                        fundoDbContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        note.IsTrash = true;
                        fundoDbContext.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            }

        public NoteEntity Color(long noteId,long userid, string color)
        {
            try
            {
                NoteEntity entity = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userid);
                if (entity != null)
                {
                    entity.color = color;
                    fundoDbContext.SaveChanges ();
                    return entity;
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

        public NoteEntity Reminder(long noteId, long userid, DateTime reminder)
        {
            try
            {
                NoteEntity entity = fundoDbContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userid);
                if (entity != null)
                {
                    entity.Reminder = reminder;
                    fundoDbContext.SaveChanges();
                    return entity;
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


        public List<NoteEntity> SerachNoteByDate(DateTime date)
        {
            try
            {
                List<NoteEntity> result = fundoDbContext.Notes.Where(x => x.Createat == date).ToList();
                if (result != null)
                {
                    return result;
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

        public List<NoteEntity> SerachNoteByTitle(string title)
        {
            try
            {
                List<NoteEntity> result = fundoDbContext.Notes.Where(x => x.title==title).ToList();
                if (result != null)
                {
                    return result;
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
            
    }
    }


