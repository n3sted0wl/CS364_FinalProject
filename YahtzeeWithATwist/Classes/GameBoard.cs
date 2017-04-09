// Programmers: Paul Antonio
//              Seth Neds
//
// Date:        April 27, 2017
// File Name:   GameBoard.cs

#region Development Notes and TODOs
// --------------------
// TODO: Remove unnecessary using statements
// TODO: Remove unnecessary documentation
// TODO: Fill out Exception documentation
//
// I haven't come up with a design for this yet
// All I know is that it is going to contain 
//   universal data like total points and turn
//   counter.
// It will also be where I define the functions
//   that calculate the points of a given hand
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWithATwist.Classes
{
    /// <exception cref="">
    /// </exception>
    public static class GameBoard
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        // --------------------
        private const int NUMBER_OF_DICE = 5;
        #endregion

        #region Properties
        // --------------------
        #endregion

        #region Structures
        // --------------------
        #endregion

        #region Enumerations
        // --------------------
        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        // Dice are not zero-indexed
        public static Dictionary<int, Dice> RollableDice =
            new Dictionary<int, Dice>();
        public static Dictionary<int, Dice> HeldDice =
            new Dictionary<int, Dice>();              
        #endregion

        #region Delegates
        // --------------------
        #endregion
        #endregion

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Initializer
        // --------------------
        public static void initialize()
        {
            // Create all the dice
            for (int diceCount = 1; diceCount <= NUMBER_OF_DICE; diceCount += 1)
            {
                RollableDice.Add(diceCount, new Dice());
                HeldDice.Add(diceCount, new Dice());
            }
            return;
        }
        #endregion

        #region Overrides
        // --------------------
        #endregion

        #region Accessors
        // --------------------
        #endregion

        #region Mutators
        // --------------------
        #endregion

        #region Other Methods
        // --------------------
        #endregion
        #endregion
    }
}
