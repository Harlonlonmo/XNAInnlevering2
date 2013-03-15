using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Innlevering_2.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Innlevering_2
{
    public class World
    {
        public Level Level { get; protected set; }
        public List<Player> Players { get; protected set; }

        public List<Projectile> Projectiles { get; protected set; }

        public Game Game { get; protected set;}

        private List<Projectile> toDestroy = new List<Projectile>();
        private List<Projectile> toAdd = new List<Projectile>();

        public World(Game game, Level level){
            Game = game;
            Level = level;
            Players = new List<Player>();
            Players.Add(new Player(Game, new Vector2(200, 200), new Rectangle(-10, -10, 20, 20), 50));
            Projectiles = new List<Projectile>();

        }

        public void Update(GameTime gameTime)
        {
            

            
            foreach (Projectile proj in Projectiles)
            {
                proj.Update(this, Players, gameTime);
            }
            foreach (Projectile proj in toDestroy)
            {
                Projectiles.Remove(proj);
            }
            toDestroy = new List<Projectile>();
            foreach (Projectile proj in toAdd)
            {
                Projectiles.Add(proj);
            }
            toAdd = new List<Projectile>();

            foreach (Player player in Players)
            {
                player.Update(this, gameTime);
            }
        }



        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Level.Draw(spriteBatch);
            spriteBatch.Begin();


            foreach (Player player in Players)
            {
                player.Draw(spriteBatch, gameTime);
            }

            foreach (Projectile proj in Projectiles)
            {
                proj.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }

        internal void AddProjectile(Projectile projectile)
        {
            toAdd.Add(projectile);
            Console.WriteLine(Projectiles);
        }

        internal void RemoveProjectile(Projectile projectile)
        {
            toDestroy.Add(projectile);
            Console.WriteLine(Projectiles);
        }
    }
}
