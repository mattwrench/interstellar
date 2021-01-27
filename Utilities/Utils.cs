using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Utilities
{
    static class Utils
    {
        public static float GetAngle(this Vector2 vec)
        {
            float radians = (float) Math.Atan2(vec.Y, vec.X);
            return MathHelper.ToDegrees(radians);
        }
    }
}
