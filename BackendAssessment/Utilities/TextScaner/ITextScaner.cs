namespace BackendAssessment.Utilities.TextScaner
{
	using System.Collections.Generic;

	public interface ITextScaner
	{
		IEnumerable<string> SearchPhrase(string phrase, string text, 
			int maxCharDistanceBetweenPhraseWords = 25, int maxCharSurraund = 25);
	}
}