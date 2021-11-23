namespace Adapters.Zeebe.Jobs;

public class SendAnswerJobV1 : AbstractJob<SendAnswerJobV1.JobState>
{
    public SendAnswerJobV1(IJob job, SendAnswerJobV1.JobState state) : base(job, state) { }

    public class JobState
    {
        public AnswerQuestionId QuestionId { get; set; }
        public Guid SendAnswerCommandId { get; set; }
    }
}
