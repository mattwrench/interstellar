using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Utilities
{
    static class Utils
    {
        public const int DegreesInCircle = 360;

        public static float GetAngle(this Vector2 vec)
        {
            float radians = (float) Math.Atan2(vec.Y, vec.X);
            return MathHelper.ToDegrees(radians);
        }

        // Rotate vector by n degrees
        public static Vector2 Rotate(this Vector2 vec, float degrees)
        {
            float radians = MathHelper.ToRadians(degrees);
            float newX = (float)(Math.Cos(radians) * vec.X - Math.Sin(radians) * vec.Y);
            float newY = (float)(Math.Sin(radians) * vec.X + Math.Cos(radians) * vec.Y);
            return new Vector2(newX, newY);
        }
    }
}
