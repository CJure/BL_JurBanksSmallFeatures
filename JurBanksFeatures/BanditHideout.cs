using System.Diagnostics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace JurBanksFeatures
{
	class BanditHideout : Hideout
	{
		/*	public override void AfterStart()  //HideoutPhasedMissionController
			{
				Debug.WriteLine("Bandit mission after start: ");
				MobileParty heroParty = MobileParty.MainParty;
				TroopRoster troops = heroParty.MemberRoster;
				for(int i = 0; i < troops.Count; i++)
				{
					CharacterObject troop = troops.GetCharacterAtIndex(i);
					Debug.WriteLine("Troop i: " + troop.Name);
				}
			}*/

		public override void OnPartyEntered(MobileParty heroParty)
		{
			Debug.WriteLine("Bandit mission after start: ");
			TroopRoster troops = heroParty.MemberRoster;
			for (int i = 0; i < troops.Count; i++)
			{
				CharacterObject troop = troops.GetCharacterAtIndex(i);
				Debug.WriteLine("Troop i: " + troop.Name);
			}
		}

	}
}
