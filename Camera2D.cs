using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interstellar
{
    class Camera2D
    {
        public float Zoom;
        public Vector2 Location;
        public float Rotation;
        public int ViewportWidth, ViewportHeight;

        public Matrix TransformMatrix
        {
            get {
                return
                    Matrix.CreateTranslation(new Vector3(-Location.X, -Location.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
            }
        }

        public Camera2D(int viewportWidth, int viewportHeight, Vector2 location)
        {
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
            Location = new Vector2(location.X, location.Y);

            Zoom = 1.0f;
            Rotation = 0f;
        }
    }
}
