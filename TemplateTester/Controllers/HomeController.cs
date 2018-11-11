using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

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

		[Route("entities")]
		[HttpPost]
		public IActionResult PostWithModelParam([FromBody] SimpleEntity newEntity)
		{
			return Ok();
		}

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
