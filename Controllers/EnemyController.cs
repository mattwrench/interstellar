using Interstellar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    class EnemyController : ShipController
    {
        public EnemyController(World w) : base(w)
        {
        }

        public override void Update(float dt)
        {
            spawnEnemies(dt);

            for (int i = world.Enemies.Count - 1; i >= 0; i--)
            {
                Ship enemy = world.Enemies[i];
                setRotation(enemy);
                handleAttack(enemy, dt);
                setVelocity(enemy, dt);
                setPosition(enemy, dt);
                enemy.SetBounds();
                boundsCheck(enemy);
                if (collisionDetect(enemy))
                    world.Enemies.RemoveAt(i);
            }
        }

        private void spawnEnemies(float dt)
        {
            // TODO
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
