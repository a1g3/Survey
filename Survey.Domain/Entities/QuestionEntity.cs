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
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        [Required]
        public int QuestionType { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Options { get; set; }

        public string Response { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
