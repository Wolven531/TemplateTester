using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TemplateTester;
using TemplateTester.Models;
using Xunit;

namespace TemplateTesterTests
{
	/// <summary>
	/// Test class that asserts app's ability to start up and communicate via HTTPS
	///
	/// Technique informed by: https://xunit.github.io/docs/comparisons.html
	/// </summary>
	public class BasicBackendTests
	{
		private const string _ContentTypeCharSet = "utf-8";
		private const string _ServerURL_HTTPS = "https://localhost:5310";

		private static readonly MediaTypeHeaderValue _HTMLContentType = new MediaTypeHeaderValue("text/html") { CharSet = _ContentTypeCharSet };
		private static readonly MediaTypeHeaderValue _TextContentType = new MediaTypeHeaderValue("text/plain") { CharSet = _ContentTypeCharSet };

		private readonly TestServer _Server;

		/// <summary>
		/// This constructor is used to set up any shared context needed by EACH Fact in this class
		/// </summary>
		public BasicBackendTests()
		{
			_Server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Staging)
					.UseStartup<Startup>())
			{
				BaseAddress = new Uri(_ServerURL_HTTPS)
			};
		}

		[Fact]
		public async Task GetAPIRoot_WhenInvoked_ShouldReturnJSONArrayOfStrings()
		{
			// Arrange
			var expectedResponse = new string[]
			{
				"value1", "value2"
			};
			var client = _Server.CreateClient();

			// Act
			var response = await client.GetAsync("/api");
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			response.Content.Headers.ContentType.Should().Be(JsonContent.JSONContentType);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			responseString.Should().BeEquivalentTo(JsonConvert.SerializeObject(expectedResponse));
		}

		[Fact]
		public async Task GetAPIRoot_WhenInvokedWithParameter_ShouldReturnString()
		{
			// Arrange
			var expectedResponse = "value";
			var client = _Server.CreateClient();

			// Act
			var response = await client.GetAsync("/api/999");
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			response.Content.Headers.ContentType.Should().Be(_TextContentType);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			responseString.Should().BeEquivalentTo(expectedResponse);
		}

		[Fact]
		public async Task PostAPIRoot_WhenInvokedWithoutDataInBody_ShouldReturnNoContent()
		{
			// Arrange
			//var expectedResponse = string.Empty;
			var client = _Server.CreateClient();
			//var uploadData = new JObject { ["value"] = "uploadValue" };

			// Act
			//var response = await client.PostAsync("/api", new StringContent(uploadData.ToString(Formatting.None), Encoding.UTF8, _JSONContentType.MediaType));
			//var response = await client.PostAsync("/api", new StringContent(uploadData.ToString(Formatting.None), Encoding.UTF8));
			//var response = await client.PostAsync("/api", new StringContent(uploadData.ToString(Formatting.None)));
			//var response = await client.PostAsync("/api", new StringContent(string.Empty));
			var response = await client.PostAsync("/api", null);
			//var postContent = new MultipartFormDataContent();
			//postContent.Add(new StringContent("asdf"), "value");
			//var response = await client.PostAsync("/api", postContent);
			//var response = await client.PostAsync("/api", new StringContent("value=qwer"));
			//var response = await client.PostAsync("/api", new JsonContent("value=qwer"));
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			response.Content.Headers.ContentType.Should().BeNull();
			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
			//responseString.Should().BeEquivalentTo(expectedResponse);
		}

		// TODO: fix test to test param POSTing properly
		[Fact]
		public async Task PostAPIRoot_WhenInvokedWithSurplusDataInBody_ShouldReturnBadRequest()
		{
			// Arrange
			var client = _Server.CreateClient();

			// Act
			var response = await client.PostAsync("/api", new StringContent("param1=qwer"));
			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			response.Content.Headers.ContentType.Should().Be(JsonContent.JSONContentType);
			var expectedResponse = new JObject
			{
				["error"] = "POST request to this endpoint should not have content"
			}.ToString(Formatting.None);
			responseString.Should().BeEquivalentTo(expectedResponse);
		}
	}
}
