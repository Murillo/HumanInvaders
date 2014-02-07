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
using HumanInvaders.Shot;

namespace HumanInvaders.Person
{
    class Player
    {
        Texture2D shipSprite;
        Vector2 position, spriteOrigin;
        Rectangle person;
        KeyboardState key_01;

        public Texture2D imgEnemy;
        public SoundEffect Sound { get; set; }
        public SpriteFont TextDebug { get; set; }
        public Boolean Ativo { get; set; }
        public Texture2D Shoot { get; set; }
        public Texture2D Fire { get; set; }
        public Rectangle Topo { get; set; }
        public SoundEffect SoundExplosion { get; set; }
        public int State { get; set; }
        String txtDebug;
        List<Fireball> playerFireballList = new List<Fireball>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Explosion> explosionList = new List<Explosion>();
        float btnFireDelay = 0.0f;
        float explosionDeley = 0.0f;
        float enemyDelay = 0.0f;
        float speedEnemy = 20.0f;
        int score = 0;

        public Vector2 Position { get { return position; } }

        public Player(GraphicsDevice device, Vector2 pos, Texture2D sprite, Texture2D ShipEnemy)
        {
            imgEnemy = ShipEnemy;
            person = new Rectangle(0, 0, 90, 66);
            position = pos;
            shipSprite = sprite;
            spriteOrigin = new Vector2(((float)shipSprite.Width / 2.0f), -200);
        }

        public void Update(GameTime gameTime) 
        {
            key_01 = Keyboard.GetState();
            if (Ativo)
            {
                #region Player: Movimento para a Esquerda
                if (key_01.IsKeyDown(Keys.Left))
                {
                    if ((position.X - 50) > 0)
                    {
                        position.X -= 3;
                    }
                    
                }
                #endregion

                #region Player: Movimento para a Direita
                if (key_01.IsKeyDown(Keys.Right))
                {
                    if ((position.X - shipSprite.Width) < 650)
                    {
                        position.X += 3;        
                    }

                }
                #endregion

                #region Player: Disparando um tiro
                Boolean flag = false;

                try
                {
                    for (int i = 0; i < playerFireballList.Count; i++)
                    {
                        playerFireballList[i].Update(gameTime);

                        for (int j = 0; j < enemyList.Count; j++)
                        {
                            txtDebug = enemyList[j].Position.Y.ToString();
                            if (playerFireballList[i].Position.X >= (enemyList[j].PosicaoInicial * -1) &&
                            playerFireballList[i].Position.X <= ((enemyList[j].PosicaoInicial * -1) + enemyList[j].WidthPicture()) &&
                            playerFireballList[i].Position.Y < (enemyList[j].Position.Y + 90))
                            {
                                foreach (Enemy item in enemyList)
                                {
                                    item.Speed += 10;
                                    speedEnemy += 10;
                                }

                                ////Explosão
                                Explosion exp = new Explosion(Fire, new Vector2((playerFireballList[i].Position.X - 25), (playerFireballList[i].Position.Y - 50)));
                                explosionList.Add(exp);
                                explosionDeley = 0.06f;
                                SoundExplosion.Play();
                                

                                score++;
                                enemyList.RemoveAt(j);
                                flag = true;
                            }
                        }

                        if (flag)
                        {
                            playerFireballList.RemoveAt(i);
                            flag = false;
                        }
                        else if (playerFireballList[i].Position.Y <= 0)
                        {
                            playerFireballList.RemoveAt(i);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                

                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                btnFireDelay -= elapsed;
                
                // Evento para disparar o tiro
                if (key_01.IsKeyDown(Keys.Space))
                {
                    if (btnFireDelay <= 0.0f)
                    {
                        Vector2 positionFireball = new Vector2(position.X, 500);
                        Fireball shot = new Fireball(Shoot, positionFireball, 200.0f);
                        playerFireballList.Add(shot);
                        btnFireDelay = 0.35f;
                        Sound.Play();
                    }
                    
                }
                #endregion

                #region Inimigos
                enemyDelay -= elapsed;
                Random random = new Random();

                if (enemyDelay <= 0.0f)
                {
                    if (enemyList.Count <= 100)
                    {
                        Enemy enemy2 = new Enemy(imgEnemy, new Vector2(0, 0), new Vector2(-random.Next(0, 650), -10), speedEnemy);
                        enemyList.Add(enemy2);
                    }
                    enemyDelay = 5f;
                }

                // Animação do inimigo avançando
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyList[i].Update(gameTime);
                    if (enemyList[i].Position.Y > 500)
                    {
                        State = 0;
                        enemyList.RemoveAt(i);
                    }   
                }

                #endregion

                #region Removendo explosões
                explosionDeley -= elapsed;
                for (int i = 0; i < explosionList.Count; i++)
                {
                    if (explosionDeley <= 0.0f)
                    {
                        explosionList.RemoveAt(i);
                    }
                }
                #endregion
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if (Ativo)
            {
                // Renderiza os Tiros na tela
                foreach (Fireball f in playerFireballList)
                    f.Draw(batch);
                // Renderiza o player
                batch.Draw(shipSprite, position, person, Color.White, 0.0f, spriteOrigin, 1.0f, SpriteEffects.None, 0.0f);
                // Renderiza a pontuação do players
                batch.DrawString(TextDebug, "Score: "+ Convert.ToString(score).PadLeft(2,'0'), new Vector2(0, 0), Color.White);
                // Renderiza os vários enimigos na tela
                foreach (Enemy item in enemyList)
                    item.Draw(batch);
                // Renderiza as explosões na tela
                foreach (Explosion explosion in explosionList)
                    explosion.Draw(batch);
            }
        }
    }
}
