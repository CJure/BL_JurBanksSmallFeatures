using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;


namespace JurBanksFeatures
{
    //Credits for helping: Australian, Zervox and Doombox
    public class MySubModule : MBSubModuleBase
    {
        bool enableFollowFeature = true;
        bool enableHideoutFeature = true;

        protected override void OnSubModuleLoad()
        {
            var module = Module.CurrentModule;
            module.AddInitialStateOption(new InitialStateOption(
            "JurbankOptions",
            new TextObject("JurBankFeatures options"),
            10001,
            ShowModOptions,
            false
          ));
            base.OnSubModuleLoad();
        }

        private void ShowModOptions()
        {
            var elements = new List<InquiryElement>();

            elements.Add(new InquiryElement(
              nameof(enableFollowFeature),
              enableFollowFeature ? "Disable follow party" : "Enable follow party",
              null
            ));

            elements.Add(new InquiryElement(
              nameof(enableHideoutFeature),
              enableHideoutFeature ? "Disable hideout feature" : "Enable hideout feature",
              null
            ));
           
            InformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(
              "Mod Options",
              "JurBankFeatures options",
              elements,
              true,
              true,
              "Apply",
              "Return",
              list => {
                  var selected = (string)list[0].Identifier;
                  switch (selected)
                  {
                      case nameof(enableFollowFeature):
                          enableFollowFeature = !enableFollowFeature;
                          ShowMessage($"Follow feature: {(enableFollowFeature ? "Enabled" : "Disabled")}.");
                          //Options.Save();
                          break;
                      case nameof(enableHideoutFeature):
                          enableHideoutFeature = !enableHideoutFeature;
                          ShowMessage($"Hideout feature: {(enableHideoutFeature ? "Enabled" : "Disabled")}.");
                          //Options.Save();
                          break;
                      default:
                          throw new NotImplementedException(selected);
                  }
              }, null));
        }

        private static void ShowMessage(string msg)
        {
            InformationManager.DisplayMessage(new InformationMessage(msg));
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
            if (gameStarterObject is CampaignGameStarter cgs)
            {
                AddModels(gameStarterObject as CampaignGameStarter);
            }
        }

        private void AddModels(CampaignGameStarter gameStarter)
        {
            Debug.WriteLine("follow: " + enableFollowFeature + ", hideout: " + enableHideoutFeature);
            if(enableFollowFeature)
            {
                gameStarter.AddBehavior(new FollowPartyBehaviour());
                gameStarter.AddModel(new SetPlayerSpeed());
            }
            if(enableHideoutFeature)
            {
                gameStarter.AddBehavior(new HideoutMission());
            }
            
            //gameStarter.AddModel(new HideoutMission());
            
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

    

