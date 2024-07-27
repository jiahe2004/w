using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;
using PlayerStatsSystem;


namespace w.Hander
{
    internal class playerhabder
    {
        public float ContactDamage { get; set; } = 500f;
        private plugin plugin;
        public playerhabder(plugin plugin)
        {
            this.plugin = plugin;
        }
        private bool RunRaycast(Player player, out RaycastHit hit)
        {
            Vector3 forward = player.CameraTransform.forward;
            return Physics.Raycast(player.Position + forward, forward, out hit, 200f, StandardHitregBase.HitregMask);
        }
        private void AdjustHeight(Player player)
        {
            if (Physics.Raycast(player.Position + Vector3.up, Vector3.down, out RaycastHit groundHit, 2f, StandardHitregBase.HitregMask))
            {
                player.Position = new Vector3(player.Position.x, groundHit.point.y, player.Position.z);
            }
        }
        private IEnumerator<float> runPlayer(Player player, RaycastHit hit)
        {

            while ((player.Position - hit.point).sqrMagnitude >= 2.5f)
            {
                player.Position = Vector3.MoveTowards(player.Position, hit.point, 0.5f);
                AdjustHeight(player);
                yield return Timing.WaitForSeconds(0.00025f);
            }

            Player target = Player.Get(hit.collider.GetComponentInParent<ReferenceHub>());

            if (target != null)
            {
                if ((player.Position - target.Position).sqrMagnitude < 1.0f)
                    target.Hurt(ContactDamage);

            }

        }


        public void OnTogglingNoClip(TogglingNoClipEventArgs ev)
        {
            Log.Debug($"{ev.Player.DisplayNickname} is toggling noclip ");
            if (ev.Player.CustomInfo == "w_boy")
            {
                Log.Debug("effect test");
                if(RunRaycast(ev.Player,out RunRaycast hit))
                {
                    Timing.RunCoroutine(runPlayer(ev.Player,hit));
                }

            }
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            Log.Debug("contect");
            if (ev.Item.Type == ItemType.SCP1853 && plugin.chance)
            {
                Vector3 room = ev.Player.Position;

                CustomRole.Get(typeof(w_boy))?.AddRole(ev.Player);
                ev.Player.Teleport(room);
                plugin.chance = false;

                ev.Player.CustomInfo = "w_boy";

                Log.Debug($"give {ev.Player.DisplayNickname} a rule");
            }
        }
        public void OnRoundStarted()
        {
            plugin.chance = true;
        }

    }
}
