using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Threading.Tasks;
using TemplateTester;
using Xunit;

namespace TemplateTesterTests
{
	public class BasicIntegrationTests
	{
		[Fact]
		public async Task GetHomePage_WhenInvoked_ShouldReturnHomepage()
		{
			// Arrange
			//var builder = WebHostBuilderFactory.CreateFromAssemblyEntryPoint
			var server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Staging)
					.UseStartup<Startup>()
			);
			server.BaseAddress = new Uri("https://localhost:5310");
			var client = server.CreateClient();

			// Act
			var response = await client.GetAsync("/");

			// Assert
			response.EnsureSuccessStatusCode();
		}
	}
}
