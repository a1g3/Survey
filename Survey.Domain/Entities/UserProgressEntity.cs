using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Domain.Entities
{
    [Table("User_Progress")]
    public class UserProgressEntity
    {
        [Key]
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        [Required]
        public int PartNumber { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        [ForeignKey("QuestionId")]
        public virtual QuestionEntity Question { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
