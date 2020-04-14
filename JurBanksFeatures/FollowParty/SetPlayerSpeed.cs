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
			if (FollowData.getIsFollowActive())
			{
				if (mobileParty != null && mobileParty.Leader != null)
				{ 
					float unChangedPartySpeed = base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
					if (!mobileParty.IsLeaderless && mobileParty.Leader.IsPlayerCharacter)
					{
						if (FollowData.getIsFollowActive())
						{
							float followPartySpeed = FollowData.getFollowParty().LastCachedSpeed;
							float distanceToParty = CalculateDistanceToParty(mobileParty, FollowData.getFollowParty());
							if (distanceToParty <= FollowData.slowdownThreshold)
							{
								return followPartySpeed * (distanceToParty / FollowData.slowdownThreshold);
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
				return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
			}
			return base.CalculateFinalSpeed(mobileParty, baseSpeed, explanation);
		}

        private float CalculateDistanceToParty(MobileParty heroParty, MobileParty targetParty)
        {
            float distanceToTarget = heroParty.Position2D.Distance(targetParty.Position2D);
            Debug.WriteLine("Distance to target: " + distanceToTarget); 
            return distanceToTarget;
        }
	}
}
