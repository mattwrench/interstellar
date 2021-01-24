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
        private const float BloomThreshold = 0.1f;
        private const float BloomStrength = 1.4f;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private World world;
        private TextureSet textures;
        private BloomFilter bloomFilter;

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

            bloomFilter = new BloomFilter();
            bloomFilter.Load(graphicsDevice, content, ViewportWidth, ViewportHeight);
            bloomFilter.BloomPreset = BloomFilter.BloomPresets.Focussed;
            bloomFilter.BloomThreshold = BloomThreshold;
            bloomFilter.BloomStrengthMultiplier = BloomStrength;
        }

        public void Render()
        {
            graphicsDevice.Clear(Color.Black);

            // Draw scene  render target
            RenderTarget2D renderTarget = new RenderTarget2D(graphicsDevice, ViewportWidth, ViewportHeight);
            graphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin();

            drawShip(world.Player);

            spriteBatch.End();

            // Generate bloom and draw to screen
            Texture2D bloom = bloomFilter.Draw(renderTarget, ViewportWidth, ViewportHeight);
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, ViewportWidth, ViewportHeight), Color.White);
            spriteBatch.Draw(bloom, new Rectangle(0, 0, ViewportWidth, ViewportHeight), Color.White);
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
