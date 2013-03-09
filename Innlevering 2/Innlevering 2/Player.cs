﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using C3.XNA;

namespace Innlevering_2
{
    class Player
    {
        private Game game;
        public Vector2 PlayerPosition { get; set; }
        public Point PlayerSize { get; private set; }
        //public Physics physics;
        public float speed = 2f;
        private Room room;


        public Player(Game game, Vector2 PlayerPosition, Point PlayerSize, float speed)
        {
            this.PlayerPosition = PlayerPosition;
            this.PlayerSize = PlayerSize;
            this.game = game;

        }

        public void Update(GameTime gameTime, Room room)
        {
            InputController controller = (InputController)game.Services.GetService(typeof(InputController));
            this.room = room;

            //physics.Update(gameTime);

            //Movement
            if (controller.gamePadState.ThumbSticks.Left.X != 0f)
                PlayerPosition += new Vector2(controller.gamePadState.ThumbSticks.Left.X * speed, 0);
        }

        public void Draw(SpriteBatch spriteBatch/*, GameTime gameTime*/)
        {
            Primitives2D.FillRectangle(spriteBatch, new Rectangle(
                (int) PlayerPosition.X, (int) PlayerPosition.Y, 
                PlayerSize.X, PlayerSize.Y), 
                Color.Brown);
        }

        protected bool Collide()
        {
            Rectangle playerRect = new Rectangle((int)PlayerPosition.X, (int)PlayerPosition.Y, PlayerSize.X, PlayerSize.Y);
            Rectangle mapFrameRect = new Rectangle(room.PosX, room.PosY, room.Width, room.Height);

            return (playerRect.Intersects(mapFrameRect));
        }
    }
}