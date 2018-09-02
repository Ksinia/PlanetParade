using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PlanetParade
{
    /// <summary>
    /// All the constants used in the game
    /// </summary>
    class GameConstants
    {
        // resolution
        public const int WindowWidth = 800;
        //public const int WindowHeight = 480;
        public const int WindowHeight = 600;

        // quantity of game attampts (from 0 to MaxAttempts)
        public const int MaxAttempts = 8;

        // offsets
        public const int HorizontalPlanetsOffset = VerticalPlanetsOffset;
        public const int VerticalPlanetsOffset = WindowHeight / 10;
    }
}