using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace PlanetParade
{
    /// <summary>
    /// A class for empty place for planet
    /// </summary>
    class EmptyPlanetPlace
    {
        #region Fields

        //PlanetType type;

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;

        #endregion

        #region Constructors


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sprite">sprite for empty planet place</param>
        /// <param name="location">location of the center</param>
        public EmptyPlanetPlace(Texture2D sprite, Vector2 location)
        {
            this.sprite = sprite;
            // build draw rectangle
            drawRectangle = new Rectangle(
                (int)location.X - sprite.Width / 2,
                (int)location.Y - sprite.Height / 2,
                sprite.Width,
                sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the draw rectangle for the planet
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the planet
        /// </summary>
        /// <param name="spritebatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion
    }
}