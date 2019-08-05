namespace PostMortem.Domain.Comments
{
    using Voters;

    public sealed class Like : Disposition
    {
        public Like(IVoterId voterId) 
            : base(voterId)
        {
            this.Liked = true;
        }
    }
}