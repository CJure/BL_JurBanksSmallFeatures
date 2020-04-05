using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace JurBanksFeatures
{
	class SetPlayerSpeed : DefaultPartySpeedCalculatingModel
	{
		public override float CalculateFinalSpeed(MobileParty mobileParty, float baseSpeed, StatExplainer explanation)
		{
			if(!mobileParty.IsLeaderless && mobileParty.Leader.IsPlayerCharacter)
			{
				if(FollowData.isFollowActive)
				{
					return FollowData.folowParty.LastCachedSpeed;
				}
				return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
			}
			return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
		}
	}
}
