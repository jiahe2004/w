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
        public override Version Version => new Version(1,0,0);

        public Dictionary<StartTeam, List<ICustomRole>> Roles = new();

        public plugin instance;
        public Methods Methods { get; private set; } = null!;
        private playerhabder playerhabder;
        public bool chance { get; set; } = true;
        public die_act CamoAbility { get; private set; }

        public override void OnEnabled()
        {
            Log.Debug("OnEnabled started.");
            instance = this;
            Methods = new Methods(this);
            playerhabder = new playerhabder(this);

            CustomRole.RegisterRoles(false,new die_act());


            Log.Debug("try to have hander.");
            PlayerEvents.UsingItem += playerhabder.OnUsingItem;
            PlayerEvents.TogglingNoClip += playerhabder.OnTogglingNoClip;
            ServerEvents.RoundStarted += playerhabder.OnRoundStarted;

            CustomRole.RegisterRoles(false, new w_boy());
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            PlayerEvents.UsingItem -= playerhabder.OnUsingItem;
            PlayerEvents.TogglingNoClip -= playerhabder.OnTogglingNoClip;
            ServerEvents.RoundStarted -= playerhabder.OnRoundStarted;
        }
    }
    
}
