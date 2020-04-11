using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace JurBanksFeatures
{
    //Credits for helping: Australian, Zervox and Doombox
    public class MySubModule : MBSubModuleBase
    {
        int troopCount = 0;
        protected override void OnSubModuleLoad()
        {
            
            /*  Module.CurrentModule.AddInitialStateOption(new InitialStateOption("Message",
              new TextObject("Message", null),
              9990,
              () => { InformationManager.DisplayMessage(new InformationMessage("Hello World!")); },
              false));*/
        }

        private void getAllHideouts()
        {
            foreach (Settlement settlement in Settlement.All)
            { 
                if(settlement.IsHideout())
                {
                    Debug.WriteLine("hideout scene: " + settlement.GetName());
                }
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            AddModels(gameStarterObject as CampaignGameStarter);
        }

        private void AddModels(CampaignGameStarter gameStarter)
        {
            gameStarter.AddBehavior(new FollowPartyBehaviour());
            gameStarter.AddModel(new SetPlayerSpeed());
            //gameStarter.AddModel(new HideoutMission());
            gameStarter.AddBehavior(new HideoutMission());
            //    gameStarter.AddModel(new HideoutMission());
        }


    /*  public override void OnMissionBehaviourInitialize(Mission mission)
        {
            //getAllHideouts();

            MissionBehaviour missionBehaviour = mission.GetMissionBehaviour<MissionBehaviour>();
            Debug.WriteLine("mission: " + missionBehaviour.ToString() + ", scene name: " + missionBehaviour.Mission.SceneName);
            if (checkIfBanditMission(missionBehaviour.Mission.SceneName))
            {
                mission.AddMissionBehaviour(new HideoutMission());
            }
            base.OnMissionBehaviourInitialize(mission);  
        }


        private bool checkIfBanditMission(string sceneName)
        {
            return sceneName.Contains("bandit");
        }*/
    }
}

    

