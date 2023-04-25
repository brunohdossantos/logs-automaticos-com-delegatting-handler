namespace LogsAutomaticos
{
    public class CepService : ICepService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CepService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetCepAsync(string cep)
        {
            var client = _httpClientFactory.CreateClient("cepService");

            var url = $"https://brasilapi.com.br/api/cep/v1/{cep}";

            return await client.GetAsync(url);
        }
    }
}
