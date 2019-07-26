using System.Threading.Tasks;

namespace PostMortem.Infrastructure
{
    public interface INameGeneratorClient
    {
        Task<string> GetNameAsync();
    }
}