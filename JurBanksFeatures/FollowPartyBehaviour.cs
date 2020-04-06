using System;
using System.Diagnostics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;

namespace JurBanksFeatures
{
	class FollowPartyBehaviour : CampaignBehaviorBase
	{
		ScreenBase topScreen = ScreenManager.TopScreen;

		public override void RegisterEvents()
		{
			Debug.WriteLine("Register events");
			CampaignEvents.TickEvent.AddNonSerializedListener(this, this.doOnTick);
		}

		public override void SyncData(IDataStore dataStore)
		{

		}

		private void doOnTick(float obj)
		{
			checkForUserInput();
			if (FollowData.getIsFollowActive()) checkIfFollowPartyStillOnMap();
		}

		private void checkForUserInput()
		{
			if (topScreen != null)
			{
				if (topScreen.DebugInput.IsKeyPressed(InputKey.F))
				{
					toggleFollowOnMouseClick();
					Debug.WriteLine(" Mouse is down: ");
				}
				else if(topScreen.DebugInput.IsKeyPressed(InputKey.LeftMouseButton))
				{
					if(FollowData.getIsFollowActive()) FollowData.stopFollowing();
				}
			}
		}

		private void toggleFollowOnMouseClick()
		{
			if (FollowData.getIsFollowActive())
			{
				FollowData.stopFollowing();
				
			}
			else
			{
				if (MobileParty.MainParty.TargetParty != null)
				{
					FollowData.startFollowing(MobileParty.MainParty.TargetParty);
				}
			}
		}

		private void checkIfFollowPartyStillOnMap()
		{
			if (FollowData.getFollowParty() != null)
			{
				if (FollowData.getFollowParty().IsWaiting()) FollowData.stopFollowing();
			}
		}
	}
}
