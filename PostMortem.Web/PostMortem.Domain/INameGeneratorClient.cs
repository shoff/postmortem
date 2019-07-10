namespace PostMortem.Domain
{
    using System.Threading.Tasks;

    public interface INameGeneratorClient
    {
        Task<string> GetNameAsync();
    }
}