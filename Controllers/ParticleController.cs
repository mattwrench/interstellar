using Interstellar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    class ParticleController : EntityController
    {
        public ParticleController(World w) : base(w)
        {
        }

        public override void Update(float dt)
        {
            for (int i = world.Particles.Count - 1; i >= 0; i--)
            {
                Particle particle = world.Particles[i];
                particle.TimeToLive -= dt;

                setVelocity(particle, dt);
                setPosition(particle, dt);
                particle.SetBounds();
                collisionDetect(particle);
                if (boundsCheck(particle) || particle.TimeToLive <= 0)
                    world.Particles.RemoveAt(i);
            }
        }

        protected override bool boundsCheck(Entity entity)
        {
            if (entity.Bounds.X < 0 ||
                entity.Bounds.X + entity.Bounds.Width > World.Width ||
                entity.Bounds.Y < 0 ||
                entity.Bounds.Y + entity.Bounds.Height > World.Height)
                return true;

            return false;
        }

        protected override bool collisionDetect(Entity entity)
        {
            // No collisions w/ particles
            return false;
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            // Velocity remains constant
        }
    }
}
