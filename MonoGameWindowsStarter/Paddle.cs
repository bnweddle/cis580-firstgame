using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Paddle
    {

        BoundingRectangle bound;
        Game1 Game;
        Texture2D texture;


        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">Reference to the game</param>
        public Paddle(Game1 game)
        {
            this.Game = game;   
        }

        public void LoadContent(ContentManager content)
        {
            bound.Width = 50;
            bound.Height = 200;
            bound.X = 0;
            bound.Y = Game.GraphicsDevice.Viewport.Height / 2 - bound.Height / 2;
            texture = content.Load<Texture2D>("onepixel");
        }

        public void Update(GameTime gameTime)
        {
            //Movement
            var newState = Keyboard.GetState();

            // increasing or/and decreasing the speed of the paddle
            if (newState.IsKeyDown(Keys.Up))
            {
                bound.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                //move up
            }
            if (newState.IsKeyDown(Keys.Down))
            {
                bound.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                //move down
            }

            // Making sure paddle doesn't go off screen
            if (bound.Y < 0)
            {
                bound.Y = 0;
            }
            if (bound.Y > Game.GraphicsDevice.Viewport.Height - bound.Height)
            {
                bound.Y = Game.GraphicsDevice.Viewport.Height - bound.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bound, Color.Red);
        }
    }
}
