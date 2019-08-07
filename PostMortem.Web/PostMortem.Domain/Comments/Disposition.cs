namespace PostMortem.Domain.Comments
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Voters;

    public abstract class Disposition
    {
        [JsonProperty]
        public IVoterId VoterId { get; private set; }

        [JsonProperty]
        public bool Liked { get; protected set; }
        
        protected Disposition(
            IVoterId voterId)
        {
            this.VoterId = voterId;
        }

        public static implicit operator bool(Disposition disposition)
        {
            return disposition != null && disposition.Liked;
        }

        private sealed class VoterIdEqualityComparer : IEqualityComparer<Disposition>
        {
            public bool Equals(Disposition x, Disposition y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (ReferenceEquals(x, null))
                {
                    return false;
                }

                if (ReferenceEquals(y, null))
                {
                    return false;
                }

                if (x.GetType() != y.GetType())
                {
                    return false;
                }

                return Equals(x.VoterId, y.VoterId);
            }

            public int GetHashCode(Disposition obj)
            {
                return (obj.VoterId != null ? obj.VoterId.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<Disposition> VoterIdComparer { get; } = new VoterIdEqualityComparer();

    }
}