using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;
using System.Threading;
using Adapters.Rest.Generated.Controllers;
using Adapters.Rest.Generated.Models;

namespace Adapters.Rest
{
    public class AnswerQuestionTasksApi : AnswerQuestionTasksApiController
    {
        private readonly IMediator mediator;

        public AnswerQuestionTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public override async Task<IActionResult> GetAnswerQuestionTask([FromRoute(Name = "task_id"), Required] string userTaskId)
        {
            var query = new GetAnswerQuestionTaskUseCase
            (
                userTaskId: userTaskId
            );
            
            var response = await this.mediator.Ask(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> AnswerQuestion([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AnswerQuestion answerQuestion)
        {
            var command = new AnswerQuestionUseCase
            (
                commandId: answerQuestion.CommandId,
                questionId: answerQuestion.QuestionId,
                userTaskId: userTaskId, answer: answerQuestion.Answer
            );

            await this.mediator.Send(command);
            return this.Accepted();
        }

        private AnswerQuestionTask Map(GetAnswerQuestionTaskUseCase.Response response)
        {
            return new AnswerQuestionTask()
            {
                UserTaskId = response.UserTaskId,
                QuestionId = response.QuestionId,
                RecievedOn = response.AskedOn,
                Subject = response.Subject,
                Question = response.Question,
                Sender = response.AskedBy
            };
        }
    }
}