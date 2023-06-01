using System;
using UnityEngine;

namespace AdvantTest.Logic.Business
{
	[Serializable]
	public struct BusinessLoadingInfo
	{
		[field: SerializeField]
		public uint lvl { get; set; }
		
		[field: SerializeField]
		public bool[] UpgradesAvailable { get; set; }
	}
}
