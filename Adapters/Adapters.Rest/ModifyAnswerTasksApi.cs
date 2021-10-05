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
    public class ModifyAnswerTasksApi : ModifyAnswerTasksApiController
    {
        private readonly IMediator mediator;

        public ModifyAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public override async Task<IActionResult> GetModifyAnswerTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = new GetModifyAnswerTaskUseCase
            (
                userTaskId: taskId
            );

            var response = await this.mediator.Send(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> ModifyAnswer([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] ModifyAnswer modifyAnswer)
        {
            var command = new ModifyAnswerUseCase
            (
                commandId: modifyAnswer.CommandId,
                questionId: modifyAnswer.QuestionId,
                userTaskId: taskId, 
                answer: modifyAnswer.Answer
            );
            
            await this.mediator.Send(command);
            return this.Accepted();
        }

        private ModifyAnswerTask Map(GetModifyAnswerTaskUseCase.Response response)
        {
            return new ModifyAnswerTask()
            {
                QuestionId = response.QuestionId,
                TaskId = response.UserTaskId,
                RecievedOn = response.AskedOn,
                Subject = response.Subject,
                Question = response.Question,
                Sender = response.AskedBy,
                Answer = response.Answer,
                Rejection = response.Rejection
            };
        }
    }
}