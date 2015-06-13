using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.API
{
	using System.Collections;
	using System.Diagnostics;
	using System.IO;
	using System.Net;
	using System.Text.RegularExpressions;

	using BackendAssessment.Services;

	using Newtonsoft.Json;

	using HttpStatusCode = Nancy.HttpStatusCode;

	public class SearchAPI : NancyModule
	{
		ISearchService SearchService { get; set; }

		public SearchAPI(ISearchService searchService)
		{
			SearchService = searchService;		
		
			Get["/ping"] = parameters =>
			{
				var result = new
				{
					Success = true,
					Timestamp = DateTime.UtcNow
				};

				return Response.AsJson(result);
			};

			Get["/search"] = parameters =>
			{				
				var query = Request.Query["query"].Value;
				Response response = null;

				if (string.IsNullOrEmpty(query))
				{
					response = (Response)"Invalid (or missing) query value";
					response.ContentType = "text/plain";
					return response.WithStatusCode(HttpStatusCode.BadRequest);
				}
				else
				{
					var results = searchService.Search(query);
					var json = JsonConvert.SerializeObject(results);
					response = (Response)json;
					return response.WithStatusCode(HttpStatusCode.OK);
				}				
			};



		}

		


	}




	
}
