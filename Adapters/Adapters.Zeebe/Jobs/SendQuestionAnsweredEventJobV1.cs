namespace Adapters.Zeebe.Jobs;

public class SendQuestionAnsweredEventJobV1 : AbstractJob<SendQuestionAnsweredEventJobV1.JobState>
{
    public SendQuestionAnsweredEventJobV1(IJob job, SendQuestionAnsweredEventJobV1.JobState state) : base(job, state) { }

    public class JobState
    {
        public AnswerQuestionId QuestionId { get; set; }
        public Guid SendQuestionAnsweredEventCommandId { get; set; }
    }
}
