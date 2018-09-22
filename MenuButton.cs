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
		Texture2D spriteYes;
		Texture2D spriteNo;
        float buttonWidth;
        float buttonHeight;

        // fields for drawing
        Rectangle drawRectangle;
        Rectangle sourceRectangle;

        // touch processing
        GameState clickState;
        bool screenIsTouched = false;
        bool buttonTouchStarted = false;
        
		// whether button is option button
		bool isOptionButton;

        // type of option
        OptionsList typeOfOption;
		// option value
        bool option = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of menu button
        /// </summary>
        /// <param name="sprite">the sprite for the button</param>
        /// <param name="center">the center of the button</param>
        /// <param name="clickState">the game state to change to when the button is clicked</param>
        public MenuButton(Texture2D sprite, Vector2 center, GameState clickState)
        {
			isOptionButton = false;
			this.sprite = sprite;
            this.clickState = clickState;
            Initialize(center);

        }    
		/// <summary>
        /// Constructor of option button
        /// </summary>
        /// <param name="spriteYes">the sprite for the button, when option is set to yes</param>
		/// <param name="spriteNo">the sprite for the button, when option is set to no</param>
        /// <param name="center">the center of the button</param>        
		public MenuButton(Texture2D spriteYes, Texture2D spriteNo, Vector2 center, OptionsList typeOfOption)
        {
			isOptionButton = true;
			this.spriteYes = spriteYes;
			this.spriteNo = spriteNo;
            this.typeOfOption = typeOfOption;
            Initialize(center);

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collision rectangle for button
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        #endregion 


        #region Public methods

        /// <summary>
        /// Updates the button to check for a button touch
        /// </summary>
        /// <param name="touches">the current touch screen state</param>
        public void Update(TouchCollection touches)
        {
            if (touches.Count == 1 && !screenIsTouched && drawRectangle.Contains(touches[0].Position))
            {
                screenIsTouched = true;
                buttonTouchStarted = true;
			}
			if (touches.Count == 1 && !drawRectangle.Contains(touches[0].Position))
			{
				buttonTouchStarted = false;
				screenIsTouched = true;
			}
            if (touches.Count == 0)
            {
                screenIsTouched = false;
            }
			if (buttonTouchStarted && !screenIsTouched)
            {
				if (!isOptionButton)
				{
					Game1.ChangeState(clickState);
				}
				else
				{
					if (option) {option = false; }
					else { option = true; }
                    Game1.ChangeOption(typeOfOption, option);
				}

                buttonTouchStarted = false;

            }
            
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
			if (!isOptionButton)
			{
				spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.DarkGray);
			}
			else
			{
				if (option)
				{
					spriteBatch.Draw(spriteYes, drawRectangle, sourceRectangle, Color.White);
				}
				else
				{
					spriteBatch.Draw(spriteNo, drawRectangle, sourceRectangle, Color.White);
				}
			}
            
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
			if (!isOptionButton)
			{
				buttonWidth = Game1.Resizing() * sprite.Width;
                buttonHeight = Game1.Resizing() * sprite.Height;
			}
			else
			{
				buttonWidth = Game1.Resizing() * spriteYes.Width;
                buttonHeight = Game1.Resizing() * spriteYes.Height;
			}
           
            // set initial draw and source rectangles
            drawRectangle = new Rectangle(
                (int)(center.X - buttonWidth / 2),
                (int)(center.Y - buttonHeight / 2),
                (int)buttonWidth, (int)buttonHeight);
			if (!isOptionButton)
			{
				sourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
			}
			else
			{
				sourceRectangle = new Rectangle(0, 0, spriteYes.Width, spriteYes.Height);
			}
        }

        #endregion
    }
}