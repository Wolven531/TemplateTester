using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateTester.Models
{
	public class SimpleEntity
	{
		public SimpleEntity(string readableName)
		{
			ReadableName = readableName;
		}

		public string ReadableName { get; }
	}
}
