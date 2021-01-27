using Interstellar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    abstract class ShipController : EntityController
    {
        public ShipController(World w) : base(w)
        {
        }

        protected abstract void setRotation(Ship ship); // Only ships will have changing rotation
        protected abstract void handleAttack(Ship ship, float dt); // Only ships can attack

        // Keep ship inside world
        protected override bool boundsCheck(Entity entity)
        {
            // Left wall
            if (entity.Bounds.X < 0)
            {
                entity.Bounds.X = 0;
                entity.Position.X = entity.Bounds.X + entity.Bounds.Width / 2;
            }

            // Right wall
            else if (entity.Bounds.X + entity.Bounds.Width > World.Width)
            {
                entity.Bounds.X = World.Width - entity.Bounds.Width;
                entity.Position.X = entity.Bounds.X + entity.Bounds.Width / 2;
            }

            // Top wall
            if (entity.Bounds.Y < 0)
            {
                entity.Bounds.Y = 0;
                entity.Position.Y = entity.Bounds.Y + entity.Bounds.Height / 2;
            }

            // Bottom wall
            else if (entity.Bounds.Y + entity.Bounds.Height > World.Height)
            {
                entity.Bounds.Y = World.Height - entity.Bounds.Height;
                entity.Position.Y = entity.Bounds.Y + entity.Bounds.Height / 2;
            }

            return false; // Ships never escape bounds
        }
    }
}
