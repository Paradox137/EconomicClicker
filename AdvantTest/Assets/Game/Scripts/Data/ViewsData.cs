using AdvantTest.MVC.View;
using UnityEngine;

namespace AdvantTest.Data
{
	public class ViewsData : MonoBehaviour
	{
		[SerializeField]
		private BalanceView _balanceView;
		
		[SerializeField]
		private BusinessView[] _businessViews;
		
		public BalanceView BalanceView => _balanceView;
		public BusinessView[] BusinessViews => _businessViews;
	}
}
