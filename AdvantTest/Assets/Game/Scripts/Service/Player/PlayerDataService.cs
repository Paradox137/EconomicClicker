using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AdvantTest.Logic.Business;
using UnityEngine;

namespace AdvantTest.Service.Player
{
	public class PlayerDataService : MonoBehaviour
	{
		public static PlayerDataService instance;

		void Awake()
		{
			if (!instance)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else Destroy(gameObject);

			Load();
		}

		public PlayerInfo saveGame;

		public PlayerInfo playerData
		{ get => saveGame;
		  set => saveGame = value; }

		public void Save()
		{
			FileStream file = new FileStream(Application.persistentDataPath + "/player.dat", FileMode.OpenOrCreate);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(file, saveGame);
			file.Close();
		}

		public void Load()
		{
			if (File.Exists(Application.persistentDataPath + "/player.dat"))
			{
				FileStream file = new FileStream(Application.persistentDataPath + "/player.dat", FileMode.Open);
				BinaryFormatter formatter = new BinaryFormatter();
				saveGame = formatter.Deserialize(file) as PlayerInfo;
				file.Close();
			}
			else
			{
				instance.playerData.Money = 0;
				instance.playerData.BusinessLoadingInfos = new BusinessLoadingInfo[5];
				instance.playerData.BusinessLoadingInfos[0].lvl = 1;
				
				for (int i = 0; i < instance.playerData.BusinessLoadingInfos.Length; i++)
					instance.playerData.BusinessLoadingInfos[i].UpgradesAvailable = new bool[2];

				Save();
			}
		}
	}

	[System.Serializable]
	public class PlayerInfo
	{
		public double Money;
		public BusinessLoadingInfo[] BusinessLoadingInfos;
	}
}
