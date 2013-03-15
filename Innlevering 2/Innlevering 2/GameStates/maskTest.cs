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
using Innlevering_2.Graphics;

namespace Innlevering_2.GameStates
{
    public class maskTest : GameState
    {

        Texture2D _Planet;
        Texture2D _AlphaMap;

        World world;

        public maskTest(Game game)
            : base(game)
        {
            LoadContent(Game.Content);
            world = new World(Game,new DestructableLevel(Game, _Planet, _AlphaMap));

        }

        public void LoadContent(ContentManager Content)
        {
            _Planet = Content.Load<Texture2D>("RedPlanet512");
            _AlphaMap = Content.Load<Texture2D>("Dots");
        }


        public override void Update(GameTime gameTime)
        {

            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                ((DestructableLevel)world.Level).removeCircle(new Vector2(ms.X, ms.Y), 20);

            }
            world.Update(gameTime);
        }



        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            //spriteBatch.Begin();
            //spriteBatch.Draw(backgroundtexture, Vector2.Zero, Color.White);
            //spriteBatch.End();
            world.Draw(spriteBatch, gameTime);
        }

        
    }
}
