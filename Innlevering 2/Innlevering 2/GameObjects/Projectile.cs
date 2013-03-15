﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Innlevering_2.Graphics;

namespace Innlevering_2.GameObjects
{
    public class Projectile : GameObject
    {

        public Player Owner { get; protected set; }
        private Sprite texture;
        public Rectangle Collision
        {
            get
            {
                return new Rectangle(relativeCollision.X + (int)Position.X, relativeCollision.Y + (int)Position.Y, relativeCollision.Width, relativeCollision.Height);
            }
        }
        private Rectangle relativeCollision;
        public Vector2 Speed { get; protected set; }
        private float airResistance;
        private Vector2 gravity;
        public float Damage { get; protected set; }
        private float radius;
        private bool destructive;

        public bool Destroy { get; protected set; }

        public Projectile(Player owner, String textureName, Rectangle collision, Vector2 spawnPosition, Vector2 spawnSpeed, float airResistance, Vector2 gravity, float damage, float radius, bool destructive)
            : base(owner.Game)
        {
            Owner = owner;
            texture = new Sprite(Game, textureName);
            relativeCollision = collision;
            Position = spawnPosition;
            Speed = spawnSpeed;
            this.airResistance = airResistance;
            this.gravity = gravity;
            Damage = damage;
            this.radius = radius;
            this.destructive = destructive;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            texture.DrawCenter(spriteBatch, Position, gameTime);
        }

        public void Update(World world, List<Player> players, GameTime gameTime)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed -= Speed * airResistance * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // add more gravity stuff

            if (world.Level.Collide(Collision))
            {
                Explode(world, players);
            }
        }

        private void Explode(World world, List<Player> players)
        {
            if (world.Level is DestructableLevel && destructive)
            {
                ((DestructableLevel)world.Level).removeCircle(Position, radius);
            }
            foreach (Player p in players)
            {
                if (Vector2.Distance(p.Position, Position) <= radius)
                {
                    p.Damage(this);
                }
            }
            Destroy = true;
        }
    }
}