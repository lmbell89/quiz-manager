using AutoMapper;
using Models = QuizManager.Core.Models;
using Entities = QuizManager.Data.Entities;

namespace QuizManager.Api.Profiles
{
    public class CoreToDataProfile : Profile
    {
        public CoreToDataProfile()
        {
            CreateMap<Models.Quiz, Entities.Quiz>().ReverseMap();
            CreateMap<Models.Question, Entities.Question>().ReverseMap();
            CreateMap<Models.Answer, Entities.Answer>().ReverseMap();
        }
    }
}
