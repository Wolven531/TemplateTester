using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateTester.Models;

namespace TemplateTester.Controllers
{
	[Route("api")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		// GET: api/Home
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/Home/5
		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

		// POST: api
		[HttpPost]
		public ActionResult Post()
		{
			return NoContent();
		}

		//// POST: api
		//[HttpPost]
		//public void Post([FromBody] string valueString)
		//{
		//}

		//// POST: api
		//[HttpPost]
		//public void Post([FromBody] JsonContent valueJson)
		//{
		//}

		//// PUT: api/Home/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE: api/ApiWithActions/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
