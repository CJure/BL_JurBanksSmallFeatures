using System;
using System.Diagnostics;
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
					float followPartySpeed = FollowData.followParty.LastCachedSpeed;
					
					if (CalculateDistanceToParty(mobileParty, FollowData.followParty) <= FollowData.slowdownThreshold)
					{
						return followPartySpeed;
					}
					else
					{
						return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
					}
				}
				return unChangedPartySpeed;
			}
			return unChangedPartySpeed;
		}

        private float CalculateDistanceToParty(MobileParty heroParty, MobileParty targetParty)
        {
            float distanceToTarget = heroParty.Position2D.Distance(targetParty.Position2D);
            System.Diagnostics.Debug.WriteLine("Distance to target: " + distanceToTarget); //A distance of "1" seems to be a good follow distance 
            return distanceToTarget;
        }
        
	}
}
