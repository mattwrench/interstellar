using Interstellar.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Interstellar
{
    public class Interstellar : Game
    {
        private GraphicsDeviceManager graphics;
        private Renderer renderer;

        public Interstellar()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "Interstellar Wars";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            renderer = new Renderer(graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            renderer.Render();
            base.Draw(gameTime);
        }
    }
}
