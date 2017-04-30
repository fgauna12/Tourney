using System.Net.Http;
using Newtonsoft.Json;

namespace Tourney.Services.Participants.Client.Infrastructure
{
    public class JsonContent : StringContent
    {
        public JsonContent(object content) : this(JsonConvert.SerializeObject(content))
        {}

        public JsonContent(string content) : base(content)
        {
            Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }
    }
}