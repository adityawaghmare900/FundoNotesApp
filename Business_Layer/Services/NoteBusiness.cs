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
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo noteRepo;

        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public NoteEntity AddNote(NoteAddModel note, int userId)
        {
            return noteRepo.AddNote(note, userId);
        }

        public List<NoteEntity> GetList()
        {
            return noteRepo.GetList();
        }

        public bool UpdateNoteModel(long noteid,  UpdateNoteModel model)
        {
            return noteRepo.UpdateNoteModel(noteid,model );
        }

        public bool deleteNote(long noteId, long userId)
        {
            return noteRepo.deleteNote(noteId,userId);
        }

        public bool IsPin(long noteId, long userId)
        {
            return noteRepo.IsPin(noteId,userId);
        }

        public bool IsArchieve(long noteId, long userId)
        {
            return noteRepo.IsArchieve(noteId,userId);
        }

        public bool IsTrash(long noteId, long userId)
        {
            return noteRepo.IsTrash(noteId,userId);
        }

        public bool DeleteForever(long noteId, long userid)
        {
            return noteRepo.DeleteForever(noteId, userid);
        }

        public NoteEntity Color(long noteId, long userid, string color)
        {
            return noteRepo.Color(noteId,userid,color);
        }

        public NoteEntity Reminder(long noteId, long userid, DateTime reminder)
        {
            return noteRepo.Reminder(noteId,userid,reminder);
        }

        public List<NoteEntity> SerachNoteByDate(DateTime date)
        {
            return noteRepo.SerachNoteByDate(date);
        }

        public List<NoteEntity> SerachNoteByTitle(string title)
        {
            return noteRepo.SerachNoteByTitle(title);
        }



    }
}
