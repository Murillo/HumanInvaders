using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HumanInvaders.Shot
{
    class Explosion
    {
        Texture2D imagem;
        public Vector2 Posicao { get; set; }

        public Explosion(Texture2D img, Vector2 position)
        {
            imagem = img;
            Posicao = position;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sptBatch)
        {
            sptBatch.Draw(imagem, Posicao, Color.White);
        }

    }
}
