namespace w.effect
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.CustomRoles.API.Features;
    using MEC;
    using PlayerStatsSystem;
    using UnityEngine;

    public class die_act : ActiveAbility
    {

        public override string Name { get; set; } = "die_effect";

        public override string Description { get; set; } = "you will die after 15 mins";

        public override float Duration { get; set; } = 10f;
        public override float Cooldown { get; set; } = 0f;

        protected override void AbilityUsed(Player player)
        {
            base.AbilityUsed(player);
        }

        protected override void AbilityEnded(Player player)
        {
            Log.Debug($"{Name} die by die_effect");
            player.Kill(DamageType.Unknown);
        }
    }
}
