using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    class Ship : Entity
    {
        public enum Type
        {
            Player, Roamer, Chaser, Runner, Shooter
        }

        public Type ShipType;

        public Ship(Vector2 pos, Type type)
        {
            // Class attributes
            switch (type)
            {
                case Type.Player:
                    Bounds.Width = 50;
                    Bounds.Height = 50;
                    TopSpeed = 400;
                    break;
                case Type.Roamer:
                    Bounds.Width = 25;
                    Bounds.Height = 25;
                    TopSpeed = 200;
                    break;
                case Type.Chaser:
                    Bounds.Width = 25;
                    Bounds.Height = 25;
                    TopSpeed = 300;
                    break;
                case Type.Runner:
                    Bounds.Width = 50;
                    Bounds.Height = 32;
                    TopSpeed = 500;
                    break;
                case Type.Shooter:
                    Bounds.Width = 40;
                    Bounds.Height = 40;
                    TopSpeed = 250;
                    break;
            }
            Position.X = pos.X;
            Position.Y = pos.Y;

            ShipType = type;

            SetBounds();
        }
    }
}
