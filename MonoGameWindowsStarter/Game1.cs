﻿/* Author: Nathan Bean
 * Modified by: Bethany Weddle
 * Date Modified: 1-31-20
 * First Game CIS 520
 * */
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
        // Won't touch till the middle of semester
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball;
        Random random = new Random();
        Vector2 ballPosition = Vector2.Zero;
        Vector2 ballVelocity;

        Paddle paddle;

        KeyboardState oldstate;
        KeyboardState newState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paddle = new Paddle(this);
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
                (float)random.NextDouble());

            //same speed, random direction
            ballVelocity.Normalize();


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
            ball = Content.Load<Texture2D>("ball");
            paddle.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Any you are manually loading need to be unloaded here.
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(newState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            paddle.Update(gameTime);

            // TODO: Add your update logic here
            ballPosition += (float)gameTime.ElapsedGameTime.TotalMilliseconds * ballVelocity;

            //Check for wall collisons, depends on where your wall is
            if(ballPosition.Y < 0) //top of screen
            {
                //invert direction 
                ballVelocity.Y *= -1;
                float delta = 0 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }

            if(ballPosition.Y > graphics.PreferredBackBufferHeight - 100) // Bottom of screen
            {
                ballVelocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 100 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }

            if(ballPosition.X < 0) // Side of Screen
            {
                ballVelocity.X *= -1;
                float delta = 0 - ballPosition.X;
                ballPosition.X += 2 * delta;

                //replace with ballVelocity.X = Zero;
            }

            if (ballPosition.X > graphics.PreferredBackBufferWidth - 100) // Side of screen
            {
                ballVelocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 100 - ballPosition.X;
                ballPosition.X += 2 * delta;
            }

            /* Add when you have finished Ball implementation
            if(paddle.Bounds.CollidesWith(ball.Bounds))
            {
                ball.Velocity.X *= -1;
                var delta = (paddle.Bounds.X + paddle.Bounds.Width) - (ball.Bounds.X - ball.Bounds.Radius);
                ball.Bounds.X += 2 * delta;
            }
            */

            newState = oldstate;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(ball, new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 100, 100), Color.White);
            paddle.Draw(spriteBatch);
            spriteBatch.End();

            //Want to do at the end of the method
            base.Draw(gameTime);
        }
    }
}
