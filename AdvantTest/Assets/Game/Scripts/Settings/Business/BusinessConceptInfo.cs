using AdvantTest.Settings.Upgrade;
using UnityEngine;

namespace AdvantTest.Settings.Business
{
	[CreateAssetMenu(fileName = "Business ", menuName = "Business Data/Business ", order = 0)]
	public class BusinessConceptInfo : ScriptableObject
	{
		[field: Header("Задержка дохода")] [field: SerializeField]
		public uint IncomeDelay { get; private set; }

		[field: Header("Базовая стоимость")] [field: SerializeField]
		public ulong BasicCost { get; private set; }

		[field: Header("Базовый доход")] [field: SerializeField]
		public ulong BasicIncome { get; private set; }

		[field: Space(20)]
		
		[field: Header("Улучшения")] [field: SerializeField]
		public UpgradeConceptInfo[] Upgrades { get; private set; }
	}
}
