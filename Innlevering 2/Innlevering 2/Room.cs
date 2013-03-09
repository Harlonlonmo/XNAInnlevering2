using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace Innlevering_2
{
    class Room
    {
        public int PosX;
        public int PosY;
        public int Width;
        public int Height;
        private int roomEdgeOffset = 5;

        public Room(int roomPosX, int roomPosY, int roomWidth, int roomHeight)
        {
            this.PosX = roomPosX;
            this.PosY = roomPosY;
            this.Width = roomWidth;
            this.Height = roomHeight;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawRoomEdges(spriteBatch);
        }

        public void DrawRoomEdges(SpriteBatch spriteBatch)
        {
            Primitives2D.DrawRectangle(spriteBatch, new Rectangle(roomEdgeOffset, roomEdgeOffset, Width, Height), Color.Black, 2f);
        }
    }
}
