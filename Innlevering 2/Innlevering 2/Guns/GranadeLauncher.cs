using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;

namespace Innlevering_2.Guns
{
    public class GranadeLauncher: Gun
    {

        public GranadeLauncher(Game game):base(game, "granadeLauncher",2,5,5)
        {
        }

        public override void Fire(World world, Player player, GameTime gameTime)
        {
            if (CooldownTimer <= 0 && MagazineCount > 0)
            {
                world.Projectiles.Add(new Projectile(player, "RPG", new Rectangle(-5, -5, 10, 10), player.Position, Vector2.Normalize(player.getReticulePosition() - player.Position) * 200, 0, Vector2.UnitY * 150, 30, 30, true));
                CooldownTimer = Cooldown;
                MagazineCount--;
            }

        }
    }
}
