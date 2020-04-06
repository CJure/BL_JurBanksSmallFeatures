
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace JurBanksFeatures
{
	static class FollowData
	{
		static public bool isFollowActive = false;
		static public MobileParty followParty = null;
		static public MBGUID followPartyId = new MBGUID();
		static public float slowdownThreshold = 1;

	}
}
