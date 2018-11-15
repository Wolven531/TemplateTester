using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateTester.Models;

namespace TemplateTester.Repositories
{
	public interface IEntityRepository
	{
		IEnumerable<SimpleEntity> GetAllEntities();

		void AddEntity(SimpleEntity newEntity);
	}
}
