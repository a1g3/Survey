using System.Collections.Generic;

namespace Survey.Models
{
    public class QuestionViewModelOut
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
    }
}
