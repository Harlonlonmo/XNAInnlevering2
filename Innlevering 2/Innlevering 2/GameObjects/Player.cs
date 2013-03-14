using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using C3.XNA;

namespace Innlevering_2.GameObjects
{
    class Player : GameObject
    {
        private Rectangle relativeBounds;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)(relativeBounds.X + Position.X), (int)(relativeBounds.Y + Position.Y), relativeBounds.Width, relativeBounds.Height);
            }
        }
        //public Physics physics;
        public float Speed { get; protected set; }
        public float FallingSpeed { get; protected set; }

        public bool Grounded { get; protected set; }

        private int walkSlope = 5;

        public Player(Game game, Vector2 PlayerPosition, Rectangle relativeBounds, float speed)
            : base(game)
        {
            Position = PlayerPosition;
            this.relativeBounds = relativeBounds;
            Speed = speed;

        }

        public override void Update(GameTime gameTime)
        {

        }

        public bool TryWalk(Vector2 rel, ICollidable collision)
        {
            int tries = -walkSlope;
            while (collision.Collide(new Rectangle(Bounds.X + (int)Math.Round(rel.X * (walkSlope - tries) / walkSlope), Bounds.Y + (int)Math.Round(rel.Y) - tries, Bounds.Width, Bounds.Height)) &&
                tries < walkSlope)
            {
                tries++;
            }
            if (tries == walkSlope)
            {
                return false;
            }
            if (tries == -walkSlope)
            {
                Grounded = false;
                move(rel);
                return true;
            }

            move(rel * new Vector2((walkSlope - tries) / walkSlope, 1) - Vector2.UnitY * tries);
            return true;
        }
        public bool TryMove(Vector2 rel, ICollidable collision)
        {
            if (collision.Collide(new Rectangle(Bounds.X + (int)Math.Round(rel.X), Bounds.Y + (int)Math.Round(rel.Y), Bounds.Width, Bounds.Height)))
            {
                return false;
            }

            move(rel);
            return true;
        }

        public void Fall(GameTime gameTime, ICollidable collision)
        {
            FallingSpeed += 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Grounded = !TryMove(Vector2.UnitY * (float)gameTime.ElapsedGameTime.TotalSeconds * FallingSpeed, collision);
            if (Grounded)
            {
                FallingSpeed = 0;
                TryWalk(Vector2.Zero, collision) ;
            }
        }


        public override void Draw(SpriteBatch spriteBatch/*, GameTime gameTime*/)
        {

            //debug
            Primitives2D.DrawRectangle(spriteBatch, Bounds,
                Color.Brown);
        }



        /*protected bool Collide()
        {
            Rectangle playerRect = new Rectangle((int)Position.X, (int)Position.Y, PlayerSize.X, PlayerSize.Y);
            Rectangle mapFrameRect = new Rectangle(room.PosX, room.PosY, room.Width, room.Height);

            return (playerRect.Intersects(mapFrameRect));
        }*/

        internal void jump()
        {
            Grounded = false;
            FallingSpeed = -100;
        }
    }
}
