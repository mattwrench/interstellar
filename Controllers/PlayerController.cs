using Interstellar.Models;
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
            // TODO
        }
    }
}
