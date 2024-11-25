using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NEA_logic {
    public class Game1 : Game {

        #region Fields
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private int N = 3; //pattern size

        private Texture2D sourcePNG; //1014 x 1014

        private int size = 1024; //height and width are same

        private Dictionary<Rectangle, string> patterns; //sub image of source PNG, along with its rotation as a string

        #endregion

        public Game1() {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics.ToggleFullScreen();

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sourcePNG = Content.Load<Texture2D>("Source"); //loading png into variable
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White); //bg to whtie

            spriteBatch.Begin();

            spriteBatch.Draw(sourcePNG, new Rectangle(0,0,1026,1026), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        static void Main(string[] args) {
            // get input

            // process input into patterns

            // initialise output

            //while(not fully collapsed), oberve + propagate
        }

        public void getInput() {
            int count = 0;

            for(int y = 0; y <= size - N; y++) {
                for(int x = 0; x <= size - N; x++) {

                    patterns.Add(new Rectangle(x, y, N, N),"0deg"); // will rotate one sub-section of the image to ensure all possible patterns are recorded
                    patterns.Add(new Rectangle(x, y, N, N), "90deg");
                    patterns.Add(new Rectangle(x, y, N, N), "180deg");
                    patterns.Add(new Rectangle(x, y, N, N), "270deg");

                }
            }

            foreach(KeyValuePair<Rectangle,string> pattern in patterns) {
                for(int j = count+1; j < patterns.Count; j++) {
                    
                }
            }
        }
    }
}

/*

TO DO LIST!:
- figure out what data structure to use to store patterns

*/