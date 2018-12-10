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

namespace TemplateTesterTests.Unit.Repositories
{
	public class InMemoryEntityRepositoryTests : IDisposable
	{
		[Fact]
		public void GetAllEntities_WhenInvoked_ShouldReturnListOfEntities()
		{
			// Arrange
			var entities = new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			};
			var fixture = new InMemoryEntityRepository(entities);

			// Act
			var response = fixture.GetAllEntities();

			// Assert
			response.Should().BeEquivalentTo(new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			});
		}

		[Fact]
		public void AddEntity_WhenInvoked_ShouldAddEntity()
		{
			// Arrange
			var entities = new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			};
			var fixture = new InMemoryEntityRepository(entities);

			// Act
			fixture.AddEntity(new SimpleEntity("new 1"));
			var allEntities = fixture.GetAllEntities();

			// Assert
			allEntities.Should().BeEquivalentTo(new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3"),
				new SimpleEntity("new 1")
			});
		}

		[Fact]
		public void RemoveEntity_WhenInvokedWithMatchingName_ShouldRemoveEntity()
		{
			// Arrange
			var entities = new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			};
			var fixture = new InMemoryEntityRepository(entities);

			// Act
			fixture.RemoveEntity("ent 2");
			var allEntities = fixture.GetAllEntities();

			// Assert
			allEntities.Should().BeEquivalentTo(new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 3")
			});
		}

		[Fact]
		public void RemoveEntity_WhenInvokedWithoutMatchingName_ShouldHaveNoEffect()
		{
			// Arrange
			var entities = new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			};
			var fixture = new InMemoryEntityRepository(entities);

			// Act
			fixture.RemoveEntity("ent 5");
			var allEntities = fixture.GetAllEntities();

			// Assert
			allEntities.Should().BeEquivalentTo(new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			});
		}

		[Fact]
		public void UpdateEntity_WhenInvokedWithMatchingName_ShouldUpdateEntity()
		{
			// Arrange
			var entities = new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("ent 2"),
				new SimpleEntity("ent 3")
			};
			var fixture = new InMemoryEntityRepository(entities);

			// Act
			fixture.UpdateEntity("ent 2", new SimpleEntity("updated 1"));
			var allEntities = fixture.GetAllEntities();

			// Assert
			allEntities.Should().BeEquivalentTo(new[]
			{
				new SimpleEntity("ent 1"),
				new SimpleEntity("updated 1"),
				new SimpleEntity("ent 3")
			});
		}

		public void Dispose()
		{
		}
	}
}
