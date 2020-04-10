using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;

namespace JurBanksFeatures
{
	class HideoutMission : MissionLogic
	{
		int troopNeeded = 9;

		TroopRoster troopsOriginal;
		public HideoutMission()
		{
			MobileParty heroParty = MobileParty.MainParty;
			TroopRoster troops = heroParty.MemberRoster;
			//troopsOriginal = SystemExtension.Clone(troops);
			RemoveTroops();
		}

		private void RemoveTroops()
		{
			
			MobileParty heroParty = MobileParty.MainParty;
			TroopRoster troops = heroParty.MemberRoster;
			Debug.WriteLine("troop count 1: " + troops.Count);
			Debug.WriteLine("troop count 2: " + troops.Count);

			for (int i = 0; i < troops.Count; i++)
			{
				CharacterObject troop = troops.GetCharacterAtIndex(i);
				Debug.WriteLine("cons Troop " + i + ": " + troop.Name);
			}
			for (int i = 0; i < troops.Count; i++)
			{
				Debug.WriteLine("i for seccond loop: " + i);
				CharacterObject troop = troops.GetCharacterAtIndex(i);
				int troopTypeCount = troops.GetTroopCount(troop);
				
				Debug.WriteLine("cons2 Troop " + i +": " + troop.Name + ", count:" + troopTypeCount);
				
				if (troopNeeded > 0)
				{
					int numOfTroopsToTake = getNumTroopNeeded(troop, troopTypeCount);
					int numOfTroopsToRemove = troopTypeCount - numOfTroopsToTake;
					Debug.WriteLine("take Troop " + troop.Name + ", num to take: " + numOfTroopsToTake + ", num to remove: " + numOfTroopsToRemove);
					troopNeeded -= numOfTroopsToTake;
					if (numOfTroopsToRemove > 0)
					{
						Debug.WriteLine("remove Troop1 " + troop.Name + ", num: " + numOfTroopsToRemove);
						troops.RemoveTroop(troop, numOfTroopsToRemove);
						i--;
					}
				}
				else
				{
					Debug.WriteLine("remove Troop2 " + troop.Name + ", num: " + troopTypeCount);
					troops.RemoveTroop(troop, troopTypeCount);
					i--;
				}
			}
			Debug.WriteLine("end of remove troops");
		}

		private int getNumTroopNeeded(CharacterObject troop, int troopCount)
		{
			int result = troopCount - troopNeeded;
			if (result < 0) result = 0;
			result = troopCount - result;
			return result;
		}

		protected override void OnEndMission()
		{
			base.OnEndMission();
			//
			MobileParty.MainParty.MemberRoster.Add(troopsOriginal);
		}

		public override void AfterStart()  //HideoutPhasedMissionController
		{
			base.AfterStart();
			Debug.WriteLine("Bandit mission after start: ");

			MobileParty heroParty = MobileParty.MainParty;
			TroopRoster troops = heroParty.MemberRoster;
			for (int i = 0; i < troops.Count; i++)
			{
				CharacterObject troop = troops.GetCharacterAtIndex(i);
				Debug.WriteLine("after Troop " + i + ": " + troop.Name);
			}
		}
	}
}
