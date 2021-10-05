using System;
using System.Collections.Generic;
using Synion.DDD.Abstractions;
using Reference.Domain.Abstractions.Events;
using Reference.Domain.Abstractions.Exceptions;

namespace Reference.Domain.Core
{
    public class AnswerQuestionsAggregateRoot : AggregateRoot
    {
        public static AnswerQuestionsAggregateRoot Start(Guid questionId, string subject, string question, string askedBy)
        {
            return new AnswerQuestionsAggregateRoot
            (
                id: questionId,
                subject: subject,
                question: question,
                askedBy: askedBy
            );
        }

        private AnswerQuestionsAggregateRoot(Guid id, string subject, string question, string askedBy)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException($"'{nameof(subject)}' cannot be null or whitespace.", nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException($"'{nameof(question)}' cannot be null or whitespace.", nameof(question));
            }

            if (string.IsNullOrWhiteSpace(askedBy))
            {
                throw new ArgumentException($"'{nameof(askedBy)}' cannot be null or whitespace.", nameof(askedBy));
            }

            Subject = subject;
            Question = question;
            AskedBy = askedBy;
            Asked = DateTime.Now;
            
            RaiseEvent(new QuestionRecievedEvent(Id, Subject, Question, AskedBy, Asked));
        }

        public AnswerQuestionsAggregateRoot(Guid id, IEnumerable<KeyValuePair<string, string>> state)
            : base(id, state)
        { }

        public void AnswerQuestion(long taskId, string answer, string answeredBy)
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

            RaiseEvent(new QuestionAnsweredEvent(Id, taskId, Answer, AnsweredBy, Answered.Value));
        }

        public void AcceptAnswer(long taskId, string acceptedBy)
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

            RaiseEvent(new AnswerAcceptedEvent(Id, taskId, AcceptedBy, Accepted.Value));
        }

        public void RejectAnswer(long taskId, string rejection, string rejectedBy)
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
            
            RaiseEvent(new AnswerRejectedEvent(Id, taskId, Rejection, RejectedBy, Rejected.Value));
        }        

        public void ModifyAnswer(long taskId, string answer, string modifiedBy)
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

            RaiseEvent(new AnswerModifiedEvent(Id, taskId, Answer, ModifiedBy, Modified.Value));
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

        public void SendQuestionAnsweredEvent()
        {
            throw new NotImplementedException();
        }

        public void EndQuestion(string endedBy)
        {
            throw new NotImplementedException();
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
    }
}