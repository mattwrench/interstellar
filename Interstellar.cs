using Interstellar.Controllers;
using Interstellar.Models;
using Interstellar.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Interstellar
{
    public class Interstellar : Game
    {
        public enum GameState
        {
            Ready, Playing, Paused, GameOver
        }

        private const float ReadyLength = 3.0f;

        private GameState gameState;
        private float timer;
        private GraphicsDeviceManager graphics;
        private World world;
        private ControllerSet controllers;
        private Renderer renderer;

        public Interstellar()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Frametime not limited to 16.66 Hz / 60 FPS
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "Interstellar Wars";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameState = GameState.Ready;
            timer = -ReadyLength; // Playing starts at 0
            world = new World();
            controllers = new ControllerSet(world);
            renderer = new Renderer(graphics, world, Content);

            // Load high score
            try
            {
                StreamReader reader = new StreamReader("highScore.txt");
                world.HighScore = int.Parse(reader.ReadLine());
                reader.Close();
            } catch (Exception e)
            {
                world.HighScore = 0;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            setGameState(dt);
            Input.Update(renderer.Cam);
            controllers.Update(dt, gameState);
            base.Update(gameTime);
        }

        private void setGameState(float dt)
        {
            // Update timers
            if (gameState == GameState.Ready || gameState == GameState.Playing)
                timer += dt;

            // Ready to Playing
            if (gameState == GameState.Ready && timer >= 0)
                gameState = GameState.Playing;

            // Reset game
            if (world.Player.Dead)
            {

                // Save high score
                if (world.Score > world.HighScore)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter("highScore.txt", false);
                        writer.WriteLine(world.Score);
                        writer.Close();
                    }
                    catch (Exception e)
                    {

                    }
                }

                LoadContent();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.Render(gameState);
            base.Draw(gameTime);
        }
    }
}
