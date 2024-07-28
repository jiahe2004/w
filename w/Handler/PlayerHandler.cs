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
    internal class PlayerHandler
    {
        public float ContactDamage { get; set; } = 500f; //撞到人的時候給予的傷害
        private plugin plugin;
        public PlayerHandler(plugin plugin)
        {
            this.plugin = plugin;
        }

        private bool RunRaycast(Player player, out RaycastHit hit)
        {
            //向玩家前方射出判定路上有沒有牆壁或門或人

            Vector3 forward = player.CameraTransform.forward;
            return Physics.Raycast(player.Position + forward, forward, out hit, 200f, StandardHitregBase.HitregMask);
        }
        private void AdjustHeight(Player player)
        {

            //GPT寫的高度檢測(避免向天上衝刺)，可靠性未測試

            if (Physics.Raycast(player.Position + Vector3.up, Vector3.down, out RaycastHit groundHit, 2f, StandardHitregBase.HitregMask))
            {
                player.Position = new Vector3(player.Position.x, groundHit.point.y, player.Position.z);
            }
        }
        private IEnumerator<float> runPlayer(Player player, RaycastHit hit)
        {

            //衝刺

            while ((player.Position - hit.point).sqrMagnitude >= 2.5f)
            {
                player.Position = Vector3.MoveTowards(player.Position, hit.point, 0.5f); //座標移動(平滑處理)
//                AdjustHeight(player);//高度檢測
                yield return Timing.WaitForSeconds(0.00025f);//等待，在思考能不能刪除
            }

            //撞到的目標
            Player target = Player.Get(hit.collider.GetComponentInParent<ReferenceHub>());

            if (target != null)
            {

                //座標重疊時給予傷害
                if ((player.Position - target.Position).sqrMagnitude < 1.0f)
                    target.Hurt(ContactDamage);

            }

        }

        public void OnTogglingNoClip(TogglingNoClipEventArgs ev)
        {
            //玩家嘗試開啟NoClip時的檢測(Lalt)
            Log.Debug($"{ev.Player.DisplayNickname} is toggling noclip ");
            if (ev.Player.CustomInfo == "w_boy")//自訂身分檢查
            {
                Log.Debug("effect test");
                if(RunRaycast(ev.Player,out RaycastHit hit))
                {
                    Timing.RunCoroutine(runPlayer(ev.Player,hit));
                }

            }
        }

        public void OnUsingItem(UsingItemEventArgs ev)
        {
            //物品檢測
            Log.Debug("contect");

            //切換身分檢測
            if (ev.Item.Type == ItemType.SCP1853 && plugin.chance)
            {
                
                Vector3 room = ev.Player.Position;

                //給予身分
                CustomRole.Get(typeof(w_boy))?.AddRole(ev.Player);
                ev.Player.Teleport(room);
                plugin.chance = false;

                //修改玩家資訊
                ev.Player.CustomInfo = "w_boy";

                Log.Debug($"give {ev.Player.DisplayNickname} a rule");
            }
        }
        public void OnRoundStarted()
        {
            //初始化
            plugin.chance = true;
        }

    }
}
