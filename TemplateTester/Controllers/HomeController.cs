﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TemplateTester.Models;
using TemplateTester.Repositories;

namespace TemplateTester.Controllers
{
	[Route("api")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly IEntityRepository _EntityRepository;

		private Dictionary<string, JObject> _Endpoints;

		private Dictionary<string, JObject> GenerateEndpointMap(Uri baseAddress)
		{
			var result = new Dictionary<string, JObject>
			{
				{
					"root",
					new JObject
					{
						["address"] = new Uri($"{baseAddress}"),
						["method"] = HttpMethods.Get
					}
				},
				{
					"endpointDetails",
					new JObject
					{
						["address"] = new Uri($"{baseAddress}{{endpoint}}"),
						["method"] = HttpMethods.Get
					}
				},
				{
					"entities",
					new JObject
					{
						["address"] = new Uri($"{baseAddress}entities"),
						["method"] = HttpMethods.Get
					}
				}
			};

			return result;
		}

		public HomeController(IEntityRepository entityRepository)
		{
			_EntityRepository = entityRepository;
		}

		[HttpGet("entities")]
		public IActionResult GetAllEntities()
		{
			return Ok(_EntityRepository.GetAllEntities());
		}

		[HttpGet("health")]
		public HttpContent GetAPIHealth([FromQuery] string format = "text")
		{
			const string responseText = "Boom baby!";

			if (string.IsNullOrEmpty(format) || format.Equals("text", StringComparison.InvariantCultureIgnoreCase))
			{
				return new TextContent(responseText);
				//return Ok(responseText);
			}
			if (format.Equals("html", StringComparison.InvariantCultureIgnoreCase))
			{
				//var respContent = new HtmlContent($"<html><head><title>Template Tester API Health Check</title></head><body>{responseText}</body></html>");
				//var resp = new HttpWebResponse() {
				//	ContentType = respContent.Headers.ContentType.MediaType
				//};

				////resp.StatusCode = HttpStatusCode.OK;
				//resp.GetResponseStream().WriteAsync(Encoding.UTF8.GetBytes(respContent.ReadAsStringAsync().Result));

				//return (IActionResult) resp;
				return new HtmlContent($"<html><head><title>Template Tester API Health Check</title></head><body>{responseText}</body></html>");
			}

			//Response.StatusCode = (int)HttpStatusCode.BadRequest;
			//Response.ContentType = JsonContent.JSONContentType.MediaType;
			//Response.Body.WriteAsync(Encoding.UTF8.GetBytes(new JObject
			//{
			//	["error"] = "GET request to this endpoint should have valid `format` query param [`text` | `html` | ``]"
			//}.ToString(Formatting.None)));

			//return (IActionResult) Response;
			return new JsonContent(new JObject
			{
				["error"] = "GET request to this endpoint should have valid `format` query param [`text` | `html` | ``]"
			});
		}

		/// <summary>
		/// This endpoint should be used to return information about this API, since
		/// it is a simple GET request at the API root
		/// </summary>
		/// <returns>A <code>Dictionary<string, JObject></code> filled with endpoint names mapped
		/// to the URI at which they can be reached</returns>
		[HttpGet]
		public IActionResult Get()
		{
			if (_Endpoints == null)
			{
				_Endpoints = GenerateEndpointMap(new Uri($"{Request.Scheme}://{Request.Host.Host}:{Request.Host.Port}"));
			}

			return Ok(JToken.FromObject(_Endpoints));
		}

		/// <summary>
		/// This endpoint provides more detailed information about a requested endpoint
		/// </summary>
		/// <param name="endpoint" type="string"></param>
		/// <returns></returns>
		[HttpGet("{endpoint}")]
		public IActionResult Get([FromRoute] string endpoint)
		{
			if (_Endpoints == null)
			{
				_Endpoints = GenerateEndpointMap(new Uri($"{Request.Scheme}://{Request.Host.Host}:{Request.Host.Port}"));
			}

			if (_Endpoints.TryGetValue(endpoint, out var jEndpointDetails))
			{
				return Ok(jEndpointDetails);
			}

			return BadRequest(new JObject
			{
				["error"] = "GET request to this endpoint should have valid endpoint slug"
			});
		}

		[HttpPost]
		public IActionResult PostWithNoParams()
		{
			var requestBody = new StreamReader(Request.Body).ReadToEnd();

			if (Request.ContentLength > 0 || requestBody.Length > 0)
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
			_EntityRepository.AddEntity(newEntity);

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
