using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using example.domain.abstractions.ports.input;
using example.adapters.rest.generated.Controllers;
using example.adapters.rest.generated.Models;
using Microsoft.AspNetCore.Mvc;

namespace example.adapters.rest
{
    public class AnswerQuestionTasksApi : AnswerQuestionTasksApiController
    {
        private readonly IGetAnswerQuestionTaskUseCase getAnswerQuestionTaskUseCase;
        private readonly IAnswerQuestionUseCase answerQuestionUseCase;

        public AnswerQuestionTasksApi(
            IGetAnswerQuestionTaskUseCase getAnswerQuestionTaskUseCase,
            IAnswerQuestionUseCase answerQuestionUseCase)
        {
            if (getAnswerQuestionTaskUseCase is null)
            {
                throw new ArgumentNullException(nameof(getAnswerQuestionTaskUseCase));
            }

            if (answerQuestionUseCase is null)
            {
                throw new ArgumentNullException(nameof(answerQuestionUseCase));
            }

            this.getAnswerQuestionTaskUseCase = getAnswerQuestionTaskUseCase;
            this.answerQuestionUseCase = answerQuestionUseCase;
        }

        public override async Task<IActionResult> GetAnswerQuestionTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = Map(taskId);
            var response = await this.getAnswerQuestionTaskUseCase.Execute(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> AnswerQuestion([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] AnswerQuestion answerQuestion)
        {
            var command = Map(answerQuestion);
            await this.answerQuestionUseCase.Execute(command);
            return this.Accepted();
        }

        private IGetAnswerQuestionTaskUseCase.Query Map(long taskId)
        {
            throw new NotImplementedException();
        }

        private AnswerQuestionTask Map(IGetAnswerQuestionTaskUseCase.Response response)
        {
            throw new NotImplementedException();
        }

        private IAnswerQuestionUseCase.Command Map(AnswerQuestion answerQuestion)
        {
            throw new NotImplementedException();
        }
    }
}