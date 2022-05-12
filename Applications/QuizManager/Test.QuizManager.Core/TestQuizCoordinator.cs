using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuizManager.Core.BusinessLogic;
using Models = QuizManager.Core.Models;
using Entities = QuizManager.Data.Entities;
using QuizManager.Data.UnitOfWork;
using QuizManager.Data.Repositories;
using System.Linq.Expressions;
using QuizManager.Core.Exceptions;

namespace Test.QuizManager.Core
{
    [TestClass]
    public class TestQuizCoordinator
    {
        private QuizCoordinator _coordinator;
        private Mock<IMapper> _mapper;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGenericRepository<Entities.Question>> _questionRepository;
        private Mock<IGenericRepository<Entities.Quiz>> _quizRepository;
        private Mock<IGenericRepository<Entities.Answer>> _answerRepository;

        [TestInitialize]
        public void Setup()
        {
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _questionRepository = new Mock<IGenericRepository<Entities.Question>>();
            _quizRepository = new Mock<IGenericRepository<Entities.Quiz>>();
            _answerRepository = new Mock<IGenericRepository<Entities.Answer>>();
            _coordinator = new QuizCoordinator(_mapper.Object, _unitOfWork.Object);

            _unitOfWork.Setup(u => u.QuestionRespository)
                .Returns(_questionRepository.Object);
            _unitOfWork.Setup(u => u.QuizRepository)
                .Returns(_quizRepository.Object);
            _unitOfWork.Setup(u => u.AnswerRepository)
                .Returns(_answerRepository.Object);

            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            _quizRepository.Setup(q => q.Create(It.IsAny<Entities.Quiz>()))
                .Returns<Entities.Quiz>(q => q);

            _mapper.Setup(m => m.Map<Models.Quiz>(It.IsAny<Entities.Quiz>()))
                .Returns<Entities.Quiz>(x => new Models.Quiz
                {
                    Id = x.Id,
                    Title = x.Title,
                });
            _mapper.Setup(m => m.Map<Entities.Quiz>(It.IsAny<Models.Quiz>()))
                .Returns<Models.Quiz>(x => new Entities.Quiz
                {
                    Id = x.Id,
                    Title = x.Title,
                });
        }

        [TestMethod]
        public void TestCreateQuiz_NullTitle()
        {
            Assert.ThrowsException<InvalidQuizException>(() => _coordinator.CreateQuiz(null));
        }

        [TestMethod]
        public void TestCreateQuiz_EmptyTitle()
        {
            Assert.ThrowsException<InvalidQuizException>(() => _coordinator.CreateQuiz(""));
        }

        [TestMethod]
        public void TestCreateQuiz_WhitespaceTitle()
        {
            Assert.ThrowsException<InvalidQuizException>(() => _coordinator.CreateQuiz("  "));
        }

        [TestMethod]
        public void TestCreateQuiz_ValidTitle()
        {
            var createdQuiz = _coordinator.CreateQuiz("ABCDE");
            _quizRepository.Verify(q => q.Create(It.IsAny<Entities.Quiz>()), Times.Once());
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once());
            Assert.AreEqual("ABCDE", createdQuiz.Title);
        }

        [TestMethod]
        public void TestDeleteQuiz_IdFound()
        {
            _coordinator.DeleteQuiz(1);
            _quizRepository.Verify(q => q.Delete(It.IsAny<Entities.Quiz>()), Times.Once());
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void TestGetQuizById_IdNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() => _coordinator.DeleteQuiz(2));
        }

        [TestMethod]
        public void TestGetQuizById_IdFound()
        {
            _coordinator.GetQuizById(1);
            _quizRepository.Verify(q => q.Find(1), Times.Once());
        }

        [TestMethod]
        public void TestGetQuizzes_NullMatchTitle()
        {
            _coordinator.GetQuizzes(10, 2, "ABCDE");
            _quizRepository.Verify(
                q => q.Get(20, 10, It.IsAny <Expression<Func<Entities.Quiz, bool>>>()), 
                Times.Once());
        }

        [TestMethod]
        public void TestUpdateTitle_NullTitle()
        {            
            Assert.ThrowsException<InvalidQuizException>(() => _coordinator.UpdateTitle(1, null));
        }

        [TestMethod]
        public void TestUpdateTitle_WhitespaceTitle()
        {
            Assert.ThrowsException<InvalidQuizException>(() => _coordinator.UpdateTitle(1, "  "));
        }

        [TestMethod]
        public void TestUpdateTitle_ValidParams()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            _coordinator.UpdateTitle(1, "XYZ");
            _quizRepository.Verify(
                q => q.Update(It.Is<Entities.Quiz>(q => q.Title == "XYZ")), 
                Times.Once());
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once());
        }
    }
}
