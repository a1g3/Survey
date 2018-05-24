using Survey.Domain.Models;

namespace Survey.Domain.Interfaces.Services
{
    public interface IRegistrationService
    {
        void RegisterUser(UserModel registrationModel);
    }
}
