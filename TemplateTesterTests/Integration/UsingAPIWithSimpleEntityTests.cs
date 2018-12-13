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
	/// Test class that tests/verifies app's API interactions with SimpleEntity
	///
	/// Technique informed by: https://xunit.github.io/docs/comparisons.html
	/// </summary>
	public class UsingAPIWithSimpleEntityTests : IDisposable
	{
		private const string _ServerURL_HTTPS = "https://localhost:5310";

		private readonly TestServer _Server;

		/// <summary>
		/// This constructor is used to set up any shared context needed by EACH Fact in this class
		/// </summary>
		public UsingAPIWithSimpleEntityTests()
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
		public async Task PostWithModelParam_WhenInvokedWithSimpleEntityInBody_ShouldPersistEntityAndReturnOk()
		{
			// NOTE: PHASE 1 - verify OK response from POST
			// Arrange
			var newEntity = new SimpleEntity("simple name 1");
			var jEntity = JToken.FromObject(newEntity);
			var uploadData = new JsonContent(jEntity);
			var client = _Server.CreateClient();

			// Act
			var response = await client.PostAsync("/api/entities", uploadData);
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			// NOTE: PHASE 2 - verify entity was persisted using different client
			// Arrange
			var expectedResponseString = JArray.FromObject(new[] { newEntity }).ToString(Formatting.None);
			var client2 = _Server.CreateClient();

			// Act
			var getResponse = await client2.GetAsync("/api/entities");
			getResponse.EnsureSuccessStatusCode();
			var getResponseString = await getResponse.Content.ReadAsStringAsync();

			// Assert
			getResponse.Content.Headers.ContentType.Should().Be(JsonContent.JSONContentType);
			getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
			getResponseString.Should().BeEquivalentTo(expectedResponseString);
		}

		[Fact]
		public async Task UpdateUsingReadableName_WhenInvokedWithSimpleEntityInBody_ShouldPersistEntityAndReturnNoContent()
		{
			// NOTE: PHASE 1 - verify OK response from POST
			// Arrange
			var newEntity = new SimpleEntity("name1");
			var jEntity = JToken.FromObject(newEntity);
			var uploadData = new JsonContent(jEntity);
			var client = _Server.CreateClient();

			// Act
			var response = await client.PostAsync("/api/entities", uploadData);
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);

			// NOTE: PHASE 2 - verify NoContent response from PUT
			// Arrange
			var client2 = _Server.CreateClient();

			// Act
			var updatedEntity = new SimpleEntity("updated 1");
			var putResponse = await client2.PutAsync("/api/entities/name1", new JsonContent(JToken.FromObject(updatedEntity)));
			putResponse.EnsureSuccessStatusCode();

			// Assert
			putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

			// NOTE: PHASE 3 - verify entity was updated using different client
			// Arrange
			var expectedResponseString = JArray.FromObject(new[] { updatedEntity }).ToString(Formatting.None);
			var client3 = _Server.CreateClient();

			// Act
			var getResponse = await client2.GetAsync("/api/entities");
			getResponse.EnsureSuccessStatusCode();
			var getResponseString = await getResponse.Content.ReadAsStringAsync();

			// Assert
			getResponse.Content.Headers.ContentType.Should().Be(JsonContent.JSONContentType);
			getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
			getResponseString.Should().BeEquivalentTo(expectedResponseString);
		}

		public void Dispose()
		{
			_Server.Dispose();
		}
	}
}
