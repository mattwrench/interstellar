using Interstellar.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Views
{
    class TextureSet
    {
        public Dictionary<Ship.Type, Texture2D> Ships;
        public Dictionary<Ship.Type, Texture2D> Particles;
        public Texture2D BulletWhite, BulletGreen;

        public TextureSet(ContentManager content)
        {
            Ships = new Dictionary<Ship.Type, Texture2D>();
            Ships.Add(Ship.Type.Player, content.Load<Texture2D>("Images/player"));
            Ships.Add(Ship.Type.Roamer, content.Load<Texture2D>("Images/roamer"));
            Ships.Add(Ship.Type.Chaser, content.Load<Texture2D>("Images/chaser"));
            Ships.Add(Ship.Type.Runner, content.Load<Texture2D>("Images/runner"));
            Ships.Add(Ship.Type.Shooter, content.Load<Texture2D>("Images/shooter"));

            Particles = new Dictionary<Ship.Type, Texture2D>();
            Particles.Add(Ship.Type.Player, content.Load<Texture2D>("Images/particleWhite"));
            Particles.Add(Ship.Type.Roamer, content.Load<Texture2D>("Images/particleBlue"));
            Particles.Add(Ship.Type.Chaser, content.Load<Texture2D>("Images/particlePurple"));
            Particles.Add(Ship.Type.Runner, content.Load<Texture2D>("Images/particleYellow"));
            Particles.Add(Ship.Type.Shooter, content.Load<Texture2D>("Images/particleGreen"));

            BulletWhite = content.Load<Texture2D>("Images/bulletWhite");
            BulletGreen = content.Load<Texture2D>("Images/bulletGreen");
        }
    }
}
