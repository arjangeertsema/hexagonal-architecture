using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Reference.Adapters.Generated.Rest.Controllers;
using Reference.Adapters.Generated.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using Reference.Domain.Abstractions;
using Reference.Domain.Abstractions.Ports.Input;

namespace Reference.Adapters.Rest
{
    public class QuestionsApi : QuestionsApiController
    {
        private readonly IMediator mediator;

        public QuestionsApi(IMediator mediator)
        {
            if (mediator is null)
            {
                throw new ArgumentNullException(nameof(mediator));
            }

            this.mediator = mediator;
        }

        public override async Task<IActionResult> RegisterQuestion([FromBody] RegisterQuestion registerQuestion)
        {
            var command = new RegisterQuestionUseCase
            (
                commandId: registerQuestion.CommandId, 
                subject: registerQuestion.Subject, 
                question: registerQuestion.Question, 
                askedBy: registerQuestion.Sender
            );            
            await this.mediator.Send(command);
            return Accepted();
        }

        public override async Task<IActionResult> GetQuestions([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit)
        {
            var query = new GetQuestionsUseCase
            (
                offset: offset,
                limit: limit
            );
            
            var response = await mediator.Send(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> EndQuestion([FromRoute(Name = "question_id"), Required] Guid questionId, [FromBody] EndQuestion endQuestion)
        {
            var command = new EndQuestionUseCase
            (
                commandId: endQuestion.CommandId,
                questionId: questionId
            );

            await this.mediator.Send(command);
            return Accepted();
        }

        private static Questions Map(GetQuestionsUseCase.Response response)
        {
            return new Questions()
            {
                Items = response.Items.Select(Map).ToList()
            };
        }

        private static QuestionsModel Map(GetQuestionsUseCase.Response.Item item) 
        {
            return new QuestionsModel()
            {
                QuestionId = item.QuestionId,
                RecievedOn = item.AskedOn,
                LastActivityOn = item.LastActivityOn,
                Subject = item.Subject,
                Sender = item.AskedBy,
                Status = (QuestionsModel.StatusEnum) Enum.Parse(typeof(QuestionsModel.StatusEnum), item.Status.ToString())
            };
        }
    }
}