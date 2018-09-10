using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
//using System.Math;


namespace PlanetParade
{
    /// <summary>
    /// A class for star
    /// </summary>
    class Star
    {
        #region Fields

        
        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;
        Rectangle sourceRectangle;
        Vector2 location;
		Color color1;

		bool starActive = true;

		//life support
		static Random rand = new Random();
		int lifeMilliseconds;
		int elapsedLifeMilliseconds = 0;
		int delayMilliseconds;
		int elapsedDelayMilliseconds = 0;
        
        #endregion

        #region Constructors


        /// <summary>
        /// Constructor
        /// </summary> 
		/// <param name="sprite">sprite for the star</param>       
        /// <param name="width">width of the screen</param>
		/// <param name="height">height of the screen</param>
		public Star(Texture2D sprite, int width, int height)
        {            
            
			this.sprite = sprite;
			lifeMilliseconds = rand.Next(3000, 6000);
			delayMilliseconds = rand.Next(0, 6000);
			location = new Vector2(rand.Next(0, width), rand.Next(0, height));
			if (rand.Next(0, 1) == 0)
			{
				starActive = false;
			}
			else 
			{
				starActive = true;
			}

            
        }
              

        #endregion

        #region Properties
       
                   
        /// <summary>
        /// Gets and sets whether a star is active or not
        /// </summary>
        public bool StarActive
        {
            get { return starActive; }
			set { starActive = value; }
        }

		/// <summary>
		/// Gets and sets life length of a star
		/// </summary>
		public int LifeMilliseconds
        {
            get { return lifeMilliseconds; }
            set { lifeMilliseconds = value; }
        }
        
		/// <summary>
        /// Gets and sets location of a star
        /// </summary>
		public Vector2 Location
        {
			get { return location; }
			set { location = value; }
        }

        
        #endregion

        #region Methods

		///Updates star (check for active/ not active)
		/// </summary>
		/// <param name="gameTime">game time</param>
		/// <param name="width">width of the screen</param>
		/// <param name="height">height of the screen</param>
		public void Update(GameTime gameTime, int width, int height)
		{
            
			if (StarActive)
			{
				elapsedDelayMilliseconds = 0;
				elapsedLifeMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
				if (elapsedLifeMilliseconds >= lifeMilliseconds)
				{
					StarActive = false;
				}
			}       
			if (!starActive)
			{
				elapsedLifeMilliseconds = 0;
                LifeMilliseconds = rand.Next (3000, 6000);
				delayMilliseconds = rand.Next(0, 6000);
                Location = new Vector2(rand.Next(0, width), rand.Next(0, height));
				elapsedDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
				if (elapsedDelayMilliseconds > delayMilliseconds)
				{
					StarActive = true;
				}
                
			}

            
		}

        /// <summary>
        /// Draws the star
        /// </summary>
        /// <param name="spritebatch">the sprite batch to use</param>                
        public void Draw(SpriteBatch spritebatch)
        {
			if (starActive)
			{
				spritebatch.Draw(sprite, drawRectangle, sourceRectangle, color1);
				color1 = Color.White*(float)(System.Math.Sin((float)elapsedLifeMilliseconds/lifeMilliseconds*System.Math.PI));
			}        
           
            // set draw rectangle and source rectangle
            drawRectangle = new Rectangle(
				(int)(location.X - sprite.Width / 2),
				(int)(location.Y - sprite.Height / 2),
				(int)sprite.Width,
                (int)sprite.Height);
            sourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }


        #endregion
    }
}
   