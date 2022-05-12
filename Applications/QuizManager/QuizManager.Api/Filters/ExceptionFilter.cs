using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizManager.Core.Exceptions;

namespace QuizManager.Api.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodeMap = 
            new Dictionary<Type, HttpStatusCode>()
            {
                { typeof(ArgumentException), HttpStatusCode.BadRequest },
                { typeof(InvalidQuizException), HttpStatusCode.BadRequest },
                { typeof(InvalidQuestionException), HttpStatusCode.BadRequest },
                { typeof(InvalidAnswerException), HttpStatusCode.BadRequest },
                { typeof(NotFoundException), HttpStatusCode.BadRequest },
                { typeof(NotImplementedException), HttpStatusCode.InternalServerError }
            };

        public int Order => int.MaxValue - 10;

        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            // Deliberately blank
        }

        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
            var exceptionType = context.Exception?.GetType();

            if (exceptionType != null && ExceptionStatusCodeMap.ContainsKey(exceptionType))
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int)ExceptionStatusCodeMap[exceptionType]
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
