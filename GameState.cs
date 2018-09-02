using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PlanetParade
{
    /// <summary>
    /// An enumeration of the possible game states
    /// </summary>
    public enum GameState
    {
        AddingUserPlanets,
        UpdatingCheckButton,
        CheckingUserPlanets,
        StartingNewGame,
        ShowingResults,
        ShowingRules,
        ShowingOptions,
        //Exiting
    }
}