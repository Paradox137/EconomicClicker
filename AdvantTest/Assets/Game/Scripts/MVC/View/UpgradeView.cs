using AdvantTest.Service.Number;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdvantTest.MVC.View
{
	public class UpgradeView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _upgradeName;
		
		[SerializeField] private TextMeshProUGUI _incomeMultiplier;
						
		[SerializeField] private TextMeshProUGUI _upgradeCost;
			
		[SerializeField] private Button _buyButton;

		[SerializeField] private TextMeshProUGUI _boughtText;
		
		[SerializeField] private GameObject PriceGroup;
		
		public Button BuyButton => _buyButton;

		public void Init(string upgradeName, double incomeMultiplier, double upgradeCost, bool isBought)
		{
			ChangeName(upgradeName);
			ChangeMultiplier(incomeMultiplier);
			
			if (isBought)
				ShowBoughtText();
			
			ChangeUpgradeCost(upgradeCost);
		}

		public void ChangeUpgradeCost(double upgradeCost)
		{
			_upgradeCost.text = AbbreviationСonverter.AbbreviateNumber(upgradeCost);
		}
		public void ChangeName(string upgradeName)
		{
			_upgradeName.text = upgradeName;
		}
		public void ChangeMultiplier( double incomeMultiplier)
		{
			_incomeMultiplier.text = AbbreviationСonverter.AbbreviateNumber(incomeMultiplier);
		}
		public void ShowBoughtText()
		{
			PriceGroup.SetActive(false);
			_boughtText.enabled = true;
		}
	}
}
