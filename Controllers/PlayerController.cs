using Interstellar.Audio;
using Interstellar.Models;
using Interstellar.Utilities;
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
            handleAttack(world.Player, dt);
            setVelocity(world.Player, dt);
            setPosition(world.Player, dt);
            world.Player.SetBounds();
            boundsCheck(world.Player);
            collisionDetect(world.Player);
        }

        // Check for collisions between player and enemies
        // Bullet collisions are handled by BulletController instead
        protected override bool collisionDetect(Entity entity)
        {
            foreach (Ship enemy in world.Enemies)
            {
                if (enemy.Bounds.Intersects(((Ship)entity).CenterBounds))
                {
                    spawnParticles((Ship)entity);
                    ((Ship)entity).Dead = true;
                }
            }

            return false;
        }

        protected override void handleAttack(Ship ship, float dt)
        {
            if (Input.Attack)
            {
                ship.ShootTimer += dt;
                if (ship.ShootTimer > ship.ShootRate)
                {
                    ship.ShootTimer -= ship.ShootRate;

                    Vector2 dir = new Vector2();
                    if (Input.IsUsingGamePad)
                    {
                        if (Input.RightThumbstick.LengthSquared() > 0)
                            dir = Vector2.Normalize(Input.RightThumbstick);
                    }
                    else // KBM
                    {
                        dir = Vector2.Subtract(Input.MouseWorldPos, ship.Position);
                        if (dir.LengthSquared() > 0)
                            dir.Normalize();
                    }

                    Vector2 pos = new Vector2(ship.Position.X, ship.Position.Y);
                    pos = Vector2.Add(pos, Vector2.Multiply(dir, BulletVerticalSpawnDist));

                    world.Bullets.Add(new Bullet(pos, dir, true));

                    // Spawn additional bullet to left and right of center bullet
                    Vector2 offset = new Vector2(dir.X, dir.Y);
                    offset = Vector2.Multiply(offset.Rotate(90), BulletHorizontalSpawnDist);
                    pos = Vector2.Add(pos, offset);
                    world.Bullets.Add(new Bullet(pos, dir, true));

                    // Subtract offset twice to counter previous addition
                    pos = Vector2.Subtract(pos, offset);
                    pos = Vector2.Subtract(pos, offset);
                    world.Bullets.Add(new Bullet(pos, dir, true));
                    AudioHandler.Shoot.Play(AudioHandler.PlayerShootVolume, 0, 0);
                }
            }

            // Reset attack timer
            else
            {
                ship.ShootTimer = 0;
            }
        }

        protected override void setRotation(Ship ship)
        {
            if (Input.IsUsingGamePad)
            {
                if (Input.RightThumbstick.LengthSquared() > 0) // Only update if past deadzone
                {
                    ship.Rotation = Input.RightThumbstick.GetAngle();
                }
            }
            else // KBM
            {
                Vector2 dir = Vector2.Subtract(Input.MouseWorldPos, ship.Position);

                if (dir.LengthSquared() > 0)
                    ship.Rotation = dir.GetAngle();
            }
        }

        protected override void setVelocity(Entity entity, float dt)
        {
            entity.Velocity = new Vector2();

            if (Input.IsUsingGamePad)
            {
                entity.Velocity = Input.LeftThumbstick;
                // Only normalize if length > 1 to allow for small thumbstick movements
                if (entity.Velocity.LengthSquared() > 0)
                    entity.Velocity = Vector2.Normalize(entity.Velocity);
                entity.Velocity = Vector2.Multiply(entity.Velocity, entity.TopSpeed);
            }
            else // KBM
            {
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
}
