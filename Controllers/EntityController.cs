using Interstellar.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    // Base class for all controllers
    abstract class EntityController
    {
        protected World world;

        public EntityController(World w)
        {
            world = w;
        }
        public abstract void Update(float dt);
        protected abstract void setVelocity(Entity entity, float dt);
        protected abstract bool boundsCheck(Entity entity); // Return true if entity out of bounds
        protected abstract bool collisionDetect(Entity entity); // Return true if entity should be deleted

        // Add velocity * dt to position
        protected void setPosition(Entity entity, float dt)
        {
            entity.Position = Vector2.Add(entity.Position, Vector2.Multiply(entity.Velocity, dt));
        }
    }
}
