using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using AutoMapper;
using QuizManager.Core.Exceptions;
using QuizManager.Core.Helpers;
using QuizManager.Core.Models;
using QuizManager.Data.UnitOfWork;

namespace QuizManager.Core.BusinessLogic
{
    public class QuizCoordinator : IQuizCoordinator
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QuizCoordinator(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Quiz CreateQuiz(string title)
        {
            var quiz = new Quiz { Title = title };
            Validators.ValidateQuiz(quiz);
            var entity = _mapper.Map<Data.Entities.Quiz>(quiz);
            var createdEntity = _unitOfWork.QuizRepository.Create(entity);
            _unitOfWork.SaveChanges();
            return _mapper.Map<Quiz>(createdEntity);
        }

        public void DeleteQuiz(int id)
        {
            var quiz = GetEntityById(id);
            _unitOfWork.QuizRepository.Delete(quiz);
            _unitOfWork.SaveChanges();
        }

        public Quiz GetQuizById(int id)
        {
            Data.Entities.Quiz quizEntity = GetEntityById(id);
            Quiz quiz = _mapper.Map<Quiz>(quizEntity);
            quiz.Questions = quiz.Questions.OrderBy(q => q.Id).ToArray();
            return quiz;
        }

        public IEnumerable<Quiz> GetQuizzes(
            int pageSize,
            int pageIndex,
            string matchTitle = null)
        {
            Expression<Func<Data.Entities.Quiz, bool>> predicate = q =>
                string.IsNullOrWhiteSpace(matchTitle) || q.Title.Contains(matchTitle);            
            int startIndex = pageIndex * pageSize;
            var quizEntities = _unitOfWork.QuizRepository.Get(startIndex, pageSize, predicate);
            return _mapper.Map<List<Quiz>>(quizEntities);
        }

        public Quiz UpdateTitle(int id, string title)
        {
            Data.Entities.Quiz quizEntity = GetEntityById(id);
            quizEntity.Title = title;

            Quiz quizModel = _mapper.Map<Quiz>(quizEntity);
            Validators.ValidateQuiz(quizModel);

            _unitOfWork.QuizRepository.Update(quizEntity);
            _unitOfWork.SaveChanges();

            var updatedQuiz = _unitOfWork.QuizRepository.Find(id);
            return _mapper.Map<Quiz>(updatedQuiz);
        }

        private Data.Entities.Quiz GetEntityById(int id)
        {
            var quiz = _unitOfWork.QuizRepository.Find(id);
            if (quiz == null)
            {
                throw new NotFoundException($"No quiz found with id {id}");
            }
            return quiz;
        }
    }
}
