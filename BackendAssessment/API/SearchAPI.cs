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

	using HttpStatusCode = Nancy.HttpStatusCode;

	public class SearchAPI : NancyModule
	{
		public SearchAPI()
		{
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

				if (string.IsNullOrEmpty(query))
				{
					var response = (Response)"Invalid (or missing) query value";
					response.ContentType = "text/plain";
					return response.WithStatusCode(HttpStatusCode.BadRequest);
				}

				//TODO: implement logic (return a list of relevant results)

				return HttpStatusCode.OK;
			};



		}

		


	}




	
}
