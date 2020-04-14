using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.CampaignSystem;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace JurBanksFeatures
{
	class HideoutBaseBehaviour : CampaignBehaviorBase
	{
		
		public TroopRoster troopRosterRemovedTroops = new TroopRoster();
		Boolean areTroopsRemoved = false;
		DefaultTroopCountLimitModel limitModel = new DefaultTroopCountLimitModel();
		int numOfTroopNeeded;


		public override void RegisterEvents()
		{
			CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettelmentLeft));
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettelmentEntered)); 
		}

		private void OnSettelmentEntered(MobileParty party, Settlement settlement, Hero arg3)
		{
			numOfTroopNeeded = limitModel.GetHideoutBattlePlayerMaxTroopCount();
			if (party != null && party.Leader != null)
			{
				if (!party.IsLeaderless && party.Leader.IsPlayerCharacter)
				{
					Debug.WriteLine("player entered settlment: " + settlement.Name);
					if (settlement.IsHideout())
					{
						areTroopsRemoved = true;
						RemoveTroops();
					}
				}
			}
		}

		public void OnSettelmentLeft(MobileParty party, Settlement settlement)
		{
			Debug.WriteLine("player left settlment: " + settlement.Name);
			if (party != null && party.Leader != null)
			{
				Debug.WriteLine("player left settlment, arugemnts not null, troopsRemoved: " + areTroopsRemoved, ", isPChar: " + party.Leader.IsPlayerCharacter + ", isleaderless: " + party.IsLeaderless);
				if (!party.IsLeaderless && party.Leader.IsPlayerCharacter && areTroopsRemoved)
				{
					AddTroops();
				}
			}
		}

		private void AddTroops()
		{
			Debug.WriteLine("adding troops, count: " + troopRosterRemovedTroops.Count());
			MobileParty heroParty = MobileParty.MainParty;
			TroopRoster troops = heroParty.MemberRoster;
			troops.Add(troopRosterRemovedTroops);
			troopRosterRemovedTroops = new TroopRoster();
			areTroopsRemoved = false;
		}

		private void RemoveTroops()
		{

			MobileParty heroParty = MobileParty.MainParty;
			TroopRoster troops = heroParty.MemberRoster;
			TroopRosterElement troopElement;
			CharacterObject troop;
			int troopTypeCount;
			int numOfTroopsToTake;
			TroopRoster tempTroopRoster;
			for (int i = 0; i < troops.Count; i++)
			{
				tempTroopRoster = new TroopRoster();
				troop = troops.GetCharacterAtIndex(i);
				troopElement = troops.GetElementCopyAtIndex(i);
				Debug.WriteLine("troopElement " + i + ": " + troopElement.Character.Name);
				troopTypeCount = troops.GetTroopCount(troop);
				Debug.WriteLine("cons2 Troop " + i + ": " + troop.Name + ", count:" + troopTypeCount);
				if (numOfTroopNeeded > 0)
				{
					 numOfTroopsToTake = getNumTroopNeeded(troop, troopTypeCount);
					int numOfTroopsToRemove = troopTypeCount - numOfTroopsToTake;
					Debug.WriteLine("take Troop " + troop.Name + ", num to take: " + numOfTroopsToTake + ", num to remove: " + numOfTroopsToRemove);
					numOfTroopNeeded -= numOfTroopsToTake;
					if (numOfTroopsToRemove > 0)
					{
						Debug.WriteLine("remove Troop1 " + troop.Name + ", num: " + numOfTroopsToRemove);
						CharacterObject tempTroop = troopElement.Character;
						tempTroopRoster.FillMembersOfRoster(numOfTroopsToTake, troop);
						troopRosterRemovedTroops.Add(tempTroopRoster);
						Debug.WriteLine("troopRoosterRemovedTroops count" + troopRosterRemovedTroops.Count());
						troops.RemoveTroop(troop, numOfTroopsToRemove);
						i--;
					}
				}
				else
				{
					Debug.WriteLine("remove Troop2 " + troop.Name + ", num: " + troopTypeCount + ", troopCound: " + troopTypeCount);
					CharacterObject tempTroop = troopElement.Character;
					tempTroopRoster.FillMembersOfRoster(troopTypeCount, troop);
					Debug.WriteLine("output temp roster: ");
					outputRoster(troopRosterRemovedTroops);
					troopRosterRemovedTroops.Add(tempTroopRoster);
					Debug.WriteLine("output roster: ");
					outputRoster(troopRosterRemovedTroops);
					Debug.WriteLine("troopRoosterRemovedTroops count" + troopRosterRemovedTroops.Count() + ", troopRoster tootalCount: " + troopRosterRemovedTroops.TotalManCount);
					troops.RemoveTroop(troop, troopTypeCount);
					i--;
				}
			}
			Debug.WriteLine("end of remove troops");
		}

		private void outputRoster(TroopRoster roster)
		{
			for(int i = 0; i < roster.Count(); i++)
			{
				CharacterObject troop = roster.GetCharacterAtIndex(i);
				Debug.WriteLine("roster member: " + troop.Name + ", count: " + roster.GetTroopCount(troop));
			}
		}

		private int getNumTroopNeeded(CharacterObject troop, int troopCount)
		{
			int result = troopCount - numOfTroopNeeded;
			if (result < 0) result = 0;
			result = troopCount - result;
			return result;
		}

		public override void SyncData(IDataStore dataStore)
		{
			
		}
	}
}
