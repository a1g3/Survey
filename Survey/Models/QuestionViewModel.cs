using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
    public class QuestionViewModel
    {
        public string userId { get; set; }
        public string Instructions { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        [Required]
        public string Response { get; set; }
    }
}
