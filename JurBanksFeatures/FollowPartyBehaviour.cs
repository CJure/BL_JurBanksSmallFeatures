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
			if (FollowData.isFollowActive) checkIfFollowPartyStillOnMap();
		}

		private void checkForUserInput()
		{
			if (topScreen != null)
			{
				if (topScreen.DebugInput.IsKeyPressed(InputKey.RightMouseButton))
				{
					toggleFollowOnMouseClick();
					Debug.WriteLine(" Mouse is down: ");
				}
				else if(topScreen.DebugInput.IsKeyPressed(InputKey.LeftMouseButton))
				{
					FollowData.isFollowActive = false;
				}
			}
		}

		private void toggleFollowOnMouseClick()
		{
			if (FollowData.isFollowActive)
			{
				FollowData.isFollowActive = false;
				InformationManager.DisplayMessage(new InformationMessage("Stoped following"));
			}
			else
			{
				if (MobileParty.MainParty.TargetParty != null)
				{
					FollowData.isFollowActive = true;
					FollowData.followParty = MobileParty.MainParty.TargetParty;
					InformationManager.DisplayMessage(new InformationMessage("Following " + MobileParty.MainParty.TargetParty.Name));
				}
			}
		}

		private void checkIfFollowPartyStillOnMap()
		{
			if (FollowData.followParty != null)
			{
				if (FollowData.followParty.IsWaiting()) FollowData.isFollowActive = false;
			}
		}
	}
}
