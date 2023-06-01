using AdvantTest.Data;
using AdvantTest.ECS.UpgradeEntity.Components;
using AdvantTest.ECS.UpgradeEntity.Event;
using AdvantTest.Provider;
using AdvantTest.Service.Player;
using Leopotam.Ecs;

namespace AdvantTest.ECS.UpgradeEntity.Systems
{
	public class UpgradeInitSystem : IEcsInitSystem
	{
		private EcsFilter<UpgradeComponent, UpgradeInitializer> _filter;
		private ViewsData _viewsData;
		private ConfigsData _configsData;
		public void Init()
		{
			foreach (int i in _filter)
			{
				ref var upgradeComponent = ref _filter.Get1(i);
				ref var initializer = ref _filter.Get2(i);
				ref var entity = ref _filter.GetEntity(i);

				InitUpgradeComponent(initializer.BusinessNumber, initializer.UpgradeNumber, ref upgradeComponent, ref initializer);
				InitDependencies(initializer.BusinessNumber, initializer.UpgradeNumber, ref upgradeComponent, entity);
				
				entity.Del<UpgradeInitializer>();
			}
		}
		
		private void InitUpgradeComponent(uint i, uint j, ref UpgradeComponent upgradeComponent,ref UpgradeInitializer initializer)
		{
			upgradeComponent.UpgradeNumber = j;
			upgradeComponent.BalancePresenter = initializer.BalancePresenter;
			upgradeComponent.BusinessEntity = initializer.BusinessEntity;
			upgradeComponent.IncomeMultiplier = _configsData.InGameBusinessesList.BusinessConceptInfos[i].Upgrades[j].IncomeMultiplier;
			upgradeComponent.UpgradeCost = _configsData.InGameBusinessesList.BusinessConceptInfos[i].Upgrades[j].UpgradeCost;
			upgradeComponent.IsBought = PlayerDataService.instance.playerData.BusinessLoadingInfos[i].UpgradesAvailable[j];
		}

		private void InitDependencies(uint i, uint j, ref UpgradeComponent upgradeComponent, EcsEntity upgradeEntity)
		{
			_viewsData.BusinessViews[i].UpgradeViews[j].Init
			(_configsData.BusinessNamesSettings.BusinessInfos[i].BusinessUpgradesNames[j],
				upgradeComponent.IncomeMultiplier,
				upgradeComponent.UpgradeCost,
				upgradeComponent.IsBought);
			UpgradeProvider upgradeProvider = new UpgradeProvider(upgradeEntity, _viewsData.BusinessViews[i].UpgradeViews[j]);
		}
	}
}
