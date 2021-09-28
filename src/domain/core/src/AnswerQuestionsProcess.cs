using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    public class AnswerQuestionsProcess : AggregateRoot<QuestionId>
    {
        public static AnswerQuestionsProcess Start(string subject, string question, string sender)
        {
            return new AnswerQuestionsProcess
            (
                questionId: QuestionId.NewQuestionId(),
                subject: subject,
                question: question,
                sender: sender
            );
        }

        private AnswerQuestionsProcess(QuestionId questionId, string subject, string question, string sender)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException($"'{nameof(question)}' cannot be null or whitespace.", nameof(question));
            }

            if (string.IsNullOrWhiteSpace(sender))
            {
                throw new ArgumentException($"'{nameof(sender)}' cannot be null or whitespace.", nameof(sender));
            }
            
            RaiseEvent(new AnswerQuestionsProcessStartedEvent(QuestionId.NewQuestionId(), subject, question, sender));
        }

        private string Subject { get; set; }
        private string Question { get; set; }
        private string AskedBy { get; set; }
        private DateTime Asked { get; set; }
        private string Answer { get; set; }
        private string AnsweredBy { get; set; }
        private DateTime? Answered { get; set; }
        private string AcceptedBy { get; set; }
        private DateTime? Accepted { get; set; }
        private string Rejection { get; set; }
        private string RejectedBy { get; set; }
        private DateTime? Rejected { get; set; }
        private string ModifiedBy { get; set; }
        private DateTime? Modified { get; set; }
        private DateTime? Sent { get; set; }

        public void AnswerQuestion(string answer, string answeredBy)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            if (string.IsNullOrWhiteSpace(answeredBy))
            {
                throw new ArgumentException($"'{nameof(answeredBy)}' cannot be null or whitespace.", nameof(answeredBy));
            }

            if(Answered.HasValue)
            {
                throw new AnswerQuestionProcessException("Question is already answered."); 
            }

            RaiseEvent(new QuestionAnsweredEvent(Id, answer, answeredBy));
        }

        public void AcceptAnswer(string acceptedBy)
        {
            if (string.IsNullOrWhiteSpace(acceptedBy))
            {
                throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
            }

            if(!Answered.HasValue)
            {
                throw new AnswerQuestionProcessException("Question is not answered."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionProcessException("Answer has been sent."); 
            }

            if(AnsweredBy == acceptedBy)
            {
                throw new AnswerQuestionProcessException("Answer may not be reviewed by the same person."); 
            }

            RaiseEvent(new AnswerAcceptedEvent(Id, acceptedBy));
        }

        public void RejectAnswer(string rejection, string rejectedBy)
        {
            if (string.IsNullOrWhiteSpace(rejection))
            {
                throw new ArgumentException($"'{nameof(rejection)}' cannot be null or whitespace.", nameof(rejection));
            }

            if (string.IsNullOrWhiteSpace(rejectedBy))
            {
                throw new ArgumentException($"'{nameof(rejectedBy)}' cannot be null or whitespace.", nameof(rejectedBy));
            }

            if (!Answered.HasValue)
            {
                throw new AnswerQuestionProcessException("Question is not answered."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionProcessException("Answer has been sent."); 
            }

            if(AnsweredBy == rejectedBy)
            {
                throw new AnswerQuestionProcessException("Answer may not be reviewed by the same person."); 
            }
            
            RaiseEvent(new AnswerRejectedEvent(Id, rejection, rejectedBy));
        }
        

        public void ModifyAnswer(string answer, string modifiedBy)
        {
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new ArgumentException($"'{nameof(answer)}' cannot be null or whitespace.", nameof(answer));
            }

            if (string.IsNullOrWhiteSpace(modifiedBy))
            {
                throw new ArgumentException($"'{nameof(modifiedBy)}' cannot be null or whitespace.", nameof(modifiedBy));
            }

            if (!Answered.HasValue)
            {
                throw new AnswerQuestionProcessException("Question is not answered."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionProcessException("Answer has already been sent."); 
            }

            if(!Rejected.HasValue)
            {
                throw new AnswerQuestionProcessException("Answer has not been rejected."); 
            }

            if(AnsweredBy != modifiedBy)
            {
                throw new AnswerQuestionProcessException("Answer must be modified by the same person."); 
            }

            RaiseEvent(new AnswerModifiedEvent(Id, answer, modifiedBy));
        }
        public void SendAnswer()
        {
            if (!Accepted.HasValue)
            {
                throw new AnswerQuestionProcessException("Answer is not accepted."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionProcessException("Answer has already been sent."); 
            }

            RaiseEvent(new AnswerSentEvent(Id));
        }

        internal void Apply(AnswerQuestionsProcessStartedEvent @event)
        {
            Id = @event.AggregateId;
            Subject = @event.Subject;
            Question = @event.Question;            
            AskedBy = @event.AskedBy;            
            Asked = @event.Asked;
        }

        internal void Apply(QuestionAnsweredEvent @event)
        {
            Answer = @event.Answer;
            AnsweredBy = @event.AnsweredBy;
            Answered = @event.Answered;
        }

        internal void Apply(AnswerAcceptedEvent @event)
        {
            AcceptedBy = @event.AcceptedBy;
            Accepted = @event.Accepted;
        }

        internal void Apply(AnswerRejectedEvent @event)
        {
            Rejection = @event.Rejection;
            RejectedBy = @event.RejectedBy;
            Rejected = @event.Rejected;
        }

        internal void Apply(AnswerModifiedEvent @event)
        {
            Answer = @event.Answer;
            ModifiedBy = @event.ModifiedBy;
            Modified = @event.Modified;
        }

        internal void Apply(AnswerSentEvent @event)
        {
            Sent = @event.Sent;
        }
    }
}