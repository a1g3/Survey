using AutoMapper;
using Survey.Domain.Entities;
using Survey.Domain.Models;
using Survey.Models;

namespace Survey.Helpers
{
    public static class AutoMapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<QuestionModel, QuestionViewModel>();
                cfg.CreateMap<UserModel, UserEntity>();
            });
        }
    }
}
