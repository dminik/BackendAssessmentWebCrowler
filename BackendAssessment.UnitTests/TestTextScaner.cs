using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssessment.UnitTests
{
	using BackendAssessment.Services;
	using BackendAssessment.Utilities.TextScaner;

	using NUnit.Framework;

	[TestFixture]
	public class TestTextScaner
	{
		[Test]
		public void SearchPhrase_ThreeExistedWords_Success()
		{
			// Arrange 
			var phrase = "one two five";
			var text = "one minus3 minus2 minus1 zero one  two three four five six seven";

			// Act
			var resultMatchedStrings = new TextScaner().SearchPhrase(phrase, text).ToList();


			// Assert
			Assert.NotNull(resultMatchedStrings);
			Assert.AreEqual(1, resultMatchedStrings.Count());
			Assert.AreEqual("inus3 minus2 minus1 zero one  two three four five six seven", resultMatchedStrings[0]);
		}

		[Test]
		public void SearchPhrase_ThreeNotExistedWords_EmptyResult()
		{
			// Arrange 
			var phrase = "one twooo five";
			var text = "one minus3 minus2 minus1 zero one  two three four five six seven";

			// Act
			var resultMatchedStrings = new TextScaner().SearchPhrase(phrase, text).ToList();
			
			// Assert
			Assert.NotNull(resultMatchedStrings);
			Assert.AreEqual(0, resultMatchedStrings.Count());			
		}
	}
}
