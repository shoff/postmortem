namespace PostMortem.Domain.Questions
{
    using ChaosMonkey.Guards;

    public class QuestionId
    {
        public QuestionId(string previousId)
        {
            Guard.IsNotNullOrWhitespace(previousId, nameof(previousId));
            this.PreviousId = previousId;
        }

        public string Id { get; set; }

        public string PreviousId { get;  }
    }
}