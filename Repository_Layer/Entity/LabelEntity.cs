using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Label { get; set; }
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
