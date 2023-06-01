using AdvantTest.ECS.BusinessEntity.Events;
using AdvantTest.MVC.View;
using AdvantTest.Service.Player;
using Leopotam.Ecs;

namespace AdvantTest.Provider
{
	public delegate void NextLvlBoughtCallback(uint businessNumber, uint newLvl, double newIncome, double newNextLvlCost);
	
	public class BusinessProvider
	{
		private readonly EcsEntity _businessEntity;
		private readonly BusinessView _businessView;
		private readonly NextLvlBoughtCallback _nextLvlBought;
		
		public BusinessProvider(EcsEntity businessEntity, BusinessView businessView)
		{
			_businessEntity = businessEntity;
			_businessView = businessView;
			
			_nextLvlBought += BoughtSuccess;
			
			SubscribeButtonActions();
		}
		
		private void BoughtSuccess(uint businessNumber, uint newLvl, double newIncome, double newNextLvlCost)
		{
			_businessView.UpdateLvl(newLvl);
			_businessView.UpdateIncome(newIncome);
			_businessView.UpdateNextLvlCost(newNextLvlCost);
			
			PlayerDataService.instance.playerData.BusinessLoadingInfos[businessNumber].lvl = newLvl;
			PlayerDataService.instance.Save();
		}
		private void SubscribeButtonActions()
		{
			_businessView.NextLvlButton.onClick.AddListener(TryUpgradeToNextLvl);
		}
		private void TryUpgradeToNextLvl()
		{
			_businessEntity.Get<TryBuyLvl>().NextLvlBought = _nextLvlBought;
		}
	}
}
