using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HumanInvaders.Background;
using HumanInvaders.Media;
using HumanInvaders.Person;
using HumanInvaders.Shot;

namespace HumanInvaders
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bg_001, bg_002, playerImage, playerEnemy, playerFireballtexture, enemyExplosion;
        SpriteFont score;
        Boolean flag = false;

        Player player;
        Fundo fundo;
        VideoFinal video;
        ScreenManager screen;
        MusicSpace music;
        SoundEffect soundFireBall, soundExplosion;

        KeyboardState key_01;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "Human Invaders";
            graphics.ApplyChanges();
            screen = new ScreenManager(this);            
            Components.Add(screen);
            music = new MusicSpace(this);
            Components.Add(music);
            video = new VideoFinal(this);

            
            

            base.Initialize();
        }



        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            score = Content.Load<SpriteFont>("Fonts/text");

            // Carregando Fundo
            bg_001 = Content.Load<Texture2D>("Images/marte");
            bg_002 = Content.Load<Texture2D>("Images/space");
            fundo = new Fundo(bg_001, graphics, spriteBatch);           

            //  Carregando imagem Inimigo
            playerEnemy = Content.Load<Texture2D>("Images/Naves/NaveFelipe 1");
            enemyExplosion = Content.Load<Texture2D>("Images/explosion");
            
            // Carregando personagem, tiros e sons
            soundExplosion = Content.Load<SoundEffect>("Sound/explosion4");
            soundFireBall = Content.Load<SoundEffect>("Sound/bomb1");
            playerFireballtexture = Content.Load<Texture2D>("Images/fire2");
            playerImage = Content.Load<Texture2D>("Images/Naves/navamonstro");
            player = new Player(GraphicsDevice, new Vector2(400, 300), playerImage, playerEnemy);
            player.Ativo = false;
            player.TextDebug = score;
            player.Shoot = playerFireballtexture;
            player.Sound = soundFireBall;
            player.SoundExplosion = soundExplosion;
            player.State = 1;
            player.Fire = enemyExplosion;
            
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            key_01 = Keyboard.GetState();

            // Carregando a cena do jogo
            if (key_01.IsKeyDown(Keys.Enter))
            {
                fundo.Background(bg_002);
                Components.Remove(screen);
                music.Vol(0.95f);
                music.Start();
                player.Ativo = true;
            }

            if (player.State == 0)
            {
                player.Ativo = false;
                music.Stop();
                //fundo.Background(bg_001);
                
                if (flag == false)
                {
                    Components.Add(video);
                    flag = true;
                }
            }

            // Comandos para o Player
            player.Update(gameTime);

            if (key_01.IsKeyDown(Keys.Escape)) this.Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            fundo.Draw();
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
