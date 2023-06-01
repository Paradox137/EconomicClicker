using AdvantTest.Data;
using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.ECS.BusinessEntity.Events;
using AdvantTest.ECS.UpgradeEntity.Components;
using AdvantTest.MVC.Model;
using AdvantTest.MVC.Presenter;
using AdvantTest.Provider;
using AdvantTest.Service.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace AdvantTest.ECS.Core
{
	public class GameInitSystem : IEcsInitSystem
	{
		private EcsWorld _world;
		private ViewsData _viewsData; 
		private ConfigsData _configsData;
		public void Init()
		{
			PlayerDataService.instance.Load();
			InitUpgradeConfig();
			CreateWorld();
		}
		
		private void CreateWorld()
		{
			BalancePresenter balancePresenter = InitBalance();
			
			for (uint i = 0; i < _viewsData.BusinessViews.Length; i++)
			{
				EcsEntity businessEntity = InitBusinessEntity(i, balancePresenter);

				for (uint j = 0; j < 2; j++)
				{
					InitUpgradeEntity(i, j, balancePresenter, businessEntity);
				}
			}
		}
		private void InitUpgradeEntity(uint numberBusiness, uint numberUpgrade, BalancePresenter balancePresenter, EcsEntity businessEntity)
		{
			EcsEntity upgradeEntity = _world.NewEntity();

			ref var upgradeInitializer = ref upgradeEntity.Get<UpgradeEntity.Event.UpgradeInitializer>();
			upgradeInitializer.BalancePresenter = balancePresenter;
			upgradeInitializer.BusinessEntity = businessEntity;
			upgradeInitializer.BusinessNumber = numberBusiness;
			upgradeInitializer.UpgradeNumber = numberUpgrade;
					
			upgradeEntity.Get<UpgradeComponent>();
		}
		private EcsEntity InitBusinessEntity(uint number, BalancePresenter balancePresenter)
		{
			EcsEntity businessEntity = _world.NewEntity();
				
			ref var businessInitializer = ref businessEntity.Get<BusinessInitializer>();
			businessInitializer.BusinessNumber = number;
			businessInitializer.BalancePresenter = balancePresenter;
				
			businessEntity.Get<BusinessComponent>();

			return businessEntity;
		}
		private BalancePresenter InitBalance()
		{
			BalanceModel balanceModel = new BalanceModel
			{
				CurrentBalance = PlayerDataService.instance.playerData.Money
			};
			
			BalancePresenter balancePresenter = new BalancePresenter(balanceModel, _viewsData.BalanceView);
			InitBalanceView();
			return balancePresenter;
		}
		private void InitBalanceView()
		{
			_viewsData.BalanceView.Init(PlayerDataService.instance.playerData.Money);
		}
		private void InitUpgradeConfig()
		{
			ulong[] costs = new ulong[_configsData.InGameBusinessesList.BusinessConceptInfos.Length];
			ulong[] incomes = new ulong[_configsData.InGameBusinessesList.BusinessConceptInfos.Length];
			ulong[] firstMultipliers = new ulong[_configsData.InGameBusinessesList.BusinessConceptInfos.Length];
			ulong[] secondMultipliers = new ulong[_configsData.InGameBusinessesList.BusinessConceptInfos.Length];

			for (int i = 0; i < _configsData.InGameBusinessesList.BusinessConceptInfos.Length; i++)
				costs[i] = _configsData.InGameBusinessesList.BusinessConceptInfos[i].BasicCost;

			for (int i = 0; i < _configsData.InGameBusinessesList.BusinessConceptInfos.Length; i++)
				incomes[i] = _configsData.InGameBusinessesList.BusinessConceptInfos[i].BasicIncome;

			for (int i = 0; i < _configsData.InGameBusinessesList.BusinessConceptInfos.Length; i++)
				firstMultipliers[i] = _configsData.InGameBusinessesList.BusinessConceptInfos[i].Upgrades[0].IncomeMultiplier;

			for (int i = 0; i < _configsData.InGameBusinessesList.BusinessConceptInfos.Length; i++)
				secondMultipliers[i] = _configsData.InGameBusinessesList.BusinessConceptInfos[i].Upgrades[1].IncomeMultiplier;

			UpgradeConfig.Init(costs, incomes, firstMultipliers, secondMultipliers);
		}
	}
}
