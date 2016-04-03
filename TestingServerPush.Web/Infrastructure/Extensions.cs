using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TestingServerPush.Web.Infrastructure
{
    public static class Extensions
    {
        public static string AsJson(this object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
