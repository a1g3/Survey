namespace Survey.Domain.Interfaces.Services
{
    public interface IUserProgressService
    {
        int GetCurrentPartNumber(string userId);
        int GetAnsweredQuestionCount(string userId);
        void UpdatePartNumber(string userId, int newPartNumber);
    }
}
