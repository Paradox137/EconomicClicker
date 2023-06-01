using AdvantTest.ECS.UpgradeEntity.Components;
using AdvantTest.MVC.Presenter;
using Leopotam.Ecs;

namespace AdvantTest.ECS.BusinessEntity.Components
{
	public delegate void IncomeProgressUpdateCallback(float progress);
	public struct BusinessComponent
	{
		public uint BusinessNumber;
		public uint BusinessLvl;
		public double BusinessIncome;
		public double BusinessNextLvlCost;
		public bool[] AvailableUpgrades;
		public IncomeProgressUpdateCallback IncomeProgressUpdateCallback;
		public BalancePresenter BalancePresenter;
	}
}
