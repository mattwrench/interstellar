using Interstellar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    class BulletController : EntityController
    {
        public BulletController(World w) : base(w)
        {
        }

        public override void Update(float dt)
        {
            foreach (Bullet bullet in world.Bullets)
            {
                setPosition(bullet, dt);
                bullet.SetBounds();
                collisionDetect(bullet);
                boundsCheck(bullet);
            }
        }

        // Eliminate out of bound bullets
        protected override bool boundsCheck(Entity entity)
        {
            // TODO
            return false;
        }

        // Handles collisions between bullets and ships
        protected override bool collisionDetect(Entity entity)
        {
            // TODO
            return false;
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            // Bullet velocity does not change
        }
    }
}
