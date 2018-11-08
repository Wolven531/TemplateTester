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
		private static readonly MediaTypeHeaderValue _JSONContentType = new MediaTypeHeaderValue("application/json") { CharSet = _ContentTypeCharSet };
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
