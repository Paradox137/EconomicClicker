using System;
using UnityEngine;

namespace AdvantTest.Settings.Upgrade
{
	[Serializable]
	public struct UpgradeConceptInfo
	{
		[field: Header("Цена улучшения")] [field: SerializeField]
		public ulong UpgradeCost { get; private set; }
		
		[field: Header("Множитель дохода")] [field: SerializeField]
		public ulong IncomeMultiplier { get; private set; }
	}
}
