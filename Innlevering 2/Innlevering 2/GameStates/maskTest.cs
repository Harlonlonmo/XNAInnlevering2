using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace Innlevering_2.GameStates
{
    public class maskTest : GameState
    {

        Texture2D _Planet;
        Texture2D _AlphaMap;
        
        Level level;
        Player player;

        public maskTest(Game game)
            : base(game)
        {
            LoadContent(game.Content);
            level = new DestructableLevel(Game, _Planet, _AlphaMap);
            player = new Player(Game, new Vector2(200, 0), new Point(20,20), 5);
        }

        public void LoadContent(ContentManager Content)
        {
            _Planet = Content.Load<Texture2D>("RedPlanet512");
            _AlphaMap = Content.Load<Texture2D>("Dots");
        }

        bool collide = false;

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            //collide = level.Collide(player.Bounds);

            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                ((DestructableLevel)level).removeCircle(new Vector2(ms.X, ms.Y), 20);
            }
        }

        

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(collide)
                Game.GraphicsDevice.Clear(Color.Red);
            level.Draw(spriteBatch);
        }
    }
}
