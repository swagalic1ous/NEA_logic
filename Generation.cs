using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace NEA_logic {
    public class Generation : Game {

        #region Fields

        private SpriteBatch spriteBatch;

        private int N; //pattern size

        private Texture2D sourcePNG; //5028x5028

        private Rectangle wholeImage;

        #endregion

        public Generation() {
            N = 3;

            wholeImage = new Rectangle(0,0,1014,1014);
        }

        protected override void LoadContent() {

            sourcePNG = Content.Load<Texture2D> ("Tubes"); //loading png into variable

            base.LoadContent(); 
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White); //bg to whtie

            spriteBatch.Draw(sourcePNG, new Vector2(100,100), Color.White);

            base.Draw(gameTime); 
        }

        protected override void Update(GameTime gameTime) {


            base.Update(gameTime);
        }

        static void Main(string[] args) {
            // get input
            
            // process input into patterns

            // initialise output

            //while(not fully collapsed), oberve + propagate
        }

        public void getInput() {

        }
    }
}
