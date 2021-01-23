using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Views
{
    class Renderer
    {
        private const int WindowWidth = 1280;
        private const int WindowHeight = 720;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;

        public Renderer(GraphicsDeviceManager graphics)
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);

            // Set window size & title
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
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
