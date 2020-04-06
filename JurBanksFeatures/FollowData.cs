
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace JurBanksFeatures
{
	static class FollowData
	{
		static private  bool isFollowActive = false;
		static private MobileParty followParty = null;
		static public float slowdownThreshold = 1;

		static public bool getIsFollowActive()
		{
			return isFollowActive;
		}

		static public void startFollowing(MobileParty party)
		{
			followParty = party;
			isFollowActive = true;
			InformationManager.DisplayMessage(new InformationMessage("Started following party: " + followParty.Name));

		}

		static public void stopFollowing()
		{
			InformationManager.DisplayMessage(new InformationMessage("Stoped following party: " + followParty.Name));
			isFollowActive = false;
		}

		static public MobileParty getFollowParty()
		{
			return followParty;
		}
	}
}
