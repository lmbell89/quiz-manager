using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using QuizManager.Core.Exceptions;
using QuizManager.Core.Helpers;
using QuizManager.Core.Models;
using QuizManager.Data.UnitOfWork;

namespace QuizManager.Core.BusinessLogic
{
    public class QuestionCoordinator : IQuestionCoordinator
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionCoordinator(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Question CreateQuestion(Question question)
        {
            Validators.ValidateQuestion(question);
            foreach (Answer answer in question.Answers)
            {
                Validators.ValidateAnswer(answer);
            }
            if (question.Id != null)
            {
                throw new ArgumentException(
                    "Id must be null when creating a question",
                    nameof(question));
            }

            var quiz = _unitOfWork.QuizRepository.Find(question.QuizId);
            if (quiz == null)
            {
                throw new NotFoundException($"No quiz found with id {question.QuizId}");
            }

            var entity = _mapper.Map<Data.Entities.Question>(question);

            // double all indexes then subtract 1 from the new question index
            // this prevents indexes colliding
            DoubleQuestionIndexes(question.QuizId);
            question.Index = question.Index * 2 - 1;

            Data.Entities.Question createdEntity = 
                _unitOfWork.QuestionRespository.Create(entity);

            // reindex the questions to undo the doubling
            ReIndexQuestions(question.QuizId);

            _unitOfWork.SaveChanges();
            return _mapper.Map<Question>(createdEntity);
        }

        public void DeleteQuestion(int id)
        {
            var question = GetQuestionEntityById(id);
            var quiz = _unitOfWork.QuizRepository.Find(question.QuizId);
            _unitOfWork.QuestionRespository.Delete(question);
            ReIndexQuestions(quiz.Id);
            _unitOfWork.SaveChanges();
        }

        public Question GetQuestionById(int id)
        {
            var entity = GetQuestionEntityById(id);
            return _mapper.Map<Question>(entity);
        }

        public Question UpdateQuestion(int id, Question question)
        {
            Validators.ValidateQuestion(question);

            Data.Entities.Question entity = GetQuestionEntityById(id);
            IEnumerable<int> currentAnswerIds = question.Answers.Select(a => (int)a.Id);

            foreach (Data.Entities.Answer answerEntity in entity.Answers)
            {
                if (currentAnswerIds.Contains(answerEntity.Id))
                {
                    Answer answerModel = question.Answers.First(a => a.Id == answerEntity.Id);
                    answerEntity.Text = answerModel.Text;
                    answerEntity.IsCorrect = answerModel.IsCorrect;
                }
                else
                {
                    entity.Answers.Remove(answerEntity);
                }
            }

            foreach (Answer answer in question.Answers)
            {
                if (answer.Id == null || answer.Id == 0)
                {
                    var answerEntity = _mapper.Map<Data.Entities.Answer>(answer);
                    entity.Answers.Add(answerEntity);
                }
            }

            if (entity.Index != question.Index)
            {
                DoubleQuestionIndexes(question.QuizId);
                entity.Index = question.Index * 2 - 1;
                ReIndexQuestions(question.QuizId);
            }

            entity.Text = question.Text;
            _unitOfWork.QuestionRespository.Update(entity);

            _unitOfWork.SaveChanges();
            return GetQuestionById(id);
        }

        private Data.Entities.Question GetQuestionEntityById(int id)
        {
            var question = _unitOfWork.QuestionRespository.Find(id);
            if (question == null)
            {
                throw new NotFoundException($"No question found with id {id}");
            }
            return question;
        }

        private void ReIndexQuestions(int quizId)
        {
            IEnumerable<Data.Entities.Question> questions = 
                _unitOfWork.QuestionRespository.Get(null, null, q => q.QuizId == quizId);

            int index = 1;
            questions = questions.OrderBy(q => q.Id);
            foreach (Data.Entities.Question question in questions)
            {
                question.Index = index;
                index++;
                _unitOfWork.QuestionRespository.Update(question);
            }
        }

        private void DoubleQuestionIndexes(int quizId)
        {
            IEnumerable<Data.Entities.Question> questions =
                _unitOfWork.QuestionRespository.Get(null, null, q => q.QuizId == quizId);

            foreach (Data.Entities.Question question in questions)
            {
                question.Index *= 2;
                _unitOfWork.QuestionRespository.Update(question);
            }
        }
    }
}
