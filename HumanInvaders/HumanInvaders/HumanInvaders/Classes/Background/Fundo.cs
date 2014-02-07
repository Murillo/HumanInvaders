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
    class Fundo : Microsoft.Xna.Framework.Game
    {
        GraphicsDevice dev;
        Texture2D bg_001;
        SpriteBatch spBatch;
        int screenWidth;
        int screenHeight;

        public Fundo(Texture2D bg, GraphicsDeviceManager grafico, SpriteBatch spriteBatch)
        {
            bg_001 = bg;
            spBatch = spriteBatch;
            dev = grafico.GraphicsDevice;
            screenWidth = dev.PresentationParameters.BackBufferWidth;
            screenHeight = dev.PresentationParameters.BackBufferHeight;
        }

        public void Background(Texture2D bg)
        {
            bg_001 = bg;
        }

        public void Draw()
        {
            Rectangle screenRect = new Rectangle(0, 0, screenWidth, screenHeight);
            spBatch.Draw(bg_001, screenRect, Color.White);
        }
    }
}
