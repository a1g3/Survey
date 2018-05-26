using AutoMapper;
using Survey.Domain.Entities;
using Survey.Domain.Interfaces.Infastructure;
using Survey.Domain.Interfaces.Services;
using Survey.Domain.Models;

namespace Survey.Domain.Services
{
    public class RegistrationService : IRegistrationService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public void RegisterUser(UserModel registrationModel)
        {
            var user = Mapper.Map<UserEntity>(registrationModel);
            var userProgress = new UserProgressEntity() { QuestionNumber = 0, User = user, UserId = user.UserId, PartNumber = 0};
            UnitOfWork.UserProgressRepository.Insert(userProgress);
            UnitOfWork.UserRepository.Insert(user);
            UnitOfWork.Commit();
        }
    }
}
