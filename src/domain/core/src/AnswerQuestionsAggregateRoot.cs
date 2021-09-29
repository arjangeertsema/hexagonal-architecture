using System;
using System.Collections.Generic;
using Reference.Domain.Abstractions.DDD;
using Reference.Domain.Core;
using Reference.Domain.Core.AnswerQuestions;

namespace Reference.Domain.Core
{
    public class AnswerQuestionsAggregateRoot : AggregateRoot
    {
        public static AnswerQuestionsAggregateRoot Start(string subject, string question, string sender)
        {
            return new AnswerQuestionsAggregateRoot
            (
                subject: subject,
                question: question,
                sender: sender
            );
        }

        private AnswerQuestionsAggregateRoot(string subject, string question, string sender)
            : base(Guid.NewGuid())
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

            Subject = subject;
            Question = question;
            AskedBy = sender;
            Asked = DateTime.Now;
            
            RaiseEvent(new QuestionRegisteredEvent(Id, Subject, Question, AskedBy, Asked));
        }

        public AnswerQuestionsAggregateRoot(IEnumerable<KeyValuePair<string, string>> state)
            : base(state)
        { }

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
                throw new AnswerQuestionsException("Question is already answered."); 
            }

            Answer = answer;
            AnsweredBy = answeredBy;
            Answered = DateTime.Now;

            RaiseEvent(new QuestionAnsweredEvent(Id, Answer, AnsweredBy, Answered.Value));
        }

        public void AcceptAnswer(string acceptedBy)
        {
            if (string.IsNullOrWhiteSpace(acceptedBy))
            {
                throw new ArgumentException($"'{nameof(acceptedBy)}' cannot be null or whitespace.", nameof(acceptedBy));
            }

            if(!Answered.HasValue)
            {
                throw new AnswerQuestionsException("Question is not answered."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionsException("Answer has been sent."); 
            }

            if(AnsweredBy == acceptedBy)
            {
                throw new AnswerQuestionsException("Answer may not be reviewed by the same person."); 
            }

            AcceptedBy = acceptedBy;
            Accepted = DateTime.Now;

            RaiseEvent(new AnswerAcceptedEvent(Id, AcceptedBy, Accepted.Value));
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
                throw new AnswerQuestionsException("Question is not answered."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionsException("Answer has been sent."); 
            }

            if(AnsweredBy == rejectedBy)
            {
                throw new AnswerQuestionsException("Answer may not be reviewed by the same person."); 
            }

            Rejection = rejection;
            RejectedBy = rejectedBy;
            Rejected = DateTime.Now;
            
            RaiseEvent(new AnswerRejectedEvent(Id, Rejection, RejectedBy, Rejected.Value));
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
                throw new AnswerQuestionsException("Question is not answered."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionsException("Answer has already been sent."); 
            }

            if(!Rejected.HasValue)
            {
                throw new AnswerQuestionsException("Answer has not been rejected."); 
            }

            if(AnsweredBy != modifiedBy)
            {
                throw new AnswerQuestionsException("Answer must be modified by the same person."); 
            }

            Answer = answer;
            ModifiedBy = modifiedBy;
            Modified = DateTime.Now;

            RaiseEvent(new AnswerModifiedEvent(Id, Answer, ModifiedBy, Modified.Value));
        }
        public void SendAnswer()
        {
            if (!Accepted.HasValue)
            {
                throw new AnswerQuestionsException("Answer is not accepted."); 
            }

            if(Sent.HasValue)
            {
                throw new AnswerQuestionsException("Answer has already been sent."); 
            }

            Sent = DateTime.Now;

            RaiseEvent(new AnswerSentEvent(Id, Sent.Value));
        }
    }
}