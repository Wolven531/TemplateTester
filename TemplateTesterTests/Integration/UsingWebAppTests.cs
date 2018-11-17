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
	/// Test class that tests/verifies app's ability to respond with web app
	///
	/// Technique informed by: https://xunit.github.io/docs/comparisons.html
	/// </summary>
	public class UsingWebAppTests : IDisposable
	{
		private const string _ServerURL_HTTPS = "https://localhost:5310";

		private readonly TestServer _Server;

		/// <summary>
		/// This constructor is used to set up any shared context needed by EACH Fact in this class
		/// </summary>
		public UsingWebAppTests()
		{
			_Server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Production)
					.UseStartup<Startup>()
				)
			{
				BaseAddress = new Uri(_ServerURL_HTTPS)
			};
		}

		//[Fact]
		//public async Task GetServerRoot_WhenInvoked_ShouldReturnNonEmptyHtmlPageInOkResponse()
		//{
		//	// Arrange
		//	var client = _Server.CreateClient();

		//	// Act
		//	var getResponse = await client.GetAsync("/");
		//	getResponse.EnsureSuccessStatusCode();
		//	var getResponseString = await getResponse.Content.ReadAsStringAsync();

		//	// Assert
		//	getResponse.Content.Headers.ContentType.Should().Be(HtmlContent.HtmlContentType);
		//	getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
		//	getResponseString.Should().NotBeEmpty();
		//}

		public void Dispose()
		{
			_Server.Dispose();
		}
	}
}
