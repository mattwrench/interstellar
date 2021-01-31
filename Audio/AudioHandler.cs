using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Audio
{
    static class AudioHandler
    {
        public const float PlayerShootVolume = 0.2f;
        public const float ExplosionVolume = 0.5f;
        public const int NumSongs = 4;

        public static SoundEffect Shoot, Pause, Explosion, Lose;
        public static Song[] Songs;

        public static void Load(ContentManager content)
        {
            Shoot = content.Load<SoundEffect>("Sounds/shoot");
            Pause = content.Load<SoundEffect>("Sounds/pause");
            Explosion = content.Load<SoundEffect>("Sounds/explosion");
            Lose = content.Load<SoundEffect>("Sounds/lose");

            Songs = new Song[NumSongs];
            Songs[0] = content.Load<Song>("Music/Punch Deck - Bhangra Bass");
            Songs[1] = content.Load<Song>("Music/Punch Deck - More More More");
            Songs[2] = content.Load<Song>("Music/Punch Deck - Signal in the Noise");
            Songs[3] = content.Load<Song>("Music/Punch Deck - The Traveler");
        }
    }
}
