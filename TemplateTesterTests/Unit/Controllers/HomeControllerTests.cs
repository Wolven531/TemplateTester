using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TemplateTester;
using TemplateTester.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TemplateTester.Models;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;

namespace TemplateTesterTests.Unit.Controllers
{
	public class HomeControllerTests : IDisposable
	{
		private const string _ServerURL_HTTPS = "https://localhost:5310";

		private readonly Mock<IEntityRepository> _MockEntityRepository;

		private readonly TestServer _Server;

		public HomeControllerTests()
		{
			_MockEntityRepository = new Mock<IEntityRepository>();
			_Server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Staging)
					.UseStartup<Startup>()
					//.Configure(app => {
					//})
					.ConfigureServices(serviceCollection =>
					{
						serviceCollection.TryAddSingleton(_MockEntityRepository.Object);
					}))
			{
				BaseAddress = new Uri(_ServerURL_HTTPS)
			};
		}

		[Fact]
		public async Task GetAllEntities_WhenInvoked_ShouldReturnJArrayOfEntitiesFromRepository()
		{
			// Arrange
			var entities = new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			};
			_MockEntityRepository
				.Setup(repo => repo.GetAllEntities())
				.Returns(entities);
			var expectedResponseString = JArray.FromObject(entities).ToString(Formatting.None);
			var client = _Server.CreateClient();

			// Act
			var getResponse = await client.GetAsync("/api/entities");
			getResponse.EnsureSuccessStatusCode();
			var getResponseString = await getResponse.Content.ReadAsStringAsync();

			// Assert
			_MockEntityRepository.VerifyAll();
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
