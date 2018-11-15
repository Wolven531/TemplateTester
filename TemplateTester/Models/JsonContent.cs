using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TemplateTester.Models
{
	public class JsonContent : StringContent
	{
		private const string _ContentTypeCharSet = "utf-8";
		public static MediaTypeHeaderValue JSONContentType = new MediaTypeHeaderValue("application/json") { CharSet = _ContentTypeCharSet };

		//public JsonContent() : base(string.Empty)
		//{
		//	Headers.ContentType = JSONContentType;
		//}

		//public JsonContent(string value) : base(value)
		//{
		//	Headers.ContentType = JSONContentType;
		//}

		public JsonContent(JToken value) : base(value.ToString(Formatting.None))
		{
			Headers.ContentType = JSONContentType;
		}
	}
}
