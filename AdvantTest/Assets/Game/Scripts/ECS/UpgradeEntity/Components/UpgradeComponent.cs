using AdvantTest.MVC.Presenter;
using Leopotam.Ecs;

namespace AdvantTest.ECS.UpgradeEntity.Components
{
	public struct UpgradeComponent
	{
		public uint UpgradeNumber;
		public bool IsBought;
		public double UpgradeCost;
		public double IncomeMultiplier;
		public EcsEntity BusinessEntity;
		public BalancePresenter BalancePresenter;
	}
}
