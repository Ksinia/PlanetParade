using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace PlanetParade
{
    class GameMessage
    {
        #region Fields

        string text;
        SpriteFont font;
        Vector2 center;
        Vector2 position;
        Color color;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">the text for the message</param>
        /// <param name="font">the sprite font for the message</param>
        /// <param name="center">the center of the message</param>
        /// <param name="color">the color of the message</param>
        public GameMessage(string text, SpriteFont font, 
		                   Vector2 center, Color color)
        {
            this.text = text;
            this.font = font;
            this.center = center;
            this.color = color;
            
            // calculate position from text and center
            float textWidth = font.MeasureString(text).X;
            float textHeight = font.MeasureString(text).Y;
			position = new Vector2(center.X - textWidth / 2,
                center.Y - textHeight / 2);			
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sets the text for the message
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;

                // changing text could change text location
                float textWidth = font.MeasureString(text).X;
                float textHeight = font.MeasureString(text).Y;
                position.X = center.X - textWidth / 2;
                position.Y = center.Y - textHeight / 2;
            }
        }
        
        public Color Color
        {
            set { color = value; }
        }

		public Vector2 Position
        {
            set { position = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the message
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
			spriteBatch.DrawString(font, Text, position, color); 
        }

        
		public void DrawMultiline(SpriteBatch spriteBatch, int width)
        {
			spriteBatch.DrawString(font, parseText(Text, width), position, color);
        }

		private String parseText(String text, int width)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
				if (font.MeasureString(line + word).Length() > width)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }
            return returnString + line;
        }

		#endregion
        
    }
}