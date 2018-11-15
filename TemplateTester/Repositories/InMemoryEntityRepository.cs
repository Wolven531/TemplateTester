using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateTester.Models;

namespace TemplateTester.Repositories
{
	public class InMemoryEntityRepository : IEntityRepository
	{
		private readonly List<SimpleEntity> _Entities;

		public InMemoryEntityRepository()
		{
			_Entities = new List<SimpleEntity>();
		}

		public void AddEntity(SimpleEntity newEntity)
		{
			_Entities.Add(newEntity);
		}

		public IEnumerable<SimpleEntity> GetAllEntities()
		{
			return _Entities;
		}
	}
}
