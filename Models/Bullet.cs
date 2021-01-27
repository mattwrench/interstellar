using Interstellar.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    class Bullet : Entity
    {
        public bool IsShotByPlayer;
        public Bullet(Vector2 pos, Vector2 dir, bool shotByPlayer)
        {
            // Class attributes
            Bounds.Width = 10;
            Bounds.Height = 10;
            TopSpeed = 800;

            Position.X = pos.X;
            Position.Y = pos.Y;

            Velocity = dir;
            if (Velocity.LengthSquared() > 0)
                Velocity = Vector2.Multiply(Velocity, TopSpeed);

            SetBounds();

            Rotation = dir.GetAngle();

            IsShotByPlayer = shotByPlayer;
        }
    }
}
