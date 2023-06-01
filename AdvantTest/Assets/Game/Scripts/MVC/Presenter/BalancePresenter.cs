using AdvantTest.MVC.Model;
using AdvantTest.MVC.View;
using AdvantTest.Service.Player;

namespace AdvantTest.MVC.Presenter
{
	public class BalancePresenter
	{
		private BalanceModel _balanceModel;
		private readonly BalanceView _balanceView;

		public BalancePresenter(BalanceModel balanceModel, BalanceView balanceView)
		{
			_balanceModel = balanceModel;
			_balanceView = balanceView;
		}
		
		public void ReceiveMoney(double money)
		{
			_balanceModel.CurrentBalance += money;	
				
			_balanceView.UpdateBalance(_balanceModel.CurrentBalance);
			
			PlayerDataService.instance.playerData.Money = _balanceModel.CurrentBalance;
			PlayerDataService.instance.Save();
			PlayerDataService.instance.Load();
		}

		public bool IsEnoughMoneyToBuy(double neededMoney)
		{
			if (_balanceModel.CurrentBalance >= neededMoney)
			{
				_balanceModel.CurrentBalance -= neededMoney;
				
				_balanceView.UpdateBalance(_balanceModel.CurrentBalance);
				
				PlayerDataService.instance.playerData.Money = _balanceModel.CurrentBalance;
				PlayerDataService.instance.Save();
				PlayerDataService.instance.Load();

				return true;
			}

			return false;
		}
	}
}
