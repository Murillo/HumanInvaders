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

namespace HumanInvaders.Background
{
    class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Vector2 posicaoLogo, posicaoMonstro;
        SpriteBatch spriteBatch;
        SpriteFont texto;
        Texture2D nave, logo;
        String flag = "Left";
        Color cor = Color.White;
        int timeSizeLastFrame = 0;


        public ScreenManager(Game game)
            : base(game)
        {

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            texto = Game.Content.Load<SpriteFont>("Fonts/text");
            nave = Game.Content.Load<Texture2D>("Images/Naves/navamonstro");
            logo = Game.Content.Load<Texture2D>("Images/Logo");
            posicaoLogo = new Vector2(125, 125);
            posicaoMonstro = new Vector2(50, 440);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {   

            AnimacaoNave();
            BotapPress(gameTime);

            base.Update(gameTime);
        }

        private void BotapPress(GameTime gameTime)
        {
            timeSizeLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSizeLastFrame > 500)
            {
                timeSizeLastFrame -= 500;
                if (cor == Color.White)
                {
                    cor = Color.Gray;
                }
                else
                {
                    cor = Color.White;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            spriteBatch.DrawString(texto, "Press Enter", new Vector2(260, 350), cor);
            spriteBatch.Draw(nave, posicaoMonstro, Color.White);
            spriteBatch.Draw(logo, posicaoLogo, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AnimacaoNave()
        {
            if ((posicaoMonstro.X + nave.Width) == 800)
            {
                flag = "Right";
            }
            else if (posicaoMonstro.X == 0)
            {
                flag = "Left";
            }

            if (flag.Equals("Right"))
            {
                posicaoMonstro.X -= 2;
            }
            else
            {
                posicaoMonstro.X += 2;
            }
        }
    }
}
