using Interstellar.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    class EnemyController : ShipController
    {
        private const float SpawnRate = 8.0f;
        private const float RoamerSpawnChance = 0.4f;
        private const float ChaserSpawnChance = 0.3f;
        private const float RunnerSpawnChance = 0.2f;
        private const float ShooterSpawnChance = 0.1f;
        private const float MinSpawnDist = 200f;

        private Random rand;
        private float spawnTimer;
        private int spawnCount; // Enemies to spawn

        public EnemyController(World w) : base(w)
        {
            rand = new Random();
            spawnTimer = SpawnRate; // Spawn first enemy immediately
            spawnCount = 1;
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
            spawnTimer += dt;

            if (spawnTimer >= SpawnRate)
            {
                spawnTimer -= SpawnRate;
                for (int i = 0; i < spawnCount; i++)
                {
                    Vector2 pos = new Vector2();
                    do
                    {
                        pos.X = (float)(rand.NextDouble() * World.Width);
                        pos.Y = (float)(rand.NextDouble() * World.Height);
                    } while (Vector2.DistanceSquared(pos, world.Player.Position) < MinSpawnDist * MinSpawnDist);

                    float shipType = (float)(rand.NextDouble());
                    if (shipType < RoamerSpawnChance)
                        world.Enemies.Add(new Ship(pos, Ship.Type.Roamer));
                    else if (shipType < RoamerSpawnChance + ChaserSpawnChance)
                        world.Enemies.Add(new Ship(pos, Ship.Type.Chaser));
                    else if (shipType < RoamerSpawnChance + ChaserSpawnChance + RunnerSpawnChance)
                        world.Enemies.Add(new Ship(pos, Ship.Type.Runner));
                    else
                        world.Enemies.Add(new Ship(pos, Ship.Type.Shooter));
                }
                spawnCount++;
            }
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
