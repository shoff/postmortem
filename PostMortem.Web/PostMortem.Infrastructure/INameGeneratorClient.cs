namespace PostMortem.Infrastructure
{
    using System.Threading.Tasks;

    public interface INameGeneratorClient
    {
        Task<string> GetNameAsync();
    }
}