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
using Innlevering_2.Graphics;
using Innlevering_2.Guns;

namespace Innlevering_2.GameObjects
{
    public class Player : GameObject
    {
        //Kenneth: testing av keyboard siden jeg ikke har xbox-kontroller med ledning
        public KeyboardState KeyboardState;

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

        private Sprite reticule;
        private float reticuleAngle;

        public Gun Wepon { get; protected set; }

        public Player(Game game, Vector2 PlayerPosition, Rectangle relativeBounds, float speed)
            : base(game)
        {
            Position = PlayerPosition;
            this.relativeBounds = relativeBounds;
            Speed = speed;
            reticule = new Sprite(Game, "reticule");
            Wepon = new HandGun(Game);
        }

        public void Update(World world, GameTime gameTime)
        {

            Wepon.Update(gameTime);

            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));
            KeyboardState = Keyboard.GetState();

            //if (controller.ButtonWasPressed(Buttons.RightTrigger))
            if (controller.gamePadState.IsButtonDown(Buttons.RightTrigger) || KeyboardState.IsKeyDown(Keys.E))
            {
                Wepon.Fire(world, this, gameTime);
                //((DestructableLevel)level).removeCircle(getReticulePosition(), 20);
            }
            if (controller.ButtonWasPressed(Buttons.X) || KeyboardState.IsKeyDown(Keys.R))
            {
                Wepon.Reload();
                //((DestructableLevel)level).removeCircle(getReticulePosition(), 20);
            }
            //reticule position

            if (controller.gamePadState.ThumbSticks.Right.LengthSquared() > .75f)
            {
                reticuleAngle = (float)Math.Atan2(controller.gamePadState.ThumbSticks.Right.Y, controller.gamePadState.ThumbSticks.Right.X);

            }


            Vector2 move = Vector2.Zero;

            //Movement
            if (Math.Abs(controller.gamePadState.ThumbSticks.Left.X) >= 0.2f)
                move += controller.gamePadState.ThumbSticks.Left * Vector2.UnitX;
            if (Math.Abs(controller.gamePadState.ThumbSticks.Left.Y) >= 0.2f)
                move -= controller.gamePadState.ThumbSticks.Left * Vector2.UnitY;
            if (controller.keyboardState.IsKeyDown(Keys.W))
                move.Y = -1;
            if (controller.keyboardState.IsKeyDown(Keys.S))
                move.Y = 1;
            if (controller.keyboardState.IsKeyDown(Keys.A))
                move.X = -1;
            if (controller.keyboardState.IsKeyDown(Keys.D))
                move.X = 1;



            if (Grounded)
            {
                if (controller.KeyWasPressed(Keys.Space) || controller.ButtonWasPressed(Buttons.A))
                {
                    jump();
                }
                TryWalk(move * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, world.Level);
            }
            else
            {
                TryMove(move * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, world.Level);
                Fall(gameTime, world.Level);
            }
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


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //debug
            Primitives2D.DrawRectangle(spriteBatch, Bounds,
                Color.Brown);

            reticule.DrawCenter(spriteBatch, getReticulePosition(), gameTime);
        }

        public Vector2 getReticulePosition()
        {
            return new Vector2((float)Math.Cos(reticuleAngle), -(float)Math.Sin(reticuleAngle)) * 40 + Position;
        }

        public void Damage(Projectile projectile)
        {
            // TODO take damage
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
