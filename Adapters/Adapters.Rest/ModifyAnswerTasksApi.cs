using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Adapters.Rest.Generated.Controllers;
using Adapters.Rest.Generated.Models;
using Microsoft.AspNetCore.Mvc;
using Common.CQRS.Abstractions;
using Domain.Abstractions.UseCases;

namespace Adapters.Rest
{
    public class ModifyAnswerTasksApi : ModifyAnswerTasksApiController
    {
        private readonly IMediator mediator;

        public ModifyAnswerTasksApi(IMediator mediator) => this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        public override async Task<IActionResult> GetModifyAnswerTask([FromRoute(Name = "task_id"), Required] string userTaskId)
        {
            var query = new GetModifyAnswerTaskUseCase
            (
                userTaskId: userTaskId
            );

            var response = await this.mediator.Ask(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> ModifyAnswer([FromRoute(Name = "task_id"), Required] string userTaskId, [FromBody] ModifyAnswer modifyAnswer)
        {
            var command = new ModifyAnswerUseCase
            (
                commandId: modifyAnswer.CommandId,
                questionId: modifyAnswer.QuestionId,
                userTaskId: userTaskId, 
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
                UserTaskId = response.UserTaskId,
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