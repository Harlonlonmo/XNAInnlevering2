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
        Effect _AlphaShader;

        Texture2D _circle;

        public maskTest(Game game)
            : base(game)
        {
            LoadContent(game.Content);
        }

        public void LoadContent(ContentManager Content)
        {
            _Planet = Content.Load<Texture2D>("RedPlanet512");
            _AlphaMap = Content.Load<Texture2D>("Dots");
            _AlphaShader = Content.Load<Effect>("AlphaMap");
            _circle = Content.Load<Texture2D>("circle");
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                removeCircle(new Vector2(ms.X, ms.Y), 20);
            }
        }

        public void removeCircle(Vector2 position, float radius)
        {
            RenderTarget2D target = new RenderTarget2D(Game.GraphicsDevice, _AlphaMap.Width, _AlphaMap.Height);
            Game.GraphicsDevice.SetRenderTarget(target);
            SpriteBatch spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.Draw(_AlphaMap, Vector2.Zero, Color.White);
            spriteBatch.Draw(_circle, new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(radius*2), (int)(radius*2)), Color.White);
            spriteBatch.End();
            Game.GraphicsDevice.SetRenderTarget(null);
            _AlphaMap = target;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // set the Mask to use for our shader.
            // note that "MaskTexture" corresponds to the public variable in AlphaMap.fx
            _AlphaShader.Parameters["MaskTexture"]
                .SetValue(_AlphaMap);

            // start a spritebatch for our effect
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                null, null, null, _AlphaShader);

            spriteBatch.Draw(_Planet, Vector2.Zero, Color.White);

            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                Primitives2D.DrawCircle(spriteBatch, new Vector2(ms.X, ms.Y), 10, 20, Color.White);
            }
            spriteBatch.End();
        }
    }
}
