using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Services
{
    public class RestService: IRestService
    {
        public async Task SendFormEncodedAsync(string url, IEnumerable<KeyValuePair<string, string>> formData)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(formData);
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
