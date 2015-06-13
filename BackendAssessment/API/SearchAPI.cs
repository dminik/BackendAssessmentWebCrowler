namespace BackendAssessment.API
{
	using System;

	using BackendAssessment.Services;

	using Nancy;

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
					try
					{
						var results = searchService.Search(query);
						var json = JsonConvert.SerializeObject(results);
						response = (Response)json;
						return response.WithStatusCode(HttpStatusCode.OK);
					}
					catch (Exception ex)
					{
						response = string.Format("{0}{1}", (Response)"Error: ", ex.Message);
						response.ContentType = "text/plain";
						return response.WithStatusCode(HttpStatusCode.BadRequest);
					}					
				}				
			};
		}
	}
}
