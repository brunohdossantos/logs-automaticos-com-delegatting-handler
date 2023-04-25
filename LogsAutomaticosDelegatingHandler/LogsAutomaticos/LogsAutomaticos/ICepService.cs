namespace LogsAutomaticos
{
    public interface ICepService
    {
        Task<HttpResponseMessage> GetCepAsync(string cep);
    }
}
