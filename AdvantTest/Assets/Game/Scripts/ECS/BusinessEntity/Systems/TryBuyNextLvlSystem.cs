using AdvantTest.Data;
using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.ECS.BusinessEntity.Events;
using AdvantTest.Service.Player;
using Leopotam.Ecs;

namespace AdvantTest.ECS.BusinessEntity.Systems
{
	public class TryBuyNextLvlSystem : IEcsRunSystem
	{
		private EcsFilter<BusinessComponent, TryBuyLvl> _filter;
		
		private ConfigsData _configsData;
		public void Run()
		{
			foreach (int i in _filter)
			{
				ref var business = ref _filter.Get1(i);
				ref var sender = ref _filter.Get2(i);
				ref var entity = ref _filter.GetEntity(i);

				if (business.BalancePresenter.IsEnoughMoneyToBuy(business.BusinessNextLvlCost))
				{
					business.BusinessLvl++;
					business.BusinessIncome = UpgradeConfig.CalculateIncome(
						business.BusinessLvl, business.BusinessNumber, 
						business.AvailableUpgrades[0], business.AvailableUpgrades[1]);
					
					business.BusinessNextLvlCost = UpgradeConfig.CalculateNextLevelCost(business.BusinessLvl, business.BusinessNumber);

					if (business.BusinessLvl == 1)
						entity.Get<EarnIncome>().TimeToWait = _configsData.InGameBusinessesList.BusinessConceptInfos[business.BusinessNumber].IncomeDelay;

					sender.NextLvlBought?.Invoke(business.BusinessNumber,business.BusinessLvl, business.BusinessIncome, business.BusinessNextLvlCost);
				}
				
				entity.Del<TryBuyLvl>();
			}
		}
	}
}
