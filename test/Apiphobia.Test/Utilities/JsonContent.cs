using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Apiphobia.Test.Utilities
{
    public class JsonContent : StringContent
    {
        public JsonContent(object json): base(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json")
        {
        }
    }
}