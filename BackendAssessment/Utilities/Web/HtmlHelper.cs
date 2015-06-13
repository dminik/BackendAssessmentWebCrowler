using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.Utilities.Web
{
	using System.Diagnostics;
	using System.Text.RegularExpressions;

	public class HtmlHelper
	{
		public static List<LinkItem> FindLinks(string file, Uri initialUrl)
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
							newLink.Href = new Uri(initialUrl, hrefValue);
						else
						{
							newLink.Href = new Uri(hrefValue);
						}

						newLink.IsLocal = (newLink.Href.Host == initialUrl.Host);
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


		public static string HtmlToText(string source)
		{
			char[] array = new char[source.Length];
			int arrayIndex = 0;
			bool inside = false;

			for (int i = 0; i < source.Length; i++)
			{
				char let = source[i];
				if (@let == '<')
				{
					inside = true;
					continue;
				}
				if (@let == '>')
				{
					inside = false;
					continue;
				}
				if (!inside)
				{
					array[arrayIndex] = @let;
					arrayIndex++;
				}
			}

			var result = new string(array, 0, arrayIndex).Replace('\n', ' ').Replace('\t', ' ');
			return result;
		}
	}
}
