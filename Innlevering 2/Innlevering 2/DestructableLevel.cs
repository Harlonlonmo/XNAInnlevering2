﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace Innlevering_2
{
    public class DestructableLevel : Level
    {

        private Texture2D mask;
        private Texture2D texture;
        private Effect _AlphaShader;
        private Texture2D _circle;

        public DestructableLevel(Game game, Texture2D texture, Texture2D mask)
            : base(game)
        {
            this.texture = texture;
            this.mask = mask;
            _AlphaShader = Game.Content.Load<Effect>("AlphaMap");
            _circle = Game.Content.Load<Texture2D>("circle");
        }

        public override bool Collide(Rectangle rect)
        {
            if (rect.X < 0 || rect.X + rect.Width > mask.Width || rect.Y < 0 || rect.Y + rect.Height > mask.Height) return true;
            uint[] data = new uint[mask.Width * mask.Height];
            mask.GetData(data);
            for (int y = rect.Y; y < rect.Height + rect.Y; y++)
            {
                for (int x = rect.X; x < rect.Width + rect.X; x++)
                {
                    if (data[x + y* mask.Width] == 0xFF000000)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool Collide(Point point)
        {
            uint[] data = new uint[mask.Width * mask.Height];
            mask.GetData(data);
            if (data[point.X + point.Y * mask.Width] == 0)
            {
                return true;
            }
            return false;
        }

        public void removeCircle(Vector2 position, float radius)
        {
            RenderTarget2D target = new RenderTarget2D(Game.GraphicsDevice, mask.Width, mask.Height);
            Game.GraphicsDevice.SetRenderTarget(target);
            SpriteBatch spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.Draw(mask, Vector2.Zero, Color.White);
            spriteBatch.Draw(_circle, new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(radius * 2), (int)(radius * 2)), Color.White);
            spriteBatch.End();
            Game.GraphicsDevice.SetRenderTarget(null);
            mask = target;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // set the Mask to use for our shader.
            // note that "MaskTexture" corresponds to the public variable in AlphaMap.fx
            _AlphaShader.Parameters["MaskTexture"]
                .SetValue(mask);

            // start a spritebatch for our effect
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                null, null, null, _AlphaShader);

            spriteBatch.Draw(texture, Vector2.Zero, Color.White);

            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                Primitives2D.DrawCircle(spriteBatch, new Vector2(ms.X, ms.Y), 10, 20, Color.White);
            }
            spriteBatch.End();
        }
    }
}
