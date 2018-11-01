using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TemplateTester;
using Xunit;

namespace TemplateTesterTests
{
	public class BasicIntegrationTests
	{
		private const string _ContentTypeCharSet = "utf-8";
		private const string _ServerURL = "https://localhost:5310";

		private readonly MediaTypeHeaderValue _HTMLContentType;
		private readonly MediaTypeHeaderValue _JSONContentType;
		private readonly MediaTypeHeaderValue _TextContentType;

		private readonly TestServer _Server;

		public BasicIntegrationTests()
		{
			_HTMLContentType = MediaTypeHeaderValue.Parse("text/html");
			_HTMLContentType.CharSet = _ContentTypeCharSet;
			_JSONContentType = MediaTypeHeaderValue.Parse("application/json");
			_JSONContentType.CharSet = _ContentTypeCharSet;
			_TextContentType = MediaTypeHeaderValue.Parse("text/plain");
			_TextContentType.CharSet = _ContentTypeCharSet;

			_Server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Staging)
					.UseStartup<Startup>())
			{
				BaseAddress = new Uri(_ServerURL)
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
			response.Content.Headers.ContentType.Should().Be(_JSONContentType);
			response.StatusCode.Should().Be(200);
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
			response.StatusCode.Should().Be(200);
			responseString.Should().BeEquivalentTo(expectedResponse);
		}
	}
}
