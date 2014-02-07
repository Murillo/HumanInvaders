using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HumanInvaders.Person
{
    class Enemy
    {
        Vector2 position, posInicial;
        public Texture2D picture;
        public float PosicaoInicial { get; private set; }
        public Rectangle Retangulo { get; set; }
        public Vector2 Position { get { return position; } }
        public float Speed { get; set; }

        public Enemy(Texture2D enemyPicture, Vector2 startPosition, Vector2 positionInicial, float updateSpeed)
        {
            picture = enemyPicture;
            position = startPosition;
            Speed = updateSpeed;
            Retangulo = new Rectangle(0, 0, 12, 12);
            posInicial = positionInicial;
            PosicaoInicial = positionInicial.X;
        }

        public float WidthPicture()
        {
            return picture.Width;
        }

        public void Update(GameTime gameTime)
        {
            position.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(picture, position, null, Color.White, 0.0f, posInicial, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
