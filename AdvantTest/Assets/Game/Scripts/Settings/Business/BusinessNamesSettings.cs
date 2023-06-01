using UnityEngine;

namespace AdvantTest.Settings.Business
{
	[CreateAssetMenu(fileName = "Business Names Settings", menuName = "Business Data/Name Settings", order = 0)]
	public class BusinessNamesSettings : ScriptableObject
	{
		[SerializeField]
		private BusinessNamesInfo[] businessInfos;

		public BusinessNamesInfo[] BusinessInfos => businessInfos;
	}
}
