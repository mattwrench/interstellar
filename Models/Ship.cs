using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    class Ship : Entity
    {
        public float ShootTimer, ShootRate;
        public enum Type
        {
            Player, Roamer, Chaser, Runner, Shooter
        }

        public Type ShipType;
        public bool HitByBullet;

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
                    TopSpeed = 200;
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
                    TopSpeed = 250;
                    ShootRate = 1.5f;
                    break;
            }
            Position.X = pos.X;
            Position.Y = pos.Y;

            ShipType = type;

            SetBounds();

            ShootTimer = 0;
            HitByBullet = false;
        }
    }
}
