namespace PostMortem.Domain.Comments
{
    using Voters;

    public sealed class DisLike : Disposition
    {
        public DisLike(IVoterId voterId)
            : base(voterId)
        {
        }
    }
}