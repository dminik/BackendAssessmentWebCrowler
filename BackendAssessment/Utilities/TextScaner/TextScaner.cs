using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.Services
{
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;

	public class TextScaner
	{
		public static IEnumerable<string> SearchPhrase(string phrase, string text, int maxCharDistanceBetweenPhraseWords = 25, int maxCharSurraund = 25)
		{

			var resultList = new List<string>();
			var regExpStr = new StringBuilder();
			

			var phraseTokens = phrase.Split(' ');
			var isFirstIteration = true;

			regExpStr.Append(@".{0," + maxCharSurraund + "}"); 
			foreach (var currentToken in phraseTokens)
			{
				if (isFirstIteration)
				{
					isFirstIteration = false;					
				}
				else
				{
					regExpStr.Append(@".{1," + maxCharDistanceBetweenPhraseWords + "}"); 
				}

				regExpStr.AppendFormat(@"\b{0}\b", currentToken);
			}

			regExpStr.Append(@".{0," + maxCharSurraund + "}"); 
			
			Regex reg = new Regex(regExpStr.ToString(), RegexOptions.IgnoreCase);
			MatchCollection matchList = reg.Matches(text);

			foreach (Match currentMatch in matchList)
			{
				resultList.Add(currentMatch.ToString());				
			}
			
			return resultList;
		}
	}
}
