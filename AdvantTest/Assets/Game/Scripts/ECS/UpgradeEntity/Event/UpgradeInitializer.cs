using AdvantTest.MVC.Presenter;
using Leopotam.Ecs;

namespace AdvantTest.ECS.UpgradeEntity.Event
{
	public struct UpgradeInitializer
	{
		public uint UpgradeNumber;
		public uint BusinessNumber;
		public BalancePresenter BalancePresenter;
		public EcsEntity BusinessEntity;
	}
}
