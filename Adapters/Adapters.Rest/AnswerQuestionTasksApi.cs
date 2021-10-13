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

        public override async Task<IActionResult> GetAnswerQuestionTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = new GetAnswerQuestionTaskUseCase
            (
                taskId: taskId
            );
            
            var response = await this.mediator.Ask(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> AnswerQuestion([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] AnswerQuestion answerQuestion)
        {
            var command = new AnswerQuestionUseCase
            (
                commandId: answerQuestion.CommandId,
                questionId: answerQuestion.QuestionId,
                userTaskId: taskId, answer: answerQuestion.Answer
            );

            await this.mediator.Send(command);
            return this.Accepted();
        }

        private AnswerQuestionTask Map(GetAnswerQuestionTaskUseCase.Response response)
        {
            return new AnswerQuestionTask()
            {
                TaskId = response.UserTaskId,
                QuestionId = response.QuestionId,
                RecievedOn = response.AskedOn,
                Subject = response.Subject,
                Question = response.Question,
                Sender = response.AskedBy
            };
        }
    }
}