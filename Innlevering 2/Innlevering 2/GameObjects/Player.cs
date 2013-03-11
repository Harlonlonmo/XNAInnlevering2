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
        public Point PlayerSize { get; private set; }
        //public Physics physics;
        public float speed = 2f;


        public Player(Game game, Vector2 PlayerPosition, Point PlayerSize, float speed):base(game)
        {
            Position = PlayerPosition;
            this.PlayerSize = PlayerSize;


        }

        public override void Update(GameTime gameTime)
        {
            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));

            //physics.Update(gameTime);

            //Movement
            if (controller.gamePadState.ThumbSticks.Left.X != 0f)
                Position += controller.gamePadState.ThumbSticks.Left * speed * Vector2.UnitX;
        }

        public override void Draw(SpriteBatch spriteBatch/*, GameTime gameTime*/)
        {
            Primitives2D.FillRectangle(spriteBatch, new Rectangle(
                (int) Position.X, (int) Position.Y, 
                PlayerSize.X, PlayerSize.Y), 
                Color.Brown);
        }

        /*protected bool Collide()
        {
            Rectangle playerRect = new Rectangle((int)Position.X, (int)Position.Y, PlayerSize.X, PlayerSize.Y);
            Rectangle mapFrameRect = new Rectangle(room.PosX, room.PosY, room.Width, room.Height);

            return (playerRect.Intersects(mapFrameRect));
        }*/
    }
}
