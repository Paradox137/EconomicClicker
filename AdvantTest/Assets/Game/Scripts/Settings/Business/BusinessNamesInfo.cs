using System;
using UnityEngine;

namespace AdvantTest.Settings.Business
{
	[Serializable]
	public struct BusinessNamesInfo
	{
		[field: Header("Название Бизнеса")] [field: SerializeField]
		public string BusinessName { get; private set; }

		[field: Header("Название Улучшений")] [field: SerializeField]
		public string[] BusinessUpgradesNames { get; private set; }
	}
}
