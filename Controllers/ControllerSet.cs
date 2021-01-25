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

        public ControllerSet(World w)
        {
            playerController = new PlayerController(w);
        }

        public void Update(float dt)
        {
            playerController.Update(dt);
        }
    }
}
