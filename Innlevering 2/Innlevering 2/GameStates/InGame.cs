using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Innlevering_2.GameObjects;
using Innlevering_2.GUI;
using Microsoft.Xna.Framework.Input;

namespace Innlevering_2.GameStates
{
    public class InGame : GameState
    {
        Player player;


        SoundEffectInstance test; 
        SoundEffectInstance test2;


        InGameMenu pauseMenu;


        public InGame(Game game) : base(game) 
        {
            player = new Player(game, PlayerIndex.One, new Vector2(100f, 100f));
            test = ((ContentLoader<SoundEffect>)Game.Services.GetService(typeof(ContentLoader<SoundEffect>))).get("test").CreateInstance();
            test2 = ((ContentLoader<SoundEffect>)Game.Services.GetService(typeof(ContentLoader<SoundEffect>))).get("test2").CreateInstance();
            test.Play();
            test2.IsLooped = false;
            pauseMenu = new InGameMenu(Game);
        }

        public override void Update(GameTime gameTime)
        {
            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));
            if (controller.KeyWasPressed(Keys.Escape) ||
                    controller.ButtonWasPressed(Buttons.Start))
            {
                pauseMenu.Open = !pauseMenu.Open;
            }
            if (pauseMenu.Open)
            {
                pauseMenu.Update(gameTime);
                int pressed = pauseMenu.getPressed();
                if (pressed >= 0)
                {
                    if (pressed == 0)
                    {
                        ((Game1)Game).changeState(new Menu(Game));
                    }
                    else if (pressed == 1)
                    {
                        pauseMenu.Open = false;
                    }
                }
            }
            else
            {
                if (test.State != SoundState.Playing && test2.State != SoundState.Playing)
                {
                    test2.Play();
                }
                player.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            player.Draw(spriteBatch, gameTime);
            if (pauseMenu.Open)
            {
                pauseMenu.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
    }
}
