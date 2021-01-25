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
    }
}
