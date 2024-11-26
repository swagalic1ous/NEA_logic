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

        private Pattern[] patterns; //sub image of source PNG, along with its rotation as a string

        private int outputSize;

        private Cell[,] wave; //coords of output wave which stores each cell and their possible patterns

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

            for(int y = 0; y <= size - N; y++) { //will go through one pixel at a time... probably should change this?
                for(int x = 0; x <= size - N; x++) {

                    patterns[count] = new Pattern(new Rectangle(x, y, N, N), "0deg"); // will rotate one sub-section of the image to ensure all possible patterns are recorded
                    count++;
                    patterns[count] = new Pattern(new Rectangle(x, y, N, N), "90deg");
                    count++;
                    patterns[count] = new Pattern(new Rectangle(x, y, N, N), "190deg");
                    count++;
                    patterns[count] = new Pattern(new Rectangle(x, y, N, N), "270deg");
                    count++;

                }
            }

            for(int i = 0; i < patterns.Length; i++) { 
                for(int j = count+1; j < patterns.Length; j++) {

                    if (patterns[j] != null || patterns[i] != null) {

                        if (patterns[i].getRectangle().Equals(patterns[j].getRectangle())) {

                            patterns[i].incFrequency();
                            patterns[j] = null;
                            j--;
                            continue;
                        }
                    }
                }
            }
        }

        public void initialiseOutput() { //initalising the initial wave

            for(int y = 0; y <= outputSize; y++) {

                for(int x = 0; x <= outputSize; x++) {

                    foreach(Pattern p in patterns) { //each cell initally has all possible patterns!
                        wave[x,y] = new Cell(x, y, patterns.Length); //default entropy is set to number of possible patterns! =)
                    }
                }
            }
        }

        public List<Cell> findLowestEntropy() {

            int currentLowestEntropy = -1;

            List<Cell> lowestECells = new List<Cell>(); //holds the cells with lowest entropy

            for(int y = 0; y<= outputSize; y++) {
                for(int x = 0; x<= outputSize; x++) {

                    if (wave[x, y].isDefinite()) {

                        continue;
                    }

                    else if(currentLowestEntropy == -1 || wave[x,y].getEntropy() < currentLowestEntropy) { //if current lowest entropy is still not known OR current cell has lower entropy

                        lowestECells.Clear(); //if lower entropy found, clear previous list of cells
                        currentLowestEntropy = wave[x,y].getEntropy();
                    }

                    else if(currentLowestEntropy == wave[x,y].getEntropy()) { //if cell is equal to lowest entropy, add it to the current list of cells!

                        lowestECells.Add(wave[x, y]);
                    }

                }
            }

            return lowestECells;
        }
    }

    public class Pattern {

        private Rectangle rectangle;
        private string rotation;
        private int frequency;

        public Pattern(Rectangle rectangle, string rotation) { 
            this.rectangle = rectangle;
            this.rotation = rotation;
            frequency = 0;
        }

        public Rectangle getRectangle() { return rectangle; }
        public string getRotation() { return rotation; }
        public void incFrequency() { frequency++; } //increments this pattern frequency by one

    }

    public class Cell {

        private Pattern[] possiblePatterns;

        private int x, y;

        private int entropy;

        private bool hasCollapsed;


        public Cell(int x, int y, int entropy) {
            this.x = x;
            this.y = y;
            this.entropy = entropy;
            hasCollapsed = false;
        }

        public int getX() { return x; }
        public int getY() { return y; }
        public int getEntropy() { return entropy; }
        public void incEntropy() { entropy++; }
        public bool isDefinite() { return hasCollapsed; }

        public void collapseCell() { 
            hasCollapsed = true;
            entropy = -1;
        }
    }
}

/*

TO DO LIST!:
- just do stuff

*/
