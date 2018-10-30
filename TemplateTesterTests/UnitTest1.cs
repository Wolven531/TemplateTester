using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using TemplateTester;
using Xunit;

namespace TemplateTesterTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			//var builder = WebHostBuilderFactory.CreateFromAssemblyEntryPoint
			var server = new TestServer(
				new WebHostBuilder()
					.UseEnvironment(EnvironmentName.Staging)
					.UseStartup<Startup>()
			);
		}
	}
}
