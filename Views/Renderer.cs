﻿using Interstellar.Models;
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
        private const float BloomStrength = 1.1f;
        private const int BorderThickness = 1;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private World world;
        private TextureSet textures;
        private BloomFilter bloomFilter;
        private Texture2D whiteRect; // Used for rendering rectangles
        private Camera2D cam;

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
            bloomFilter.BloomPreset = BloomFilter.BloomPresets.Small;
            bloomFilter.BloomThreshold = BloomThreshold;
            bloomFilter.BloomStrengthMultiplier = BloomStrength;

            whiteRect = new Texture2D(graphicsDevice, 1, 1);
            whiteRect.SetData(new[] { Color.White });

            cam = new Camera2D(ViewportWidth, ViewportHeight, world.Player.Position);
        }

        public void Render()
        {
            updateCamera();

            graphicsDevice.Clear(Color.Black);

            // Draw scene  render target
            RenderTarget2D renderTarget = new RenderTarget2D(graphicsDevice, ViewportWidth, ViewportHeight);
            graphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin(transformMatrix: cam.TransformMatrix);

            drawBorder();

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

        // Camera follows player
        private void updateCamera()
        {
            cam.Location.X = world.Player.Position.X;
            cam.Location.Y = world.Player.Position.Y;
        }

        // Draw border around world
        private void drawBorder()
        {
            // Left wall
            Rectangle left = new Rectangle(0, 0, BorderThickness, World.Height);
            drawRect(left, Color.White);

            // Right wall
            Rectangle right = new Rectangle(World.Width - BorderThickness, 0, BorderThickness, World.Height);
            drawRect(right, Color.White);

            // Top wall
            Rectangle top = new Rectangle(0, 0, World.Width, BorderThickness);
            drawRect(top, Color.White);

            // Bottom wall
            Rectangle bottom = new Rectangle(0, World.Height - BorderThickness, World.Width, BorderThickness);
            drawRect(bottom, Color.White);
        }

        private void drawEntity(Entity entity, Texture2D texture, float rotation)
        {
            // Destination must be adjusted due to rotation
            Rectangle dest = new Rectangle(
                entity.Bounds.X + entity.Bounds.Width / 2,
                entity.Bounds.Y + entity.Bounds.Height / 2,
                entity.Bounds.Width,
                entity.Bounds.Height);
            spriteBatch.Draw(
                texture,
                dest,
                null, 
                Color.White, 
                MathHelper.ToRadians(rotation), 
                new Vector2(entity.Bounds.Width / 2, entity.Bounds.Height / 2), 
                SpriteEffects.None, 
                0);
        }

        private void drawShip(Ship ship)
        {
            drawEntity(ship, textures.Ships[ship.ShipType], ship.Rotation);
        }

        private void drawRect(Rectangle rect, Color color)
        {
            spriteBatch.Draw(whiteRect, rect, color);
        }
    }
}
