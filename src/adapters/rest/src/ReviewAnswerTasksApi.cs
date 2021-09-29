using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Reference.Domain.Abstractions.Ports.Input;
using Reference.Adapters.Rest.Generated.Controllers;
using Reference.Adapters.Rest.Generated.Models;
using Microsoft.AspNetCore.Mvc;

namespace Reference.Adapters.Rest
{
    public class ReviewAnswerTasksApi : ReviewAnswerTasksApiController
    {
        private readonly IGetReviewAnswerTaskUseCase getReviewAnswerTaskUseCase;
        private readonly IAcceptAnswerUseCase acceptAnswerUseCase;
        private readonly IRejectAnswerUseCase rejectAnswerUseCase;

        public ReviewAnswerTasksApi(
            IGetReviewAnswerTaskUseCase getReviewAnswerTaskUseCase,
            IAcceptAnswerUseCase acceptAnswerUseCase,
            IRejectAnswerUseCase rejectAnswerUseCase)
        {
            if (getReviewAnswerTaskUseCase is null)
            {
                throw new ArgumentNullException(nameof(getReviewAnswerTaskUseCase));
            }

            if (acceptAnswerUseCase is null)
            {
                throw new ArgumentNullException(nameof(acceptAnswerUseCase));
            }

            if (rejectAnswerUseCase is null)
            {
                throw new ArgumentNullException(nameof(rejectAnswerUseCase));
            }

            this.getReviewAnswerTaskUseCase = getReviewAnswerTaskUseCase;
            this.acceptAnswerUseCase = acceptAnswerUseCase;
            this.rejectAnswerUseCase = rejectAnswerUseCase;
        }

        public override async Task<IActionResult> GetReviewAnswerTask([FromRoute(Name = "task_id"), Required] long taskId)
        {
            var query = Map(taskId);
            var response = await this.getReviewAnswerTaskUseCase.Execute(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> AcceptAnswer([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] AcceptAnswer acceptAnswer)
        {
            var command = Map(acceptAnswer);
            await this.acceptAnswerUseCase.Execute(command);
            return this.Accepted();
        }

        public override async Task<IActionResult> RejectAnswer([FromRoute(Name = "task_id"), Required] long taskId, [FromBody] RejectAnswer rejectAnswer)
        {
            var command = Map(rejectAnswer);
            await this.rejectAnswerUseCase.Execute(command);
            return this.Accepted();
        }

        private IGetReviewAnswerTaskUseCase.Query Map(long taskId)
        {
            throw new NotImplementedException();
        }

        private ReviewAnswerTask Map(IGetReviewAnswerTaskUseCase.Response response)
        {
            throw new NotImplementedException();
        }

        private IAcceptAnswerUseCase.Command Map(AcceptAnswer acceptAnswer)
        {
            throw new NotImplementedException();
        }

        private IRejectAnswerUseCase.Command Map(RejectAnswer rejectAnswer)
        {
            throw new NotImplementedException();
        }
    }
}