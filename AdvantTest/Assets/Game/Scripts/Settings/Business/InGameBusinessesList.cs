using UnityEngine;

namespace AdvantTest.Settings.Business
{
	[CreateAssetMenu(fileName = "In Game Businesses List", menuName = "Business Data/Businesses List", order = 0)]
	public class InGameBusinessesList : ScriptableObject
	{
		[SerializeField]
		private BusinessConceptInfo[] businessConceptInfos;
		
		public BusinessConceptInfo[] BusinessConceptInfos => businessConceptInfos;
	}
}
