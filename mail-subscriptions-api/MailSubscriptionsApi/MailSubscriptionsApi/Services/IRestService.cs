using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Services
{
    public interface IRestService
    {
        Task SendFormEncodedAsync(string url, IEnumerable<KeyValuePair<string, string>> formData);
    }
}