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
		private Dictionary<string, Uri> _Endpoints;

		private Dictionary<string, Uri> GenerateEndpointMap(Uri baseAddress)
		{
			var result = new Dictionary<string, Uri>
			{
				{ "root", new Uri($"{baseAddress}") }
			};

			return result;
		}

		/// <summary>
		/// This endpoint should be used to return information about this API, since
		/// it is a simple GET request at the API root
		/// </summary>
		/// <returns>A <code>Dictionary<string, Uri></code> filled with endpoint names mapped
		/// to the URI at which they can be reached</returns>
		[HttpGet]
		public Dictionary<string, Uri> Get()
		{
			if (_Endpoints == null)
			{
				_Endpoints = GenerateEndpointMap(new Uri($"{Request.Scheme}://{Request.Host.Host}:{Request.Host.Port}"));
			}

			return _Endpoints;
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
