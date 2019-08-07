namespace PostMortem.Domain.Voters
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class VoterId : ValueObject, IVoterId
    {
        public VoterId(string id)
        {
            this.Id = id;
        }

        [JsonProperty]
        public string Id { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Id;
        }
    }
}