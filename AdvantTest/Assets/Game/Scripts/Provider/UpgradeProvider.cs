using AdvantTest.ECS.UpgradeEntity.Event;
using AdvantTest.MVC.View;
using AdvantTest.Service.Player;
using Leopotam.Ecs;

namespace AdvantTest.Provider
{
	public delegate void UpgradeBoughtCallback(uint upgradeNumber, uint businessNumber);
	
	public class UpgradeProvider
	{
		private readonly EcsEntity _upgradeEntity;
		private readonly UpgradeView _upgradeView;
		private readonly UpgradeBoughtCallback _upgradeBought;
		
		public UpgradeProvider(EcsEntity upgradeEntity, UpgradeView upgradeView)
		{
			_upgradeEntity = upgradeEntity;
			_upgradeView = upgradeView;
			
			_upgradeBought += BoughtSuccess;
			
			SubscribeButtonActions();
		}
		private void BoughtSuccess(uint upgradeNumber, uint businessNumber)
		{
			_upgradeView.ShowBoughtText();
			
			PlayerDataService.instance.playerData.BusinessLoadingInfos[businessNumber].UpgradesAvailable[upgradeNumber] = true;
			PlayerDataService.instance.Save();
		}
		private void SubscribeButtonActions()
		{
			_upgradeView.BuyButton.onClick.AddListener(TryBuyUpgrade);
		}
		
		private void TryBuyUpgrade()
		{
			_upgradeEntity.Get<TryBuyUpgrade>().UpgradeBought = _upgradeBought;
		}
	}
}
