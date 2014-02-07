using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HumanInvaders.Media
{
    class VideoFinal : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Video animacao;
        VideoPlayer videoPlayer;
        Boolean state = false;

        public VideoFinal(Game game)
            : base(game)
        {

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);

            animacao = Game.Content.Load<Video>("Video/Filme");
            videoPlayer = new VideoPlayer();
            
            base.LoadContent();
        }

        public void Start()
        {
            videoPlayer.Play(animacao);
            videoPlayer.IsLooped = false;
            videoPlayer.IsMuted = true;
            state = true;
        }

        public void Stop()
        {
            videoPlayer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            if (!state)
            {
                Start();
            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(videoPlayer.GetTexture(), new Rectangle(0, 0, 800,600), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
