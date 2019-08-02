namespace PostMortem.Domain.Voters
{
    public class Voter : IVoter
    {
        private readonly INameGeneratorClient nameGeneratorClient;

        public Voter(INameGeneratorClient nameGeneratorClient)
        {
            this.nameGeneratorClient = nameGeneratorClient;
            this.VoterId.Id = this.nameGeneratorClient.GetNameAsync().GetAwaiter().GetResult();
        }

        public VoterId VoterId { get; set; } = new VoterId();
    }
}