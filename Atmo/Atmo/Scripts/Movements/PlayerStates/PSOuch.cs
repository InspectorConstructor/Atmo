using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmo2.Movements.PlayerStates
{
    class PSOuch : PlayerState
    {
        private int duration;
        private int damage_taken;

        public PSOuch(Player player, int damage, int duration = 24)
			: base(player)
        {
            this.player = player;
            this.damage_taken = damage;
            this.duration = duration;
        }
        public override void OnEnter()
        {
            player.Animation = "fall";
            player.Spice -= damage_taken;
            if (player.Spice == 0) return;

            player.MovementInfo.VelY = -240;
            if (player._image.FlipH)
                player.MovementInfo.VelX += 480;
            else
                player.MovementInfo.VelX -= 480;

            player.IsInvincable = true;
            // this.player.Tweener.Tween(this.player, new { Alpha = 1 }, .9f)
            //     .From(new { Alpha = 0})
            //     .Ease((t) => Math.Abs(player.Alpha - 1))
            //     .OnComplete(() =>
            //     {
            //         this.player.Alpha = 1;
            //         this.player.IsInvincable = false;
            //     });
        }

        public override void OnExit()
        {
            
        }

        public override PlayerState Update()
        {
			player.MovementInfo.VelY += player.Gravity;

			--duration;

            if(this.duration < 0)
            {
				if (player.MovementInfo.OnGround)
					if (player.InputController.LeftHeld() || player.InputController.RightHeld())
						return new PSRun(player);
					else
						return new PSIdle(player);
				else
					return new PSFall(player);
			}
            return null;
        }
    }
}
