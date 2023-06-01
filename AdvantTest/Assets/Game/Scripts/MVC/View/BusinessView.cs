using AdvantTest.ECS.BusinessEntity.Components;
using AdvantTest.Service.Number;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdvantTest.MVC.View
{
	public class BusinessView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _businessName;

		[SerializeField] private TextMeshProUGUI _lvl;

		[SerializeField] private TextMeshProUGUI _income;

		[SerializeField] private TextMeshProUGUI _nextLvlCost;
		
		[SerializeField] private Image _progressBar;

		[SerializeField] private Button _nextLvlButton;

		[SerializeField] private UpgradeView[] _upgradeViews;
		
		public Button        NextLvlButton => _nextLvlButton;
		public UpgradeView[] UpgradeViews => _upgradeViews;

		public void Init(string businessName, uint lvl, double income, double nextLvlCost, ref IncomeProgressUpdateCallback incomeProgressUpdateCallback)
		{
			UpdateNames(businessName);
			UpdateLvl(lvl);
			UpdateNextLvlCost(nextLvlCost);
			UpdateIncome(income);
			
			Subscribe(ref incomeProgressUpdateCallback);
		}

		private void Subscribe(ref IncomeProgressUpdateCallback incomeProgressUpdateCallback)
		{
			incomeProgressUpdateCallback += UpdateProgressBar;
		}

		public void UpdateLvl(uint lvl) =>_lvl.text = lvl.ToString();
		
		public void UpdateNextLvlCost(double nextLvlCost) =>_nextLvlCost.text = AbbreviationСonverter.AbbreviateNumber(nextLvlCost);
		
		public void UpdateIncome(double income) =>_income.text = AbbreviationСonverter.AbbreviateNumber(income);

		private void UpdateNames(string businessName) =>_businessName.text = businessName;

		private void UpdateProgressBar(float value) =>_progressBar.fillAmount = value;
		
	}
}
