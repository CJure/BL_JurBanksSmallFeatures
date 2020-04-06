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
			if(!mobileParty.IsLeaderless && mobileParty.Leader.IsPlayerCharacter)
			{
				if(FollowData.isFollowActive)
				{
                    if (CalculateDistanceToParty(mobileParty, FollowData.followParty) <= FollowData.slowdownThreshold)
                    {
                        return FollowData.followParty.LastCachedSpeed;
                    }
                    else
                    {
                        return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
                    }
                }
				return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
			}
			return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
		}


        private float CalculateDistanceToParty(MobileParty heroParty, MobileParty targetParty)
        {
            float distanceToTarget = heroParty.Position2D.Distance(targetParty.Position2D);
            System.Diagnostics.Debug.WriteLine("Distance to target: " + distanceToTarget); //A distance of "1" seems to be a good follow distance 
            return distanceToTarget;
        }
        
	}
}
