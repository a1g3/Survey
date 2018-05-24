namespace Survey.Domain.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        int GetAnsweredQuestionCount(string userId);
    }
}
