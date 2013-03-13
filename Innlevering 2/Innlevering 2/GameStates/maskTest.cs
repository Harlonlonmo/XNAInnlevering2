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
            player = new Player(Game, new Vector2(200, 0), new Rectangle(0,0,20,20), 50);
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
                ((DestructableLevel)level).removeCircle(new Vector2(ms.X, ms.Y), 20);

            }

            InputController controller = (InputController)Game.Services.GetService(typeof(InputController));

            Vector2 move = Vector2.Zero;

            //Movement
            if (controller.gamePadState.ThumbSticks.Left.X != 0f)
                move += controller.gamePadState.ThumbSticks.Left * Vector2.UnitX;
            if (controller.gamePadState.ThumbSticks.Left.Y != 0f)
                move -= controller.gamePadState.ThumbSticks.Left * Vector2.UnitY;
            if (controller.keyboardState.IsKeyDown(Keys.W))
                move.Y = -1;
            if (controller.keyboardState.IsKeyDown(Keys.S))
                move.Y = 1;
            if (controller.keyboardState.IsKeyDown(Keys.A))
                move.X = -1;
            if (controller.keyboardState.IsKeyDown(Keys.D))
                move.X = 1;



            if (player.Grounded)
            {
                if (controller.KeyWasPressed(Keys.Space) || controller.ButtonWasPressed(Buttons.A))
                {
                    player.jump();
                }
                player.TryWalk(move * player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, level);
            }
            else
            {
                player.TryMove(move * player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, level);
                player.Fall(gameTime, level);
            }
            

        }

        

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            level.Draw(spriteBatch);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
