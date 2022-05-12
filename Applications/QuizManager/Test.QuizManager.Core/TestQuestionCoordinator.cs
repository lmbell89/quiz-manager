using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuizManager.Core.BusinessLogic;
using QuizManager.Data.UnitOfWork;
using Models = QuizManager.Core.Models;
using Entities = QuizManager.Data.Entities;
using QuizManager.Data.Repositories;
using System;
using QuizManager.Core.Exceptions;

namespace Test.QuizManager.Core
{
    [TestClass]
    public class TestQuestionCoordinator
    {
        private QuestionCoordinator _coordinator;
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
            _coordinator = new QuestionCoordinator(_mapper.Object, _unitOfWork.Object);

            _unitOfWork.Setup(u => u.QuestionRespository)
                .Returns(_questionRepository.Object);
            _unitOfWork.Setup(u => u.QuizRepository)
                .Returns(_quizRepository.Object);
            _unitOfWork.Setup(u => u.AnswerRepository)
                .Returns(_answerRepository.Object);
        }

        [TestMethod]
        public void TestCreateQuestion_NoAnswers()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                Text = "ABCDE",
                QuizId = 1
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_TwoAnswers()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                Text = "ABCDE",
                QuizId = 1,
                Answers = new Models.Answer[]
                {
                    new Models.Answer(),
                    new Models.Answer(),
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_SixAnswers()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                Text = "ABCDE",
                QuizId = 1,
                Answers = new Models.Answer[]
                {
                    new Models.Answer(),
                    new Models.Answer(),
                    new Models.Answer(),
                    new Models.Answer(),
                    new Models.Answer(),
                    new Models.Answer(),
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_AllAnswersIncorrect()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                Text = "ABCDE",
                QuizId = 1,
                Answers = new Models.Answer[]
                {
                    new Models.Answer { IsCorrect = false },
                    new Models.Answer { IsCorrect = false },
                    new Models.Answer { IsCorrect = false },
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_AllAnswersCorrect()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                Text = "ABCDE",
                QuizId = 1,
                Answers = new Models.Answer[]
                {
                    new Models.Answer { IsCorrect = true },
                    new Models.Answer { IsCorrect = true },
                    new Models.Answer { IsCorrect = true },
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_NullText()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                QuizId = 1,
                Answers = new Models.Answer[]
                {
                    new Models.Answer { IsCorrect = true },
                    new Models.Answer { IsCorrect = false },
                    new Models.Answer { IsCorrect = false },
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_WhiteSpaceText()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                QuizId = 1,
                Text = "    ",
                Answers = new Models.Answer[]
                {
                    new Models.Answer { IsCorrect = true },
                    new Models.Answer { IsCorrect = false },
                    new Models.Answer { IsCorrect = false },
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_IdProvided()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                Id = 10,
                QuizId = 1,
                Text = "ABCDE",
                Answers = new Models.Answer[]
                {
                    new Models.Answer {
                        IsCorrect = true,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = true,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = true,
                        Text = "XYZ"
                    },
                }
            };
            Assert.ThrowsException<InvalidQuestionException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_QuizNotFound()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                QuizId = 2,
                Text = "ABCDE",
                Answers = new Models.Answer[]
                {
                    new Models.Answer {
                        IsCorrect = true,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = false,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = false,
                        Text = "XYZ"
                    },
                }
            };
            Assert.ThrowsException<NotFoundException>(() => _coordinator.CreateQuestion(question));
        }

        public void TestCreateQuestion_NegativeIndex()
        {
            _quizRepository.Setup(q => q.Find(1)).Returns(new Entities.Quiz());
            var question = new Models.Question
            {
                QuizId = 2,
                Text = "ABCDE",
                Index = -1,
                Answers = new Models.Answer[]
                {
                    new Models.Answer {
                        IsCorrect = true,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = false,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = false,
                        Text = "XYZ"
                    },
                }
            };
            Assert.ThrowsException<NotFoundException>(() => _coordinator.CreateQuestion(question));
        }

        [TestMethod]
        public void TestCreateQuestion_ValidParams()
        {
            var quiz = new Entities.Quiz
            {
                Id = 1,
                Title = "ABCDE",
                Questions = new Entities.Question[]
                {
                    new Entities.Question { Index = 0 },
                    new Entities.Question { Index = 1 },
                    new Entities.Question { Index = 2 },
                }
            };

            var question = new Models.Question
            {
                QuizId = 1,
                Text = "ABCDE",
                Index = 1,
                Answers = new Models.Answer[]
    {
                    new Models.Answer {
                        IsCorrect = true,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = false,
                        Text = "XYZ"
                    },
                    new Models.Answer {
                        IsCorrect = false,
                        Text = "XYZ"
                    },
    }
            };

            _quizRepository.Setup(q => q.Find(1)).Returns(quiz);

            _coordinator.CreateQuestion(question);

            _questionRepository.Verify(q => q.Update(It.IsAny<Entities.Question>()), Times.Exactly(2));
            _questionRepository.Verify(q => q.Create(It.IsAny<Entities.Question>()), Times.Once());
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void TestDeleteQuestion()
        {
            var quiz = new Entities.Quiz
            {
                Id = 1,
                Title = "ABCDE",
                Questions = new Entities.Question[]
                {
                    new Entities.Question { Index = 0 },
                    new Entities.Question { Index = 1 },
                    new Entities.Question { Index = 2 },
                    new Entities.Question { Index = 3 },
                    new Entities.Question { Index = 4 },
                }
            };

            var question = new Entities.Question
            {
                Id = 2,
                QuizId = 1,
                Index = 1
            };

            _quizRepository.Setup(q => q.Find(1)).Returns(quiz);
            _questionRepository.Setup(q => q.Find(2)).Returns(question);

            _coordinator.DeleteQuestion(2);

            _questionRepository.Verify(q => q.Update(It.IsAny<Entities.Question>()), Times.Exactly(3));
            _questionRepository.Verify(q => q.Delete(It.IsAny<Entities.Question>()), Times.Once());
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void TestGetQuestionById_IdNotFound()
        {
            Assert.ThrowsException<NotFoundException>(() => _coordinator.GetQuestionById(1));
        }

        [TestMethod]
        public void TestGetQuestionById_IdFound()
        {
            var questionEntity = new Entities.Question
            {
                Id = 1,
                Text = "ABCDE"
            };

            _questionRepository.Setup(q => q.Find(1)).Returns(questionEntity);
            _mapper.Setup(m => m.Map<Models.Question>(questionEntity))
                .Returns<Entities.Question>(entity => new Models.Question
                {
                    Id = entity.Id,
                    Text = entity.Text,
                });

            Models.Question result = _coordinator.GetQuestionById(1);
            Assert.AreEqual(questionEntity.Id, result.Id);
            Assert.AreEqual(questionEntity.Text, result.Text);
        }
    }
}
