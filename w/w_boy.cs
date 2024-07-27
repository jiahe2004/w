using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w
{
    using Exiled.API.Enums;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomRoles.API.Features;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.Handlers;
    using MEC;
    using PlayerRoles;
    using System.Collections.Generic;
    using w;
    using w.API;
    using w.effect;

    // 火車頭自訂角色登入
    [CustomRole(RoleTypeId.Tutorial)]
    public class w_boy : CustomRole, ICustomRole
    {
        public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
        public override float SpawnChance { get; set; } = 0;

        public override int MaxHealth { get; set; } = 500;
        public override string Name { get; set; } = "火車頭";
        public override string Description { get; set; } =
            "你是一個 中立 角色，按下ALT可以衝刺撞人造成500點傷害，但你只有15分鐘生存時間";
        public override string CustomInfo { get; set; } = "w_boy";


        public override uint Id { get; set; } = 3;

        public int Chance { get; set; } = 100;
        public StartTeam StartTeam { get; set; } = StartTeam.Other;


        protected override void RoleAdded(Exiled.API.Features.Player player)
        {
            byte x = 255;
            Timing.CallDelayed(2.5f, () =>
            {
                player.EnableEffect(EffectType.MovementBoost, x, 0f, true);

            });
        }

        public override List<CustomAbility>? CustomAbilities { get; set; } = new()
        {
            //火車頭的出生自帶自訂效果
            new die_act()
        };

    }
}
