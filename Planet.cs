using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlanetParade
{
    /// <summary>
    /// A class for planet
    /// </summary>
    class Planet
    {
        #region Fields

        PlanetType type;

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;
        Rectangle sourceRectangle;
        Vector2 location;
        float planetWidth;
        float planetHeight;
        bool big;

        // checking support
        bool planetChecked = false;

		// selecting support
		bool planetSelected = false;
		bool touched = false;
        
        #endregion

        #region Constructors


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">type of a planet</param>
        /// <param name="sprite">sprite</param>
        /// <param name="location">location of the center</param>
        public Planet(PlanetType type, Texture2D sprite, Vector2 location)
        {
            this.type = type;
            this.sprite = sprite;
            this.location = location;            
        }

        /// <summary>
        /// Constructor of a random planet
        /// </summary>
        public Planet(Random rand, Vector2 location)
        {
            this.location = location;
            int planetType = rand.Next(0, 9);
            List<PlanetType>planetTypeList = Enum.GetValues(typeof(PlanetType)).Cast<PlanetType>().ToList();
            type = planetTypeList[planetType];
            sprite = Game1.GetPlanetSprite(type);
           
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of the planet
        /// </summary>
        public PlanetType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the collision rectangle for the planet
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets and sets whether a planet is checked or not
        /// </summary>
        public bool PlanetChecked
        {
            get { return planetChecked; }
            set { planetChecked = value; }
        }

        /// <summary>
        /// Gets and sets the width of the planet
        /// </summary>
        public float PlanetWidth
        {
            get { return planetWidth; }
            set { planetWidth = value; }
        }

        /// <summary>
        /// Gets and sets the height of the planet
        /// </summary>
        public float PlanetHeight
        {
            get { return planetHeight; }
            set { planetHeight = value; }
        }

		/// <summary>
		/// Gets and sets whether a planet is touched
        /// </summary>
		public bool PlanetTouched
		{
			get { return touched; }
			set { touched = value; }
		}

		/// <summary>
        /// Gets and sets whether a planet is selected
        /// </summary>
        public bool PlanetSelected
        {
            get { return planetSelected; }
            set { planetSelected = value; }
        }

        
        #endregion

        #region Methods

        /// <summary>
        /// Draws the planet
        /// </summary>
        /// <param name="spritebatch">the sprite batch to use</param>
        /// <param name="color">color of the planet (for shadowing)</param>
        /// <param name="big">whether the planet is big or not</param>
        public void Draw(SpriteBatch spritebatch, Color color, bool big)
        {
            this.big = big;
            spritebatch.Draw(sprite, drawRectangle, sourceRectangle, color);
            // define planet size
            if (big)
            {
                // for initial planets
                planetWidth = Game1.Resizing() * sprite.Width;
                planetHeight = Game1.Resizing() * sprite.Height;
            }
            else
            {
                // for all planets in attempts and results
                planetWidth = Game1.Resizing() * sprite.Width * 3 / 5;
                planetHeight = Game1.Resizing() * sprite.Height * 3 / 5;
            }
            // set draw rectangle and source rectangle
            drawRectangle = new Rectangle(
                (int)(location.X - planetWidth / 2),
                (int)(location.Y - planetHeight / 2),
                (int)planetWidth,
                (int)planetHeight);
            sourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
        }

       



        #endregion
    }
}
   