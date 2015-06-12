using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.Services
{
	using System.Collections;
	using System.Diagnostics;
	using System.IO;
	using System.Net;
	using System.Text.RegularExpressions;

	using BackendAssessment.API;
	public struct LinkItem
	{
		public Uri Href;

		public string Text;

		public bool IsLocal;

		public override string ToString()
		{
			return Href + "\n\t" + Text;
		}
	}

	public class PageDownloader
	{
		public readonly Dictionary<string, string> Pages = new Dictionary<string, string>();

		private Uri InitialUrl = null;


		public void GetListings(Uri url)
		{
			if(Pages.ContainsKey(url.OriginalString))
				return;

			Trace.WriteLine(string.Format("Parsing page {0}", url.AbsoluteUri));

			if (InitialUrl == null)
				InitialUrl = url;

			string pageSrc = GetWebString(url);
			

			var pageLinks = FindLinks(pageSrc);
			var pageLocalLinks = pageLinks.Where(x => x.IsLocal);

			var plainText = HtmlToText(pageSrc);
			Pages[url.OriginalString] = plainText;

			foreach (var currentLink in pageLocalLinks)
			{				
				GetListings(currentLink.Href);				
			}

			
		}

		public static string HtmlToText(string source)
		{
			char[] array = new char[source.Length];
			int arrayIndex = 0;
			bool inside = false;

			for (int i = 0; i < source.Length; i++)
			{
				char let = source[i];
				if (let == '<')
				{
					inside = true;
					continue;
				}
				if (let == '>')
				{
					inside = false;
					continue;
				}
				if (!inside)
				{
					array[arrayIndex] = let;
					arrayIndex++;
				}
			}

			var result = new string(array, 0, arrayIndex).Replace('\n', ' ').Replace('\t', ' ');
			return result;
		}

		private string GetWebString(Uri url)
		{
			WebClient w = new WebClient();
			string s = w.DownloadString(url);
			return s;
		}

		public List<LinkItem> FindLinks(string file)
		{
			List<LinkItem> linkList = new List<LinkItem>();

			// 1.
			// Find all matches in file.
			MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

			// 2.
			// Loop over each match.
			foreach (Match m in m1)
			{
				string hrefValue = "";
				try
				{
					string value = m.Groups[1].Value;
					var newLink = new LinkItem();

					// 3.
					// Get href attribute.
					Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
					if (m2.Success)
					{
						hrefValue = m2.Groups[1].Value;
						if (string.IsNullOrEmpty(hrefValue) || hrefValue == "#" || hrefValue.StartsWith("html/"))
							continue;

						if (hrefValue.StartsWith("/") && !hrefValue.StartsWith("//")) // if local link
							newLink.Href = new Uri(InitialUrl, hrefValue);
						else
						{
							newLink.Href = new Uri(hrefValue);
						}

						newLink.IsLocal = (newLink.Href.Host == InitialUrl.Host);
						if (linkList.All(x => x.Href != newLink.Href))
							linkList.Add(newLink);
					}

					// 4.
					// Remove inner tags from text.
					string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
					newLink.Text = t;
				}
				catch (Exception ex)
				{
					Trace.WriteLine(string.Format("{0}. hrefValue = {1}", ex.Message, hrefValue));
					continue;
				}
			}// end for





			return linkList;
		}
	}
}
