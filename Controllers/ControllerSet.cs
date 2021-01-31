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
        private EnemyController enemyController;
        private BulletController bulletController;
        private ParticleController particleController;

        public ControllerSet(World world)
        {
            playerController = new PlayerController(world);
            enemyController = new EnemyController(world);
            bulletController = new BulletController(world);
            particleController = new ParticleController(world);
        }

        public void Update(float dt)
        {
            playerController.Update(dt);
            enemyController.Update(dt);
            bulletController.Update(dt);
            particleController.Update(dt);
        }
    }
}
