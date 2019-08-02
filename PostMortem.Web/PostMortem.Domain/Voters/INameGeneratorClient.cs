namespace PostMortem.Domain.Voters
{
    using System.Threading.Tasks;

    public interface INameGeneratorClient
    {
        Task<string> GetNameAsync();
    }
}