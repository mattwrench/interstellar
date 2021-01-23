using Interstellar.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Views
{
    class Renderer
    {
        private const int ViewportWidth = 1280;
        private const int ViewportHeight = 720;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private World world;

        public Renderer(GraphicsDeviceManager graphics, World world)
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.world = world;

            // Set window size & title
            graphics.PreferredBackBufferWidth = ViewportWidth;
            graphics.PreferredBackBufferHeight = ViewportHeight;
            graphics.ApplyChanges();
        }

        public void Render()
        {
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.End();
        }
    }
}
