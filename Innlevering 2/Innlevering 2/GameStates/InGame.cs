using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Innlevering_2.GameStates
{
    public class InGame : GameState
    {
        Player player;
        Room room;


        SoundEffectInstance test; 
        SoundEffectInstance test2;

        public InGame(Game game) : base(game) 
        {
            room = new Room(0, 0, 790, 470);
            player = new Player(game, new Vector2(100f, 100f), new Point(30, 30), 2f);
            test = ((ContentLoader<SoundEffect>)Game.Services.GetService(typeof(ContentLoader<SoundEffect>))).get("test").CreateInstance();
            test2 = ((ContentLoader<SoundEffect>)Game.Services.GetService(typeof(ContentLoader<SoundEffect>))).get("test").CreateInstance();
            test.Play();
            test2.IsLooped = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (test.State != SoundState.Playing && test2.State != SoundState.Playing)
            {
                test2.Play();
            }
            player.Update(gameTime, room);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            room.Draw(spriteBatch,gameTime);
            player.Draw(spriteBatch);
        }
    }
}
