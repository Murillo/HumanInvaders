using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HumanInvaders.Shot
{
    class Fireball
    {
        Vector2 position;
        Texture2D picture;
        float speed;
        public Rectangle Retangulo { get; set; }

        public Vector2 Position { get { return position; } }

        public Fireball(Texture2D firePicture, Vector2 startPosition, float updateSpeed)
        {
            picture = firePicture;
            position = startPosition;
            speed = updateSpeed;
            Retangulo = new Rectangle(0, 0, 12, 12);
        }

        public void Update(GameTime gameTime)
        {
            position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
        }

        public void Draw(SpriteBatch batch)
        {

            batch.Draw(picture, position, Retangulo, Color.White, 0.0f, new Vector2(10.0f, 10.0f), 1.0f, SpriteEffects.None, 1.0f);
        }
        
    }
}
