using Interstellar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    // Container class for all controllers
    class ControllerSet
    {
        private PlayerController playerController;
        private BulletController bulletController;

        public ControllerSet(World world)
        {
            playerController = new PlayerController(world);
            bulletController = new BulletController(world);
        }

        public void Update(float dt)
        {
            playerController.Update(dt);
            bulletController.Update(dt);
        }
    }
}
