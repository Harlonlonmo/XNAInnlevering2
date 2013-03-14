using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2
{
    public class ImageLoader
    {
        public Game Game { get; protected set; }

        private Dictionary<String, Texture2D> list;

        public ImageLoader(Game game)
        {
            Game = game;
            list = new Dictionary<string,Texture2D>();
        }

        public Texture2D getTexture(String name)
        {
            if (list.Keys.Contains(name))
            {
                return list[name];
            }
            else
            {
                list.Add(name, Game.Content.Load<Texture2D>(name));
                return list[name];
            }
        }
    }
}
