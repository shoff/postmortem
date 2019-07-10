namespace PostMortem.Web.Services
{
    using System.Threading.Tasks;

    public interface INameGeneratorClient
    {
        Task<string> GetNameAsync();
    }
}