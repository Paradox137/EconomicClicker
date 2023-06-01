using AdvantTest.Settings.Business;
using UnityEngine;

namespace AdvantTest.Data
{
	public class ConfigsData : MonoBehaviour
	{
		[SerializeField]
		private BusinessNamesSettings _businessNamesSettings;

		[SerializeField]
		private InGameBusinessesList _inGameBusinessesList;
		
		public BusinessNamesSettings BusinessNamesSettings => _businessNamesSettings;
		public InGameBusinessesList InGameBusinessesList => _inGameBusinessesList;
	}
}
