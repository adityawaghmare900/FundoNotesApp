using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId {  get; set; }
        public string title {  get; set; }
        public string Note {  get; set; }
        public DateTime? Reminder {  get; set; }
        public string color {  get; set; }
        public string Image {  get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin {  get; set; }
        public bool IsTrash {  get; set; }
        public DateTime? Createat { get; set; }
        public DateTime? Updateat { get; set;}

        [ForeignKey("Users")]
        public int UserId  { get; set; }
        [JsonIgnore]
        public virtual UserEntity Users { get; set; }
    }
}
