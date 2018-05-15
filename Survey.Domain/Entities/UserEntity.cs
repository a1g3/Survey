using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Domain.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }

        public virtual ICollection<QuestionEntity> Questions { get; set; }
    }
}
