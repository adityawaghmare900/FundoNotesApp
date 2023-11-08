using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class CollabratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string email {  get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity Users { get; set; }

        [ForeignKey("Notes")]
        public long NoteId { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Notes { get; set; }
    }
}
