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

		public InMemoryEntityRepository(IEnumerable<SimpleEntity> initialStore = null)
		{
			_Entities = initialStore != null
				? initialStore.ToList()
				: new List<SimpleEntity>();
		}

		public void AddEntity(SimpleEntity newEntity)
		{
			_Entities.Add(newEntity);
		}

		public IEnumerable<SimpleEntity> GetAllEntities()
		{
			return _Entities;
		}

		public void RemoveEntity(string readableName)
		{
			_Entities.RemoveAll(simpleEntity => simpleEntity.ReadableName.Equals(readableName, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
