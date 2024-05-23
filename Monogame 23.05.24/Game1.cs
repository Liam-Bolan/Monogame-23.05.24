using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_23._05._24
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;
        SpriteFont GameFont;
        Texture2D heroTexture;
        Vector2 heroLocation = new Vector2(100, 100);

        
        enum Direction
        {
            down,
            left,
            right,
            up,
        }
        Vector2 heroMovement = new Vector2(0, 0);
        Direction heroDirection = Direction.down;
        float heroFrameTime = 100;
        int heroFrame = 0;
        float timeExpired;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            heroTexture = Content.Load<Texture2D>(@"SpriteMapHero");
            GameFont = Content.Load<SpriteFont>(@"MyFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.A))
            {
                heroMovement.X = -2;
                heroMovement.Y = 0;
                heroDirection = Direction.left;

            }
            if (ks.IsKeyDown(Keys.D))
            {
                heroMovement.X = 2;
                heroMovement.Y = 0;
                heroDirection = Direction.right;

            }
            if (ks.IsKeyDown(Keys.S))
            {
                heroMovement.X = 0;
                heroMovement.Y = 2;
                heroDirection = Direction.down;

            }
            if (ks.IsKeyDown(Keys.W))
            {
                heroMovement.X = 0;
                heroMovement.Y = -2;
                heroDirection = Direction.up;
            }
            if (ks.IsKeyDown(Keys.Space))
            {
                heroMovement.X = 0;
                heroMovement.Y = 0;
                heroDirection = Direction.down;
            }
            heroLocation = heroLocation + heroMovement;
            timeExpired += gameTime.ElapsedGameTime.Milliseconds;

            if(timeExpired > heroFrameTime)
            {
                heroFrame = (heroFrame + 1) % 4;
                timeExpired = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            int X = heroFrame * 32;
            int Y = ((int)heroDirection * 54);
            spriteBatch.Draw(heroTexture, heroLocation, new Rectangle(X, Y, 32, 54), Color.White);
            spriteBatch.DrawString(GameFont, "Survived for " + (int)gameTime.TotalGameTime.TotalSeconds + " seconds", new Vector2(0, 0), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
