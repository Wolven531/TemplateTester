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
		public async Task GetAPIHealthEndpoint_WhenInvoked_ShouldReturnOkWithTextContent()
		{
			// Arrange
			var expectedResponse = new TextContent("Boom, baby!");
			var client = _Server.CreateClient();

			// Act
			var response = await client.GetAsync("/api/health");
			response.EnsureSuccessStatusCode();

			// Assert
			response.Content.Headers.ContentType.Should().Be(TextContent.TextContentType);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Should().BeEquivalentTo(expectedResponse);
		}

		public void Dispose()
		{
			_Server.Dispose();
		}
	}
}
