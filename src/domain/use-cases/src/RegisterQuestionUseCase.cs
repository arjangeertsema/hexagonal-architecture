using System;
using System.Threading.Tasks;
using example.domain.abstractions.attributes;
using example.domain.abstractions.ports.input;
using example.domain.abstractions;
using example.domain.core;
using example.domain.abstractions.ports.output;

namespace example.domain.use_cases
{
    public class RegisterQuestionUseCase : IRegisterQuestionUseCase
    {
        private readonly IPermission permission;
        private readonly ISaveAggregateRoot<QuestionId> saveAggregateRoot;

        public RegisterQuestionUseCase(
            IPermission permission,
            ISaveAggregateRoot<QuestionId> saveAggregateRoot
        )
        {
            if (permission is null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            if (saveAggregateRoot is null)
            {
                throw new ArgumentNullException(nameof(saveAggregateRoot));
            }

            this.permission = permission;
            this.saveAggregateRoot = saveAggregateRoot;
        }

        [PreAuthorize(nameof(HasPermission))]
        public async Task Execute(IRegisterQuestionUseCase.Command command)
        {
            //Idempotency check
            
            var process = AnswerQuestionsProcess.Start
            (
                subject: command.Subject,
                question: command.Question,
                sender: command.Sender
            );

            await this.saveAggregateRoot.Execute(Map(process));
        }

        public Task<bool> HasPermission()
        {
            return permission.HasPermission("A permission");
        }

        private static ISaveAggregateRoot<QuestionId>.Command Map(AnswerQuestionsProcess answerQuestionsProcess) 
        {
            return new ISaveAggregateRoot<QuestionId>.Command
            (
                commandId: Guid.NewGuid(), 
                aggregateRoot: answerQuestionsProcess
            );
        }
    }
}
