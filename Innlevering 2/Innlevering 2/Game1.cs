using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Innlevering_2.GameStates;

namespace Innlevering_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ContentLoader<SpriteFont> fontLoader;
        ContentLoader<Texture2D> imageLoader;
        ContentLoader<SoundEffectInstance> soundLoader;

        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            InputController input = new InputController(this);
            Components.Add(input);
            Services.AddService(typeof(InputController), input);

            fontLoader = new ContentLoader<SpriteFont>(this);
            Services.AddService(typeof(ContentLoader<SpriteFont>), fontLoader);

            imageLoader = new ContentLoader<Texture2D>(this);
            Services.AddService(typeof(ContentLoader<Texture2D>), imageLoader);

            soundLoader = new ContentLoader<SoundEffectInstance>(this);
            Services.AddService(typeof(ContentLoader<SoundEffectInstance>), soundLoader);
        }

        protected override void Initialize()
        {
            base.Initialize();
            gameState = new Menu(this);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            imageLoader.load("Background");
            fontLoader.load("ButtonFont");
        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            gameState.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void changeState(GameState newState)
        {
            gameState = newState;
        }
    
    }
}
