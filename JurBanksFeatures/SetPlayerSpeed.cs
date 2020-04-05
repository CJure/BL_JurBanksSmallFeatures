using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace JurBanksFeatures
{
	class SetPlayerSpeed : DefaultPartySpeedCalculatingModel
	{
		public override float CalculateFinalSpeed(MobileParty mobileParty, float baseSpeed, StatExplainer explanation)
		{
			float unChangedPartySpeed  = base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
			if (!mobileParty.IsLeaderless && mobileParty.Leader.IsPlayerCharacter)
			{
				if(FollowData.isFollowActive)
				{
					float followPartySpeed = FollowData.folowParty.LastCachedSpeed;
					if (followPartySpeed > unChangedPartySpeed) return unChangedPartySpeed;
					return followPartySpeed;

				}
				return unChangedPartySpeed;
			}
			return unChangedPartySpeed;
		}
	}
}
