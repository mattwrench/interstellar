using Interstellar.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    class PlayerController : ShipController
    {
        public PlayerController(World w) : base(w)
        {
        }

        public override void Update(float dt)
        {
            setRotation(world.Player);
            setVelocity(world.Player, dt);
            setPosition(world.Player, dt);
            world.Player.SetBounds();
            boundsCheck(world.Player);
            collisionDetect(world.Player);
        }

        protected override bool collisionDetect(Entity entity)
        {
            // TODO
            return false;
        }

        protected override void handleAttack(Ship ship, float dt)
        {
            // TODO
        }

        protected override void setRotation(Ship ship)
        {
            // TODO
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            entity.Velocity = new Vector2();

            if (Input.MoveLeft)
                entity.Velocity.X -= 1;
            if (Input.MoveRight)
                entity.Velocity.X += 1;
            if (Input.MoveUp)
                entity.Velocity.Y -= 1;
            if (Input.MoveDown)
                entity.Velocity.Y += 1;

            if (entity.Velocity.LengthSquared() > 0)
                entity.Velocity.Normalize();
            entity.Velocity = Vector2.Multiply(entity.Velocity, entity.TopSpeed);
        }
    }
}
