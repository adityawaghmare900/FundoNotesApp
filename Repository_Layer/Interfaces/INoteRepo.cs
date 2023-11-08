using Common_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface INoteRepo
    {
        public NoteEntity AddNote(NoteAddModel note, int userId);
        
        public List<NoteEntity> GetList();

        public bool UpdateNoteModel(long noteid, UpdateNoteModel model);

        public bool deleteNote(long noteId, long userId);

        public bool IsPin(long noteId, long userId);

        public bool IsArchieve(long noteId, long userId);

        public bool IsTrash(long noteId, long userId);

        public bool DeleteForever(long noteId, long userid);

        public NoteEntity Color(long noteId, long userid, string color);

        public NoteEntity Reminder(long noteId, long userid, DateTime reminder);

        public List<NoteEntity> SerachNoteByDate(DateTime date);

        public List<NoteEntity> SerachNoteByTitle(string title);





    }
}
