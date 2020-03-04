using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball;
        Vector2 ballposition = Vector2.Zero;
        Vector2 ballVelocity;
        Random random = new Random();
        Texture2D paddle;
        Rectangle paddleRect = new Rectangle();
        int paddleSpeed = 0;

        KeyboardState oldKeyBoardState;
        KeyboardState newKeyboardState;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
        
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            ballVelocity = new Vector2(
                (float)random.NextDouble(),
                (float)random.NextDouble()
            );

            ballVelocity.Normalize();

            paddleRect.X = 0;
            paddleRect.Y = 0;
            paddleRect.Width = 50;
            paddleRect.Height = 200;

            newKeyboardState = new KeyboardState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("ball");
            paddle = Content.Load<Texture2D>("pixel");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            this.oldKeyBoardState = Keyboard.GetState();
            // TODO: Add your update logic here
            ballposition += (float)gameTime.ElapsedGameTime.TotalMilliseconds * 2 * ballVelocity;

            if (newKeyboardState.IsKeyDown(Keys.Up) && !oldKeyBoardState.IsKeyDown(Keys.Up))
            {
                paddleSpeed -= 1;
            }

            if (newKeyboardState.IsKeyDown(Keys.Down) && !oldKeyBoardState.IsKeyDown(Keys.Down))
            {
                paddleSpeed += 1;
            }

            paddleRect.Y += paddleSpeed;



            //check for wall collision
            //top of screen
            if (ballposition.Y < 0)
            {
                ballVelocity.Y *= -1;
                float delta = 0 - ballposition.Y;
                ballposition.Y += 2 * delta;
            }
            //check bottom of screen
            if (ballposition.Y > graphics.PreferredBackBufferHeight - 100)
            {
                ballVelocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 100 - ballposition.Y;
                ballposition.Y += 2 * delta;
            }

            if (ballposition.X < 0)
            {
                ballVelocity.X *= -1;
                float delta = 0 - ballposition.X;
                ballposition.X += 2 * delta;
            }

            if (ballposition.X > graphics.PreferredBackBufferWidth - 100)
            {
                ballVelocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 100 - ballposition.X;
                ballposition.X += 2 * delta;
            }

            //KeyboardState newKeyboardState = Keyboard.GetState();

            //if (newKeyboardState.IsKeyDown(Keys.Left))
            //{
            //    ballposition.X = ballposition.X - 5;
            //}
            //if (newKeyboardState.IsKeyDown(Keys.Right))
            //{
            //    ballposition.X = ballposition.X + 5;
            //}
            //if (newKeyboardState.IsKeyDown(Keys.Up))
            //{
            //    ballposition.Y = ballposition.Y - 5;
            //}
            //if (newKeyboardState.IsKeyDown(Keys.Down))
            //{
            //    ballposition.Y = ballposition.Y + 5;
            //}


          
            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                paddleRect.Y = paddleRect.Y - 5;
            }
            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                paddleRect.Y = paddleRect.Y + 5;
            }
            if(paddleRect.Y < 0)
            {
                paddleRect.Y = 0;
            }
            if(paddleRect.Y > GraphicsDevice.Viewport.Height - paddleRect.Height)
            {
                paddleRect.Y = GraphicsDevice.Viewport.Height - paddleRect.Height;
            }
            base.Update(gameTime);
            newKeyboardState = oldKeyBoardState; 
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ball, new Rectangle((int)ballposition.X, (int)ballposition.Y, 100, 100), Color.White);
            spriteBatch.Draw(paddle, paddleRect, Color.Red);
            spriteBatch.End();

           
          //  spriteBatch.Draw(paddle, paddleRect, Color.Red);
          

            base.Draw(gameTime);
        }
    }
}
