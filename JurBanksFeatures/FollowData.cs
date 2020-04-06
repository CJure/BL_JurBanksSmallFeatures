
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace JurBanksFeatures
{
	static class FollowData
	{
		static public bool isFollowActive = false;
		static public MobileParty folowParty = null;
		static public MBGUID followPartyId = new MBGUID();
		static public float slowdownThreshold = 1;
	}
}
