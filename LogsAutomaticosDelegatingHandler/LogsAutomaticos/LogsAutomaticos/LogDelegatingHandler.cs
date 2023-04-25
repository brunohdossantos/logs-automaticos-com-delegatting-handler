using Microsoft.Extensions.Logging;

namespace LogsAutomaticos
{
    public class LogDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LogDelegatingHandler> _logger;

        public LogDelegatingHandler(ILogger<LogDelegatingHandler> logger)
        {
            _logger = logger;
        }

        public LogDelegatingHandler(HttpMessageHandler innerHandler, ILogger<LogDelegatingHandler> logger) : base(innerHandler)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestBody = null;
            string responseBody = null;

            if (request.Content != null)
            {
                requestBody = await request.Content.ReadAsStringAsync();
            }

            _logger.LogDebug("Http request body is {requestBody}.", requestBody);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content != null)
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }

            _logger.LogDebug("Http response body is {responseBody}.", responseBody);

            return response;
        }
    }
}
