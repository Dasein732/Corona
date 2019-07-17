using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Program
{
    public sealed class Engine : Game
    {
        private readonly GraphicsDeviceManager _deviceManager;
        private Renderer _renderer;
        private SpriteBatch _spriteBatch;
        private Texture2D _frameBuffer;

        public Engine(int x, int y)
        {
            // Manager must be set before Initialize is called
            _deviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = x,
                PreferredBackBufferHeight = y
            };
            _deviceManager.ApplyChanges();
        }

        protected override void Initialize()
        {
            Renderer.Initialize(_deviceManager.PreferredBackBufferWidth, _deviceManager.PreferredBackBufferHeight)
                .Match(
                exception =>
                {
                    Console.WriteLine("Initialization failed!");
                    Console.WriteLine(exception.Message);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                },
                renderer => _renderer = renderer);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _frameBuffer = new Texture2D(_deviceManager.GraphicsDevice, _deviceManager.PreferredBackBufferWidth, _deviceManager.PreferredBackBufferHeight, false, SurfaceFormat.Vector4);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = false;

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _frameBuffer.SetData(_renderer.NextFrame());

            _spriteBatch.Begin();
            _spriteBatch.Draw(_frameBuffer, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            Console.WriteLine((int)(1 / gameTime.ElapsedGameTime.TotalSeconds));

            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            _renderer.Dispose();
            _frameBuffer.Dispose();
            _spriteBatch.Dispose();
            _deviceManager.Dispose();

            base.OnExiting(sender, args);
        }
    }
}