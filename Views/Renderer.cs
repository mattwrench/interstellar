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
        private const float BloomStrength = 1.2f;
        private const int BorderThickness = 1;
        private const int CameraBoundsRange = 150;
        private const int LineSpacingReduction = 10;

        public Camera2D Cam;

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private World world;
        private TextureSet textures;
        private BloomFilter bloomFilter;
        private Texture2D whiteRect; // Used for rendering rectangles
        private RenderTarget2D renderTarget;
        private SpriteFont font;

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

            Cam = new Camera2D(ViewportWidth, ViewportHeight, world.Player.Position);
            renderTarget = new RenderTarget2D(graphicsDevice, ViewportWidth, ViewportHeight);
            font = content.Load<SpriteFont>("Fonts/Consolas");
        }

        public void Render(Interstellar.GameState gameState)
        {
            updateCamera();

            graphicsDevice.Clear(Color.Black);

            // Draw scene to render target
            graphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend, transformMatrix: Cam.TransformMatrix);

            drawBorder();

            foreach (Particle particle in world.Particles)
                drawParticle(particle);

            if (!world.Player.Dead)
                drawShip(world.Player);

            foreach (Ship enemy in world.Enemies)
                drawShip(enemy);

            foreach (Bullet bullet in world.Bullets)
                drawBullet(bullet);

            spriteBatch.End();

            // Generate bloom and draw to screen
            Texture2D bloom = bloomFilter.Draw(renderTarget, ViewportWidth, ViewportHeight);
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, ViewportWidth, ViewportHeight), Color.White);
            spriteBatch.Draw(bloom, new Rectangle(0, 0, ViewportWidth, ViewportHeight), Color.White);

            drawText(gameState);

            spriteBatch.End();
        }

        private void drawText(Interstellar.GameState gameState)
        {
            // Score
            spriteBatch.DrawString(font, "SCORE", new Vector2(), Color.White);
            spriteBatch.DrawString(font, world.Score.ToString(), new Vector2(0, font.LineSpacing - LineSpacingReduction), Color.White);

            // High score
            Vector2 size = font.MeasureString("HIGH SCORE");
            spriteBatch.DrawString(font, "HIGH SCORE", new Vector2(ViewportWidth - size.X, 0), Color.White);
            size = font.MeasureString(world.HighScore.ToString());
            spriteBatch.DrawString(font, world.HighScore.ToString(), new Vector2(ViewportWidth - size.X, font.LineSpacing - LineSpacingReduction), Color.White);

            if (gameState == Interstellar.GameState.Ready)
                drawStringAtCenter("READY");
            else if (gameState == Interstellar.GameState.Paused)
                drawStringAtCenter("PAUSED");
            else if (gameState == Interstellar.GameState.GameOver)
                drawStringAtCenter("GAME OVER");
        }

        // Draws in middle of screen horizontally, three-quarters up vertical (for easier reading)
        private void drawStringAtCenter(String str)
        {
            Vector2 size = font.MeasureString(str);
            spriteBatch.DrawString(font, 
                str, 
                new Vector2(ViewportWidth / 2 - size.X / 2, ViewportHeight / 4 - size.Y / 2), 
                Color.White);
        }

        private void drawBullet(Bullet bullet)
        {
            Texture2D texture = bullet.IsShotByPlayer ? textures.BulletWhite : textures.BulletGreen;
            drawEntity(bullet, texture, Color.White);
        }

        // Camera follows player
        private void updateCamera()
        {
            Cam.Location.X = world.Player.Position.X;
            Cam.Location.Y = world.Player.Position.Y;

            // Bounds check camera
            if (Cam.Location.X - Cam.ViewportWidth / 2 < -CameraBoundsRange)
                Cam.Location.X = Cam.ViewportWidth / 2 - CameraBoundsRange;
            else if (Cam.Location.X + Cam.ViewportWidth / 2 > World.Width + CameraBoundsRange)
                Cam.Location.X = World.Width + CameraBoundsRange - Cam.ViewportWidth / 2;
            if (Cam.Location.Y - Cam.ViewportHeight / 2 < -CameraBoundsRange)
                Cam.Location.Y = Cam.ViewportHeight / 2 - CameraBoundsRange;
            else if (Cam.Location.Y + Cam.ViewportHeight / 2 > World.Height + CameraBoundsRange)
                Cam.Location.Y = World.Height + CameraBoundsRange - Cam.ViewportHeight / 2;
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

        private void drawEntity(Entity entity, Texture2D texture, Color color)
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
                color, 
                MathHelper.ToRadians(entity.Rotation), 
                new Vector2(entity.Bounds.Width / 2, entity.Bounds.Height / 2), 
                SpriteEffects.None, 
                0);
        }

        private void drawShip(Ship ship)
        {
            Color color;
            if (ship.ShipType == Ship.Type.Player)
            {
                color = Color.White;
            }
            else
            {
                color = Color.White * ((ship.FadeInTimer) / (Ship.FadeInLength));
            }

            // Stretch Runner instead of rotating
            if (ship.ShipType == Ship.Type.Runner)
                spriteBatch.Draw(textures.Ships[Ship.Type.Runner], ship.Bounds, Color.White);

            else
                drawEntity(ship, textures.Ships[ship.ShipType], color);
        }

        private void drawRect(Rectangle rect, Color color)
        {
            spriteBatch.Draw(whiteRect, rect, color);
        }

        private void drawParticle(Particle particle)
        {
            Color color = Color.White * (particle.TimeToLive / Particle.LifeLength);
            drawEntity(particle, textures.Particles[particle.ShipColor], color);
        }
    }
}
