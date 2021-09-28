using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using example.domain.abstractions.ports.input;
using example.adapters.rest.generated.Controllers;
using example.adapters.rest.generated.Models;
using Microsoft.AspNetCore.Mvc;

namespace example.adapters.rest
{
    public class QuestionsApi : QuestionsApiController
    {
        private readonly IRegisterQuestionUseCase registerQuestionUseCase;
        private readonly IGetQuestionsUseCase getQuestionsUseCase;
        private readonly IEndQuestionUseCase endQuestionUseCase;

        public QuestionsApi(
            IRegisterQuestionUseCase registerQuestionUseCase,
            IGetQuestionsUseCase getQuestionsUseCase,
            IEndQuestionUseCase endQuestionUseCase)
        {
            if (registerQuestionUseCase is null)
            {
                throw new ArgumentNullException(nameof(registerQuestionUseCase));
            }

            if (getQuestionsUseCase is null)
            {
                throw new ArgumentNullException(nameof(getQuestionsUseCase));
            }

            if (endQuestionUseCase is null)
            {
                throw new ArgumentNullException(nameof(endQuestionUseCase));
            }

            this.registerQuestionUseCase = registerQuestionUseCase;
            this.getQuestionsUseCase = getQuestionsUseCase;
            this.endQuestionUseCase = endQuestionUseCase;
        }

        public override async Task<IActionResult> RegisterQuestion([FromBody] RegisterQuestion registerQuestion)
        {
            var command = Map(registerQuestion);
            await this.registerQuestionUseCase.Execute(command);
            return Accepted();
        }

        public override async Task<IActionResult> GetQuestions([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit)
        {
            var query = Map(offset, limit);
            var response = await this.getQuestionsUseCase.Execute(query);
            return Ok(Map(response));
        }

        public override async Task<IActionResult> EndQuestion([FromRoute(Name = "question_id"), Required] Guid questionId, [FromBody] EndQuestion endQuestion)
        {
            var command = Map(endQuestion);
            await this.endQuestionUseCase.Execute(command);
            return Accepted();
        }
        
        private static IRegisterQuestionUseCase.Command Map(RegisterQuestion registerQuestion)
        {
            var command = new IRegisterQuestionUseCase.Command(
                commandId: registerQuestion.CommandId,
                questionId: registerQuestion.QuestionId,
                subject: registerQuestion.Subject,
                question: registerQuestion.Question,
                sender: registerQuestion.Sender
            );

            return command;
        }

        private static IGetQuestionsUseCase.Query Map(int? offset, int? limit)
        {
            var query = new IGetQuestionsUseCase.Query
            (
                offset: offset,
                limit: limit
            );

            return query;
        }

        private static Questions Map(IGetQuestionsUseCase.Response response)
        {
            var questions = new Questions();
            questions.Items = response.Items
                .Select(i => 
                    new QuestionsAllOfItems()
                    {
                        QuestionId = i.QuestionId,
                        RecievedOn = i.RecievedOn,
                        LastActivityOn = i.LastActivityOn,
                        Subject = i.Subject,
                        Sender = i.Sender,
                        Status = (QuestionsAllOfItems.StatusEnum) Enum.Parse(typeof(QuestionsAllOfItems.StatusEnum), i.Status.ToString())
                    }
                )
                .ToList();


            return questions;
        }

        private static IEndQuestionUseCase.Command Map(EndQuestion endQuestio) 
        {
            var command = new IEndQuestionUseCase.Command();

            return command;
        }
    }
}