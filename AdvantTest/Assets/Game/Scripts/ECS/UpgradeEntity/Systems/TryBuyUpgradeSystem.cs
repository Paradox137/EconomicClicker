using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.ECS.UpgradeEntity.Components;
using AdvantTest.ECS.UpgradeEntity.Event;
using Leopotam.Ecs;

namespace AdvantTest.ECS.UpgradeEntity.Systems
{
	public class TryBuyUpgradeSystem : IEcsRunSystem
	{
		private EcsFilter<UpgradeComponent, TryBuyUpgrade> _filter;
		public void Run()
		{
			foreach (int i in _filter)
			{
				ref var upgrade = ref _filter.Get1(i);
				ref var sender = ref _filter.Get2(i);
				ref var entity = ref _filter.GetEntity(i);

				if (!upgrade.IsBought && upgrade.BalancePresenter.IsEnoughMoneyToBuy(upgrade.UpgradeCost))
				{
					upgrade.IsBought = true;

					sender.UpgradeBought?.Invoke(upgrade.UpgradeNumber, upgrade.BusinessEntity.Get<BusinessComponent>()
						.BusinessNumber);
				}
				
				entity.Del<TryBuyUpgrade>();
			}
		}
	}
}
