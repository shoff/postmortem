namespace PostMortem.Domain.Voters
{
    public class Voter : IVoter
    {
        public Voter(string voterName)
        {
            this.VoterId = new VoterId(voterName);
        }

        public VoterId VoterId { get; private set; }
    }
}