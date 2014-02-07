using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HumanInvaders.Media
{
    class MusicSpace : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Song music;

        public MusicSpace(Game game)
            : base(game)
        {

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);

            music = Game.Content.Load<Song>("Music/the_planet_music");
            
            base.LoadContent();
        }

        public void Vol(float volume)
        {
            MediaPlayer.Volume = volume;
        }

        public void Start()
        {
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}
