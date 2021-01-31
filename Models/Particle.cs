using Interstellar.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    class Particle : Entity
    {
        public Ship.Type ShipColor;

        public Particle(Vector2 pos, Vector2 dir, Ship.Type shipColor)
        {
            // Class attributes
            Bounds.Width = 10;
            Bounds.Height = 4;
            TopSpeed = 1200;

            Position.X = pos.X;
            Position.Y = pos.Y;

            Velocity = dir;
            if (Velocity.LengthSquared() > 0)
                Velocity = Vector2.Multiply(Velocity, TopSpeed);

            SetBounds();

            Rotation = dir.GetAngle();
            ShipColor = shipColor;
        }
    }
}
