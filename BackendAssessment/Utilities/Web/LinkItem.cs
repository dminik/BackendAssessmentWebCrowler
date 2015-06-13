namespace BackendAssessment.Utilities.Web
{
	using System;

	public struct LinkItem
	{
		public Uri Href;

		public string Text;

		public bool IsLocal;

		public override string ToString()
		{
			return string.Format("{0}\n\t{1}\n\t{2}", Href, Text, IsLocal);
		}
	}
}