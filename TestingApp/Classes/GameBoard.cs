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
        public  const int ROLLS_PER_TURN = 3;

        private static int _rollNumber;
        #endregion

        #region Properties
        // --------------------
        public static int rollNumber
        {
            get { return _rollNumber; }
            set
            {
                if (value < 1 || value > ROLLS_PER_TURN)
                    throw new ArgumentOutOfRangeException();

                _rollNumber = value;
            }
        }
        #endregion

        #region Structures
        // --------------------
        #endregion

        #region Enumerations
        // --------------------
        public enum GameMessages
        {
            NoMoreRolls,
            ConfirmScoreCategorySelection,
        }
        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        // Dice are not zero-indexed
        public static Dictionary<int, Dice> RollableDice;
        public static Dictionary<int, Dice> HeldDice;
        public static Dictionary<GameMessages, string> Messages =
            new Dictionary<GameMessages, string>()
            {
                { GameMessages.NoMoreRolls,
                    "Cannot roll anymore this turn. Choose a Score Category." },
                { GameMessages.ConfirmScoreCategorySelection,
                    "Confirm your selection." }
            };
        public static List<Dice> ScoreableDice;
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
            RollableDice = new Dictionary<int, Dice>();
            HeldDice     = new Dictionary<int, Dice>();
            
            // Create all the dice
            for (int diceCount = 1; 
                 diceCount <= NUMBER_OF_DICE; 
                 diceCount += 1)
            {
                RollableDice.Add(diceCount, new Dice());
                HeldDice.Add(diceCount, new Dice(
                    initialAvailability: Dice.Availability.Unavailable));
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
        public static int getDiceIndex(Dice targetDice)
        {
            int index = 0;

            if (RollableDice.Values.Contains(targetDice))
            {
                foreach (KeyValuePair<int, Dice> entry in RollableDice)
                {
                    if (entry.Value == targetDice)
                        index = entry.Key;
                }
            }
            else if (HeldDice.Values.Contains(targetDice))
            {
                foreach (KeyValuePair<int, Dice> entry in HeldDice)
                {
                    if (entry.Value == targetDice)
                        index = entry.Key;
                }
            }

            if (index == 0)
                throw new InvalidOperationException("Target dice not found");

            return index;
        }
        #endregion
        #endregion
    }
}
