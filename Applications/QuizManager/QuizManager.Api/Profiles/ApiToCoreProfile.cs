using AutoMapper;
using Models = QuizManager.Core.Models;

namespace QuizManager.Api.Profiles
{
    public class ApiToCoreProfile : Profile
    {
        public ApiToCoreProfile()
        {
            CreateMap<Contracts.Answer, Models.Answer>().ReverseMap();
            CreateMap<Contracts.QuestionSummary, Models.Question>().ReverseMap();
            CreateMap<Contracts.QuestionDetails, Models.Question>().ReverseMap();

            CreateMap<Models.Question, Contracts.QuestionWithAnswers>()
                .ForMember(c => c.Details, m => m.MapFrom(m => m));

            CreateMap<Contracts.QuestionWithAnswers, Models.Question>();

            CreateMap<Contracts.Quiz, Models.Quiz>().ReverseMap();
            CreateMap<Contracts.QuizSummary, Models.Quiz>().ReverseMap();
            CreateMap<Models.Quiz, Contracts.QuizSummary>().ReverseMap();
        }
    }
}
