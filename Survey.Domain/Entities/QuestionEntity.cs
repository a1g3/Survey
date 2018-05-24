using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Domain.Entities
{
    [Table("Questions")]
    public class QuestionEntity
    {
        [Key]
        [Required]
        public string QuestionId { get; set; }

        [Required]
        public string Question { get; set; }
        [Required]
        public string Options { get; set; }
        public string Response { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}
