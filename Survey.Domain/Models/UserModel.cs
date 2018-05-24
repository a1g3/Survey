using System;

namespace Survey.Domain.Models
{
    public class UserModel
    {
        public string UserId { get; }
        public string Name { get; }
        public DateTime BirthDate { get; }

        public UserModel(string name, DateTime birthDate)
        {
            this.UserId = Guid.NewGuid().ToString();
            this.Name = name;
            this.BirthDate = birthDate;
        }
    }
}
