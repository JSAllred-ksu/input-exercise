using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputExample
{
    public class InputManager
    {
        KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;

        MouseState currentMouseState;
        MouseState priorMouseState;

        GamePadState currentGamePadState;
        GamePadState priorGamePadState;

        /// <summary>
        /// the current direction
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// if the warp functionality has been requested
        /// </summary>
        public bool Warp { get; private set; } = false;

        /// <summary>
        /// if the user has requested the game end
        /// </summary>
        public bool Exit { get; private set; } = false;

        public void Update(GameTime gameTime)
        {
            #region State Updating

            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            priorGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            #endregion

            #region Direction Input

            // get direction from gamepad
            Direction = currentGamePadState.ThumbSticks.Right * 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // get direction from keyboard
            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(-200 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(200 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -200 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, 200 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            #endregion

            #region Warp Input

            Warp = false;
            if (currentKeyboardState.IsKeyDown(Keys.Space) && priorKeyboardState.IsKeyUp(Keys.Space))
            {
                Warp = true;
            }
            if (currentGamePadState.IsButtonDown(Buttons.A) && priorGamePadState.IsButtonUp(Buttons.A))
            {
                Warp = true;
            }
            #endregion

            #region Exit Input

            if (currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit = true;
            }

            #endregion
        }
    }
}
