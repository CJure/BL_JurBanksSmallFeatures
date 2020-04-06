using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace JurBanksFeatures
{
    //Credits for helping: Australian, Zervox and Doombox
    public class MySubModule : MBSubModuleBase
    {
        
        protected override void OnSubModuleLoad()
        {
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
        }
        
    }
}

    

