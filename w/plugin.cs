using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.CreditTags;
using System.Runtime.CompilerServices;
using w.Hander;
using PluginAPI.Core;
using InventorySystem.Items.Usables;
using Exiled.CustomRoles.Events;
using Log = Exiled.API.Features.Log;
using Exiled.CustomRoles.API.Features;
using w.API;
using PlayerEvents = Exiled.Events.Handlers.Player;
using ServerEvents = Exiled.Events.Handlers.Server;
using w.effect;
using CustomRole = Exiled.CustomRoles.API.Features.CustomRole;

namespace w
{
    public class plugin:Plugin<Config>
    {
        public override string Author => "XuQingTW";
        public override string Name => "w";
        public override Version Version => new Version(1, 0, 0);

        public Dictionary<StartTeam, List<ICustomRole>> Roles = new(); // 抄的

        public plugin instance;//似乎用不到
        public Methods Methods { get; private set; } = null!;// 抄的

        private Hander.PlayerHandler playerHandler;
        public bool chance { get; set; } = true; // 火車頭的生成判定(一場生成一隻)

        public override void OnEnabled()
        {
            Log.Debug("OnEnabled started.");
            playerHandler = new Hander.PlayerHandler(this);


            //載入自訂身分，通常會自動載入但確認一下
            CustomRole.RegisterRoles(false,new die_act());


            Log.Debug("try to have hander.");
            PlayerEvents.UsingItem += playerHandler.OnUsingItem;
            PlayerEvents.TogglingNoClip += playerHandler.OnTogglingNoClip;
            ServerEvents.RoundStarted += playerHandler.OnRoundStarted;


            //載入自訂效果，通常會自動載入但確認一下
            CustomRole.RegisterRoles(false, new w_boy());
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            PlayerEvents.UsingItem -= playerHandler.OnUsingItem;
            PlayerEvents.TogglingNoClip -= playerHandler.OnTogglingNoClip;
            ServerEvents.RoundStarted -= playerHandler.OnRoundStarted;

            //卸載身分
            CustomRole.UnregisterRoles();
        }
    }
    
}
