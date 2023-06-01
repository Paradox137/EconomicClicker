using AdvantTest.Settings.Business;
using UnityEngine;

namespace AdvantTest.Service.Player
{
	public static class UpgradeConfig
	{
		private static ulong[] _basicCosts;
		private static ulong[] _basicIncomes;
		private static ulong[] _firstUpgradeIncomeMultiplier;
		private static ulong[] _secondUpgradeIncomeMultiplier;

		public static void Init(ulong[] basicCosts, ulong[] basicIncomes, ulong[] firstUpgradeIncomeMultiplier, ulong[] secondUpgradeIncomeMultiplier)
		{
			_basicCosts = basicCosts;
			_basicIncomes = basicIncomes;
			_firstUpgradeIncomeMultiplier = firstUpgradeIncomeMultiplier;
			_secondUpgradeIncomeMultiplier = secondUpgradeIncomeMultiplier;
		}
		
		public static double CalculateNextLevelCost(uint currentLvl, uint businessNumber) => (currentLvl + 1) * _basicCosts[businessNumber];

		public static double CalculateIncome(uint currentLvl, uint businessNumber, bool isFirstUpgradeBought,bool isSecondUpgradeBought)
		{
			double multiplier = 1;
			
			if (isFirstUpgradeBought)
				multiplier += (_firstUpgradeIncomeMultiplier[businessNumber] * _basicIncomes[businessNumber])/100d;
			if(isSecondUpgradeBought)
				multiplier += (_secondUpgradeIncomeMultiplier[businessNumber] * _basicIncomes[businessNumber])/100d;

			return currentLvl * _basicIncomes[businessNumber] * multiplier;
		}
	}
}
