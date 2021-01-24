using Interstellar.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private TextureSet textures;

        public Renderer(GraphicsDeviceManager graphics, World world, ContentManager content)
        {
            graphicsDevice = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.world = world;
            textures = new TextureSet(content);

            // Set window size & title
            graphics.PreferredBackBufferWidth = ViewportWidth;
            graphics.PreferredBackBufferHeight = ViewportHeight;
            graphics.ApplyChanges();
        }

        public void Render()
        {
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            drawShip(world.Player);

            spriteBatch.End();
        }

        private void drawEntity(Entity entity, Texture2D texture)
        {
            spriteBatch.Draw(texture, entity.Bounds, Color.White);
        }

        private void drawShip(Ship ship)
        {
            drawEntity(ship, textures.Ships[ship.ShipType]);
        }
    }
}
