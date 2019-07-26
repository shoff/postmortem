namespace PostMortem.Infrastructure
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class NameGeneratorClient : INameGeneratorClient
    {
        // https://frightanic.com/goodies_content/docker-names.php
        private readonly HttpClient client;


        public NameGeneratorClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://frightanic.com/goodies_content/docker-names.php");
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "boring-wozniak");
            this.client = httpClient;
        }
        public async Task<string> GetNameAsync()
        {
            var name = await this.client.GetStringAsync("https://frightanic.com/goodies_content/docker-names.php");
            return name;
        }
    }
}