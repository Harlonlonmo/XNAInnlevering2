using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Innlevering_2.Graphics
{
    public class Sprite : IDrawable
    {
        protected Texture2D texture;

        public Sprite(Game game, String textureName)
        {
            texture = ((ContentLoader<Texture2D>)game.Services.GetService(typeof(ContentLoader<Texture2D>))).get(textureName);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void DrawCenter(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position - new Vector2(texture.Width/2, texture.Height/2), Color.White);
        }
    }
}
