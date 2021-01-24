using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    // Base class for all sprites
    abstract class Entity
    {
        public Vector2 Position; // X/Y is at center of sprite
        public Vector2 Velocity;
        public float TopSpeed;
        public Rectangle Bounds; // X/Y is at top-left of sprite
        public float Rotation; // In degrees; 0 is pointing right

        public Entity()
        {
            // Default values will be overwritten in child constructors
            Position = new Vector2();
            Velocity = new Vector2();
            Bounds = new Rectangle();
            Rotation = 270; // Default to facing upwards
        }

        // Update Bounds.X/Y from Position.X/Y
        public void SetBounds()
        {
            Bounds.X = (int)(Position.X - Bounds.Width / 2);
            Bounds.Y = (int)(Position.Y - Bounds.Height / 2);
        }
    }
}
