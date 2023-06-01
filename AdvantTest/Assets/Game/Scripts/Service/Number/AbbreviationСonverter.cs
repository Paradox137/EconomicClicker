using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace AdvantTest.Service.Number
{
	public static class AbbreviationСonverter
	{
		private static readonly SortedDictionary<double, string> abbreviations = new SortedDictionary<double, string>
		{
			{ 1000d, "K" },
			{ 1000000d, "M" },
			{ 1000000000d, "B" }
		};

		public static string AbbreviateNumber(double number)
		{
			for (int i = abbreviations.Count - 1; i >= 0; i--)
			{
				KeyValuePair<double, string> pair = abbreviations.ElementAt(i);
                
				if (Math.Abs(number) >= pair.Key)
				{
					double roundedNumber = Math.Round(number / pair.Key, 2);
					
					return roundedNumber.ToString(CultureInfo.CurrentCulture) + pair.Value;
				}
			}

			return number.ToString(CultureInfo.CurrentCulture);
		}
	}
}
