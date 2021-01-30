using Interstellar.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    class Ship : Entity
    {
        public const float FadeInLength = 1.5f;

        public float ShootTimer, ShootRate;
        public enum Type
        {
            Player, Roamer, Chaser, Runner, Shooter
        }

        public Type ShipType;
        public bool HitByBullet;
        public float FadeInTimer;
        public Ship(Vector2 pos, Type type)
        {
            // Class attributes
            switch (type)
            {
                case Type.Player:
                    Bounds.Width = 50;
                    Bounds.Height = 50;
                    TopSpeed = 400;
                    ShootRate = 0.1f;
                    break;
                case Type.Roamer:
                    Bounds.Width = 25;
                    Bounds.Height = 25;
                    TopSpeed = 250;
                    ShootRate = 0; // Does not shoot
                    break;
                case Type.Chaser:
                    Bounds.Width = 25;
                    Bounds.Height = 25;
                    TopSpeed = 300;
                    ShootRate = 0;
                    break;
                case Type.Runner:
                    Bounds.Width = 50;
                    Bounds.Height = 32;
                    TopSpeed = 500;
                    ShootRate = 0;
                    break;
                case Type.Shooter:
                    Bounds.Width = 40;
                    Bounds.Height = 40;
                    TopSpeed = 200;
                    ShootRate = 1.5f;
                    break;
            }
            Position.X = pos.X;
            Position.Y = pos.Y;

            ShipType = type;

            SetBounds();

            ShootTimer = 0;
            HitByBullet = false;

            FadeInTimer = 0;

            // Randomize roamer velocity
            if (type == Type.Roamer)
            {
                Random rand = new Random();
                Velocity.X = TopSpeed;
                Velocity = Velocity.Rotate(rand.Next(Utils.DegreesInCircle));
            }

            // Randomize runner axis of movement
            if (type == Type.Runner)
            {
                Random rand = new Random();
                if (rand.Next() % 2 == 0) // Vertical
                {
                    int temp = Bounds.Width;
                    Bounds.Width = Bounds.Height;
                    Bounds.Height = temp;
                    if (rand.Next() % 2 == 0)
                        Velocity.Y = TopSpeed;
                    else
                        Velocity.Y = -TopSpeed;
                }
                else // Horizontal
                {
                    if (rand.Next() % 2 == 0)
                        Velocity.X = TopSpeed;
                    else
                        Velocity.X = -TopSpeed;
                }
            }
        }
    }
}
