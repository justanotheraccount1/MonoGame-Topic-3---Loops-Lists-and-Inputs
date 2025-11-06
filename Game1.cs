using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGame_Topic_3___Loops_Lists_and_Inputs
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        KeyboardState keyboardState;

        Texture2D grassTexture, mowerTexture;
        Rectangle window, mowerRect;

        SoundEffect mowerSound;
        SoundEffectInstance mowerSoundInstance;

        Vector2 mowerSpeed;

        List<Rectangle> grassTiles = new List<Rectangle>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            window = new Rectangle(0, 0, 600, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            mowerRect = new Rectangle(100, 100, 30, 30);
            for (int x = 0; x < window.Width; x += 5)
            {
                for (int y = 0; y < window.Height; y += 5)
                {
                    grassTiles.Add(new Rectangle(x, y, 5, 5));
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mowerTexture = Content.Load<Texture2D>("Images/mower");
            grassTexture = Content.Load<Texture2D>("Images/long_grass");
            mowerSound = Content.Load<SoundEffect>("Sounds/mower_sound");
            mowerSoundInstance = mowerSound.CreateInstance();
            mowerSoundInstance.IsLooped = true;
            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardState = Keyboard.GetState();

            mowerSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                mowerSpeed.Y -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                mowerSpeed.X += 1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                mowerSpeed.X -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                mowerSpeed.Y += 1;
            }
            mowerRect.Offset(mowerSpeed);
            if (mowerSpeed == Vector2.Zero)
            {
                mowerSoundInstance.Stop();
            }
            else
            {
                mowerSoundInstance.Play();
            }
            if (mowerRect.Left <= window.Left)
            {
                mowerRect.X = 0;
            }
            if (mowerRect.Right >= window.Right)
            {
                mowerRect.X = window.Width - mowerRect.Width;
            }
            if (mowerRect.Top <= window.Top)
            {
                mowerRect.Y = 0;
            }
            if (mowerRect.Bottom >= window.Bottom)
            {
                mowerRect.Y = window.Height - mowerRect.Height;
            }
            for (int i = 0; i < grassTiles.Count; i++)
            {
                if (mowerRect.Contains(grassTiles[i]))
                {
                    grassTiles.RemoveAt(i);
                    i--;
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            _spriteBatch.Begin();
            foreach(Rectangle grass in grassTiles)
            {
                _spriteBatch.Draw(grassTexture, grass, Color.White);
            }
            _spriteBatch.Draw(mowerTexture, mowerRect, Color.White);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
