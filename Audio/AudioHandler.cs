using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Audio
{
    static class AudioHandler
    {
        public const float PlayerShootVolume = 0.2f;

        public static SoundEffect Shoot, Pause, Explosion, Lose;

        public static void Load(ContentManager content)
        {
            Shoot = content.Load<SoundEffect>("Sounds/shoot");
            Pause = content.Load<SoundEffect>("Sounds/pause");
            Explosion = content.Load<SoundEffect>("Sounds/explosion");
            Lose = content.Load<SoundEffect>("Sounds/lose");
        }
    }
}
