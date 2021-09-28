using System;
using example.domain.abstractions.ddd;

namespace example.domain.core
{
    public class QuestionId : IAggregateId
    {
        private const string IdAsStringPrefix = "Process-";

        public Guid Id { get; private set; }

        private QuestionId(Guid id)
        {
            Id = id;
        }

        public QuestionId(string id)
        {
            Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        public override string ToString()
        {
            return IdAsString();
        }

        public override bool Equals(object obj)
        {
            return obj is QuestionId && Equals(Id, ((QuestionId)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static QuestionId NewQuestionId()
        {
            return new QuestionId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{Id.ToString()}";
        }
    }
}