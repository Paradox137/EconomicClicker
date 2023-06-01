using AdvantTest.Service.Number;
using TMPro;
using UnityEngine;

namespace AdvantTest.MVC.View
{
	public class BalanceView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _balance;

		public void Init(double balance)
		{
			UpdateBalance(balance);
		}

		public void UpdateBalance(double balance)
		{
			_balance.text = AbbreviationСonverter.AbbreviateNumber(balance);
		}
	}
}
