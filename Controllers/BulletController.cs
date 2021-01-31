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
            for (int i = world.Bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = world.Bullets[i];
                setPosition(bullet, dt);
                bullet.SetBounds();
                collisionDetect(bullet);
                if (boundsCheck(bullet))
                    world.Bullets.RemoveAt(i);
            }
        }

        // Eliminate out of bound bullets
        protected override bool boundsCheck(Entity entity)
        {
            if (entity.Bounds.X < 0 ||
                entity.Bounds.X + entity.Bounds.Width > World.Width ||
                entity.Bounds.Y < 0 ||
                entity.Bounds.Y + entity.Bounds.Height > World.Height)
                return true;
            return false;
        }

        // Handles collisions between bullets and ships
        protected override bool collisionDetect(Entity entity)
        {
            // Enemy-bullet interaction
            if (((Bullet)entity).IsShotByPlayer) // Enemies can only be hurt by player bullets
            {
                foreach (Ship enemy in world.Enemies)
                {
                    if (enemy.Bounds.Intersects(entity.Bounds))
                        enemy.Dead = true;
                }
            }

            // Player-bullet interaction
            else // Players can only be hurt by enemy bullets
            {
                if (world.Player.Bounds.Intersects(entity.Bounds))
                    world.Player.Dead = true;
            }

            return false;
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            // Bullet velocity does not change
        }
    }
}
