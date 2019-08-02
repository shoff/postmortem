namespace PostMortem.Domain.Voters
{
    public interface IVoter
    {
        VoterId VoterId { get; set; }
    }
}