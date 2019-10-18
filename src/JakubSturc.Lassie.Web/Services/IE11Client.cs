using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JakubSturc.Lassie.Web.Services
{
    /// <summary>
    /// HTTP Client that pretends it's IE11.
    /// </summary>
    /// <remarks>
    /// Implementation is too simplistic and probably doesn't fool anyone.
    /// </remarks>
    public class IE11Client
    {
        private readonly HttpClient _http;

        public IE11Client(HttpClient http)
        {
            _http = http;

            var headers = _http.DefaultRequestHeaders;
            headers.Add("Accept", "text/html,application/xhtml+xml,application/xml");
            headers.Add("Accept-Encoding", "gzip, deflate");
            headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
            headers.Add("Accept-Language", "en-US,en;q=0.7,cz;q=0.3");
        }

        public async Task<string> Get(string url)
        {
            return await _http.GetStringAsync(url);
        }

        public async Task<string> Post(string url, string body)
        {
            var content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
