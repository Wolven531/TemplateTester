using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
		public IActionResult PostWithNoParams()
		{
			if (Request.Body.Length > 0 || Request.ContentLength > 0)
			{
				return BadRequest(new JObject
				{
					["error"] = "POST request to this endpoint should not have content"
				});
			}
			return NoContent();
		}

		//// POST: api
		//[HttpPost]
		//public IActionResult PostWithStringParam([FromBody] string param1)
		//{
		//	return NoContent();
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
