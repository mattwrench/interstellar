using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    static class Input
    {
        public const float ThumbStickDeadZone = .1f;

        public static bool Attack = false;
        public static bool Pause = false;
        public static bool IsUsingGamePad = false;

        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        private static GamePadState gamePadState, lastGamePadState;
        private static Camera2D camera;

        public static void Reset()
        {
            Attack = false;
            Pause = false;
        }

        public static void Update(Camera2D cam)
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            lastGamePadState = gamePadState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            gamePadState = GamePad.GetState(getPlayerIndex());

            // Check for swap between gamepad and KBM
            checkControlType();

            camera = cam;

            // Attack
            if (IsUsingGamePad)
            {
                if (RightThumbstick.LengthSquared() > ThumbStickDeadZone * ThumbStickDeadZone)
                    Attack = true;
                else
                    Attack = false;
            }
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    Attack = true;
                else
                    Attack = false;
            }

            // Pause
            if (IsUsingGamePad)
            {
                if (gamePadState.IsButtonDown(Buttons.Start) && !lastGamePadState.IsButtonDown(Buttons.Start))
                    Pause = !Pause;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Escape) && !lastKeyboardState.IsKeyDown(Keys.Escape))
                    Pause = !Pause;
            }
        }

        private static void checkControlType()
        {
            if (gamePadState.ThumbSticks.Left.LengthSquared() > ThumbStickDeadZone * ThumbStickDeadZone
                || gamePadState.IsButtonDown(Buttons.Start))
                IsUsingGamePad = true;

            if (keyboardState.GetPressedKeys().Length > 0 || 
                mouseState.LeftButton == ButtonState.Pressed || 
                mouseState.RightButton == ButtonState.Pressed)
                IsUsingGamePad = false;
        }

        // Get lowest-indexed, connected controller
        private static PlayerIndex getPlayerIndex()
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                return PlayerIndex.One;
            else if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                return PlayerIndex.Two;
            else if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                return PlayerIndex.Three;
            else if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                return PlayerIndex.Four;
            return PlayerIndex.One;
        }

        public static Vector2 LeftThumbstick
        {
            get
            {
                if (gamePadState.ThumbSticks.Left.LengthSquared() > ThumbStickDeadZone * ThumbStickDeadZone)
                {
                    // Invert y-axis to match rest of game
                    return new Vector2(gamePadState.ThumbSticks.Left.X, -gamePadState.ThumbSticks.Left.Y);
                }
                return new Vector2();
            }
        }

        public static Vector2 RightThumbstick
        {
            get
            {
                if (gamePadState.ThumbSticks.Right.LengthSquared() > ThumbStickDeadZone * ThumbStickDeadZone)
                {
                    // Invert y-axis to match rest of game
                    return new Vector2(gamePadState.ThumbSticks.Right.X, -gamePadState.ThumbSticks.Right.Y);
                }
                return new Vector2();
            }
        }

        public static Vector2 MouseScreenPos
        {
            get
            {
                return new Vector2(mouseState.X, mouseState.Y);
            }
        }

        public static Vector2 MouseWorldPos
        {
            get 
            {
                return new Vector2(mouseState.X + camera.Location.X - camera.ViewportWidth / 2,
                    mouseState.Y + camera.Location.Y - camera.ViewportHeight / 2);
            }
        }

        public static bool MoveLeft
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left);
            }
        }

        public static bool MoveRight
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right);
            }
        }

        public static bool MoveUp
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up);
            }
        }

        public static bool MoveDown
        {
            get
            {
                return keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down);
            }
        }
    }
}
