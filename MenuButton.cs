using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;



namespace PlanetParade
{
    /// <summary>
    /// A class for a menu button
    /// </summary>
    public class MenuButton
    {
        #region Fields

        // fields for button image
        Texture2D sprite;
        //const int ImagesPerRow = 2;
        float buttonWidth;
        float buttonHeight;

        // fields for drawing
        Rectangle drawRectangle;
        Rectangle sourceRectangle;

        // click processing
        GameState clickState;        
        TouchCollection touches;
        bool isPressed = false;
        bool touchStarted = false;
        //public bool IsEnabled;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">the sprite for the button</param>
        /// <param name="center">the center of the button</param>
        /// <param name="clickState">the game state to change to when the button is clicked</param>
        public MenuButton(Texture2D sprite, Vector2 center, GameState clickState)
        {
            this.sprite = sprite;
            this.clickState = clickState;
            Initialize(center);
        }
               

		#endregion

  //      #region Properties
  //      /// <summary>
  //      /// Gets drawrectanle of the button удалить потом
  //      /// </summary>
		//public Rectangle ButtonRectangle
  //      {
		//	get { return drawRectangle; }
  //      }

		//#endregion

        #region Public methods

        /// <summary>
        /// Updates the button to check for a button touch
        /// </summary>
        /// <param name="gamepad">the current mouse state</param>
        public void Update(TouchCollection touches)
        {
            this.touches = touches;
            //if (touches.Count == 1 && !isPressed && drawRectangle.Contains(touches[0].Position))
            //{
            //    isPressed = true;
            //    touchStarted = true;
            //}
            //else if (touches.Count == 0)
            //{
            //    isPressed = false;
            //}
            //if (touchStarted)
            //{
            //    Game1.ChangeState(clickState);
            //    touchStarted = false;
            //}
			if (touches.Count == 1 && !isPressed && drawRectangle.Contains(touches[0].Position))
            {
                isPressed = true;
                touchStarted = true;
            }
			if (touches.Count == 1 && touchStarted && !drawRectangle.Contains(touches[0].Position))
			{
				touchStarted = false;
			}
            if (touches.Count == 0)
            {
                isPressed = false;
            }
			if (touchStarted && touches.Count == 0)
            {
                Game1.ChangeState(clickState);
                touchStarted = false;
            }
            
        }

        ///// <summary>
        ///// Resets the button
        ///// </summary>
        //public void Reset()
        //{
        //    IsPressed = false;
        //    //IsEnabled = false;
        //}

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw()
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the button characteristics
        /// </summary>
        /// <param name="center">the center of the button</param>
        private void Initialize(Vector2 center)
        {
            // calculate button width
            buttonWidth = Game1.Resizing() * sprite.Width;
            buttonHeight = Game1.Resizing() * sprite.Height;
           
            // set initial draw and source rectangles
            drawRectangle = new Rectangle(
                (int)(center.X - buttonWidth / 2),
                (int)(center.Y - buttonHeight / 2),
                (int)buttonWidth, (int)buttonHeight);
            sourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }

        #endregion
    }
}