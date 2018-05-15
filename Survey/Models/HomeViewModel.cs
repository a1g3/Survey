using System;
using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
    public class HomeViewModel
    {
        [Required, DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required, DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
