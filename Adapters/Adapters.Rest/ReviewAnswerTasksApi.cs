using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Adapters.Generated.Rest.Controllers;
using Adapters.Generated.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using Synion.CQRS.Abstractions;
using Domain.Abstractions.Ports.Input;

namespace Adapters.Rest
{
    public class ReviewAnswerTasksApi : ReviewAnswerTasksApiController
    {
        private readonly IMediator mediator;

        public ReviewAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public override async Task<IActionResult> GetReviewAnswerTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = new GetReviewAnswerTaskUseCase
            (
                userTaskId: taskId
            );

            var response = await mediator.Send(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> AcceptAnswer([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] AcceptAnswer acceptAnswer)
        {
            var command = new AcceptAnswerUseCase
            (
                commandId: acceptAnswer.CommandId,
                questionId: acceptAnswer.QuestionId,
                userTaskId: taskId
            );

            await mediator.Send(command);
            return this.Accepted();
        }

        public override async Task<IActionResult> RejectAnswer([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] RejectAnswer rejectAnswer)
        {
            var command = new RejectAnswerUseCase
            (
                commandId: rejectAnswer.CommandId, 
                questionId: rejectAnswer.QuestionId,
                userTaskId: taskId, 
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
                TaskId = response.UserTaskId,
                RecievedOn = response.AskedOn,
                Subject = response.Subject,
                Question = response.Question,
                Sender = response.AskedBy,
                Answer = response.Answer
            };
        }
    }
}