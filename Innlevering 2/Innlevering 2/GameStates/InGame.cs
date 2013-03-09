using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2.GameStates
{
    public class InGame : GameState
    {
        Player player;
        Room room;

        public InGame(Game game) : base(game) 
        {
            room = new Room(0, 0, 790, 470);
            player = new Player(game, new Vector2(100f, 100f), new Point(30, 30), 2f);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, room);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            room.Draw(spriteBatch,gameTime);
            player.Draw(spriteBatch);
        }
    }
}
