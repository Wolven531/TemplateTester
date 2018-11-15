using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TemplateTester.Models
{
	public class TextContent : StringContent
	{
		private const string _ContentTypeCharSet = "utf-8";
		public static MediaTypeHeaderValue TextContentType = new MediaTypeHeaderValue("text/plain") { CharSet = _ContentTypeCharSet };

		public TextContent() : base(string.Empty)
		{
			Headers.ContentType = TextContentType;
		}

		public TextContent(string value) : base(value)
		{
			Headers.ContentType = TextContentType;
		}

		public TextContent(JToken value) : base(value.ToString(Formatting.None))
		{
			Headers.ContentType = TextContentType;
		}
	}
}
