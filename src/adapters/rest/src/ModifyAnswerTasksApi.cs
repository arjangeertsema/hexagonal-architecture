using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Adapters.Rest.Generated.Controllers;
using Reference.Adapters.Rest.Generated.Models;
using Microsoft.AspNetCore.Mvc;

namespace Reference.Adapters.Rest
{
    public class ModifyAnswerTasksApi : ModifyAnswerTasksApiController
    {
        private readonly IGetModifyAnswerTaskUseCase getModifyAnswerTaskUseCase;
        private readonly IModifyAnswerUseCase modifyAnswerUseCase;

        public ModifyAnswerTasksApi(
            IGetModifyAnswerTaskUseCase getModifyAnswerTaskUseCase,
            IModifyAnswerUseCase modifyAnswerUseCase)
        {
            if (getModifyAnswerTaskUseCase is null)
            {
                throw new ArgumentNullException(nameof(getModifyAnswerTaskUseCase));
            }

            if (modifyAnswerUseCase is null)
            {
                throw new ArgumentNullException(nameof(modifyAnswerUseCase));
            }

            this.getModifyAnswerTaskUseCase = getModifyAnswerTaskUseCase;
            this.modifyAnswerUseCase = modifyAnswerUseCase;
        }

        public override async Task<IActionResult> GetModifyAnswerTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = Map(taskId);
            var response = await this.getModifyAnswerTaskUseCase.Execute(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> ModifyAnswer([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] ModifyAnswer modifyAnswer)
        {
            var command = Map(modifyAnswer);
            await this.modifyAnswerUseCase.Execute(command);
            return this.Accepted();
        }
        
        private IGetModifyAnswerTaskUseCase.Query Map(long taskId)
        {
            throw new NotImplementedException();
        }

        private ModifyAnswerTask Map(IGetModifyAnswerTaskUseCase.Response response)
        {
            throw new NotImplementedException();
        }

        private IModifyAnswerUseCase.Command Map(ModifyAnswer modifyAnswer)
        {
            throw new NotImplementedException();
        }
    }
}