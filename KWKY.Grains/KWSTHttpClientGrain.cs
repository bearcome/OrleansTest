using KWKY.IGrains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using System.Net.Http;

namespace KWKY.Grains
{

    /// <summary>
    /// 请求数通接口的HttpClient
    /// </summary>
    [StatelessWorker]
    public class KWSTHttpClientGrain : Grain, IKWSTHttpClientGrain
    {
        private ILogger _logger;
        private HttpClient _httpClient;
        public KWSTHttpClientGrain (ILogger<DefaultGrain> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
        }
    }
}
