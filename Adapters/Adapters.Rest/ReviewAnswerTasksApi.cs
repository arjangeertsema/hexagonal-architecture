using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Adapters.Rest.Generated.Controllers;
using Adapters.Rest.Generated.Models;
using Microsoft.AspNetCore.Mvc;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;

namespace Adapters.Rest
{
    public class ReviewAnswerTasksApi : ReviewAnswerTasksApiController
    {
        private readonly IMediator mediator;

        public ReviewAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public override async Task<IActionResult> GetReviewAnswerTask([FromRoute(Name = "task_id"), Required] string userTaskId)
        {
            var query = new GetReviewAnswerTaskUseCase
            (
                userTaskId: userTaskId
            );

            var response = await mediator.Ask(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> AcceptAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] AcceptAnswer acceptAnswer)
        {
            var command = new AcceptAnswerUseCase
            (
                commandId: acceptAnswer.CommandId,
                questionId: acceptAnswer.QuestionId,
                userTaskId: userTaskId
            );

            await mediator.Send(command);
            return this.Accepted();
        }

        public override async Task<IActionResult> RejectAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] RejectAnswer rejectAnswer)
        {
            var command = new RejectAnswerUseCase
            (
                commandId: rejectAnswer.CommandId, 
                questionId: rejectAnswer.QuestionId,
                userTaskId: userTaskId, 
                rejection: rejectAnswer.Rejection
            );

            await mediator.Send(command);
            return this.Accepted();
        }

        private ReviewAnswerTask Map(GetReviewAnswerTaskUseCase.Response response)
        {
            return new ReviewAnswerTask()
            {
                QuestionId = response.QuestionId,
                UserTaskId = response.UserTaskId,
                RecievedOn = response.AskedOn,
                Subject = response.Subject,
                Question = response.Question,
                Sender = response.AskedBy,
                Answer = response.Answer
            };
        }
    }
}