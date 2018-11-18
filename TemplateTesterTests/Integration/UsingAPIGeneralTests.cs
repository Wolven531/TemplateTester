using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using TemplateTester;
using TemplateTester.Models;
using Xunit;

namespace TemplateTesterTests.Integration
{
	/// <summary>
	/// Test class that tests/verifies app's basic API interactions
	///
	/// Technique informed by: https://xunit.github.io/docs/comparisons.html
	/// </summary>
	public class UsingAPIGeneralTests : IDisposable
	{
		private const string _ServerURL_HTTPS = "https://localhost:5310";

		private readonly TestServer _Server;

		/// <summary>
		/// This constructor is used to set up any shared context needed by EACH Fact in this class
		/// </summary>
		public UsingAPIGeneralTests()
		{
			_Server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Staging)
					.UseStartup<Startup>()
				)
			{
				BaseAddress = new Uri(_ServerURL_HTTPS)
			};
		}

		[Fact]
		public async Task GetAPIHealthEndpoint_WhenInvokedWithValidTextFormatParameter_ShouldReturnOkWithTextContent()
		{
			// Arrange
			var expectedResponse = new TextContent("Boom, baby!");
			var client = _Server.CreateClient();

			// Act
			var response = await client.GetAsync("/api/health?format=text");
			response.EnsureSuccessStatusCode();

			// Assert
			response.Content.Headers.ContentType.Should().Be(TextContent.TextContentType);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Should().BeEquivalentTo(expectedResponse);
		}

		[Fact]
		public async Task GetAPIHealthEndpoint_WhenInvokedWithValidHtmlFormatParameter_ShouldReturnOkWithHtmlContent()
		{
			// Arrange
			var expectedResponse = new HtmlContent("<html><head><title>Template Tester API Health Check</title></head><body>Boom, baby!</body></html>");
			var client = _Server.CreateClient();

			// Act
			var response = await client.GetAsync("/api/health?format=html");
			response.EnsureSuccessStatusCode();

			// Assert
			response.Content.Headers.ContentType.Should().Be(HtmlContent.HtmlContentType);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Should().BeEquivalentTo(expectedResponse);
		}

		[Theory]
		[InlineData("/api/health")]
		[InlineData("/api/health?format=")]
		public async Task GetAPIHealthEndpoint_WhenInvokedWithoutValidFormatParameter_ShouldReturnBadRequestWithJsonError(string requestPath)
		{
			// Arrange
			var expectedResponse = new JsonContent(new JObject { ["error"] = "GET request to this endpoint should have valid `format` query param [`text` | `html` | ``]" });
			var client = _Server.CreateClient();

			// Act
			var response = await client.GetAsync(requestPath);

			// Assert
			response.Content.Headers.ContentType.Should().Be(JsonContent.JSONContentType);
			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			response.Content.Should().BeEquivalentTo(expectedResponse);
		}

		public void Dispose()
		{
			_Server.Dispose();
		}
	}
}
