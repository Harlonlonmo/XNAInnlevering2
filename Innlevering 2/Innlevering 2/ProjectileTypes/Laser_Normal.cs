using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Innlevering_2.Graphics;
using Innlevering_2.ProjectileTypes;
using Innlevering_2.GameObjects;

namespace Innlevering_2.ProjectileTypes
{
    class Laser_Normal : Projectile
    {

        public Laser_Normal(Player player, Vector2 spawnPosition, Vector2 spawnSpeed)
            : base("Laser", new Rectangle(-5, -5, 10, 10), 0, Vector2.UnitY * 50, 15, 5, true, player, spawnPosition, spawnSpeed)
        {
            
        }

        protected override void HandleCollision(World world)
        {
            Explode(world);
        }

    }
    }
}
