using Interstellar.Models;
using Interstellar.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    // Base class for all controllers
    abstract class EntityController
    {
        protected int ParticlesToSpawn = 8;

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

        protected void spawnParticles(Ship ship)
        {
            Vector2 dir = new Vector2(1, 0);
            Vector2 pos = new Vector2(ship.Position.X, ship.Position.Y);
            for (int i = 0; i < ParticlesToSpawn; i++)
            {
                dir = dir.Rotate(Utils.DegreesInCircle / ParticlesToSpawn);
                world.Particles.Add(new Particle(pos, dir, ship.ShipType));
            }
        }
    }
}
