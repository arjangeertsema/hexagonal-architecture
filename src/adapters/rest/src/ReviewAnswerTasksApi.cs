using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Reference.Adapters.Rest.Generated.Controllers;
using Reference.Adapters.Rest.Generated.Models;
using Microsoft.AspNetCore.Mvc;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Adapters.Rest
{
    public class ReviewAnswerTasksApi : ReviewAnswerTasksApiController
    {
        private readonly IMediator mediator;

        public ReviewAnswerTasksApi(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        public override async Task<IActionResult> GetReviewAnswerTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = new GetReviewAnswerTaskUseCase
            (
                taskId: taskId
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
                taskId: taskId
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
                taskId: taskId, 
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
                TaskId = response.TaskId,
                RecievedOn = response.AskedOn,
                Subject = response.Subject,
                Question = response.Question,
                Sender = response.AskedBy,
                Answer = response.Answer
            };
        }
    }
}