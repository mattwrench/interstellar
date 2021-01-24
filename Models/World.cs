using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Models
{
    // Container class for all models
    class World
    {
        public const int Width = 1280;
        public const int Height = 720;

        public Ship Player;

        public World()
        {
            // Spawn player at center of world
            Player = new Ship(new Vector2(Width / 2, Height / 2), Ship.Type.Player);
        }
    }
}
