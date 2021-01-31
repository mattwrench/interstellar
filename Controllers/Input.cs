using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar.Controllers
{
    static class Input
    {
        public static bool Attack = false;

        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        private static Camera2D camera;

        public static void Reset()
        {
            Attack = false;
        }

        public static void Update(Camera2D cam)
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            camera = cam;

            if (mouseState.LeftButton == ButtonState.Pressed)
                Attack = true;
            else
                Attack = false;
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
