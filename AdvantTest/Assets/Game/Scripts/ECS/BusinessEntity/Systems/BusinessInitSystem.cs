using AdvantTest.Data;
using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.ECS.BusinessEntity.Events;
using AdvantTest.MVC.Presenter;
using AdvantTest.Provider;
using AdvantTest.Service.Player;
using Leopotam.Ecs;

namespace AdvantTest.ECS.BusinessEntity.Systems
{
	public class BusinessInitSystem : IEcsInitSystem
	{
		private EcsFilter<BusinessComponent, BusinessInitializer> _filter;
		private ViewsData _viewsData;
		private ConfigsData _configsData;
		public void Init()
		{
			foreach (int i in _filter)
			{
				ref var business = ref _filter.Get1(i);
				ref var initializer = ref _filter.Get2(i);
				ref var businessEntity = ref _filter.GetEntity(i);
				
				InitBusinessComponent(initializer.BusinessNumber, initializer.BalancePresenter, ref business);
				
				InitDependencies(businessEntity, initializer.BusinessNumber, ref business);
				
				businessEntity.Del<BusinessInitializer>();
			}
		}
		private void InitBusinessComponent(uint i, BalancePresenter balancePresenter, ref BusinessComponent businessComponent)
		{
			businessComponent.BusinessLvl = PlayerDataService.instance.playerData.BusinessLoadingInfos[i].lvl;
			businessComponent.BusinessNumber = i;
				
			businessComponent.BusinessIncome = UpgradeConfig.CalculateIncome(
				businessComponent.BusinessLvl, businessComponent.BusinessNumber, 
				PlayerDataService.instance.playerData.BusinessLoadingInfos[i].UpgradesAvailable[0],
				PlayerDataService.instance.playerData.BusinessLoadingInfos[i].UpgradesAvailable[1]);

			businessComponent.BusinessNextLvlCost = UpgradeConfig.CalculateNextLevelCost(businessComponent.BusinessLvl, businessComponent.BusinessNumber);
			businessComponent.BalancePresenter = balancePresenter;
			businessComponent.AvailableUpgrades = new bool[2];
			businessComponent.AvailableUpgrades[0] = PlayerDataService.instance.playerData.BusinessLoadingInfos[i].UpgradesAvailable[0];
			businessComponent.AvailableUpgrades[1] = PlayerDataService.instance.playerData.BusinessLoadingInfos[i].UpgradesAvailable[1];
		}

		private void InitDependencies(EcsEntity businessEntity, uint i, ref BusinessComponent businessComponent)
		{
			InitBusinessView(ref businessComponent);
				
			BusinessProvider businessProvider = new BusinessProvider(businessEntity, _viewsData.BusinessViews[i]);

			if (businessComponent.BusinessLvl >= 1)
			{
				businessEntity.Get<EarnIncome>().TimeToWait = _configsData.InGameBusinessesList.BusinessConceptInfos[i].IncomeDelay;
			}
		}
		private void InitBusinessView(ref BusinessComponent businessComponent)
		{
			_viewsData.BusinessViews[businessComponent.BusinessNumber].Init(
				_configsData.BusinessNamesSettings.BusinessInfos[businessComponent.BusinessNumber].BusinessName,
				PlayerDataService.instance.playerData.BusinessLoadingInfos[businessComponent.BusinessNumber].lvl,
				businessComponent.BusinessIncome,
				businessComponent.BusinessNextLvlCost,
				ref businessComponent.IncomeProgressUpdateCallback
			);
		}
	}
}
