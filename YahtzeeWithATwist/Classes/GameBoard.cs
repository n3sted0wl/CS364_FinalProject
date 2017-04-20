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
using Windows.UI.Xaml.Controls;

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
        private static int _totalScore;
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

        public static int totalScore
        {
            get { return _totalScore; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Score can't be negative");

                _totalScore = value;
            }
        }

        public static List<Dice> ScoreableDice
        {
            get
            {
                _scoreableDice = new List<Dice>();
                foreach (KeyValuePair<int, Dice> dice in GameBoard.HeldDice)
                {
                    if (dice.Value.availability == Dice.Availability.Available)
                    {
                        _scoreableDice.Add(dice.Value);
                    }
                }
                return _scoreableDice;
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
        public  static Dictionary<int, Dice>             RollableDice;
        public  static Dictionary<int, Dice>             HeldDice;
        public  static Dictionary<GameMessages, string>  Messages;
        public  static Dictionary<string, ScoreCategory> ScoreCategories;
        private static List<Dice>                        _scoreableDice;
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
            RollableDice = new Dictionary<int, Dice>();
            HeldDice     = new Dictionary<int, Dice>();
            
            for (int diceCount = 1; 
                 diceCount <= NUMBER_OF_DICE; 
                 diceCount += 1)
            {
                RollableDice.Add(diceCount, new Dice());
                HeldDice.Add(diceCount, new Dice(
                    initialAvailability: Dice.Availability.Unavailable));
            }

            // Create all the score categories
            ScoreCategories = new Dictionary<string, ScoreCategory>()
            {
                { "Aces",   new ScoreCategory("Aces")},
                { "Twos",   new ScoreCategory("Twos")},
                { "Threes", new ScoreCategory("Threes")},
                { "Fours",  new ScoreCategory("Fours")},
                { "Fives",  new ScoreCategory("Fives")},
                { "Sixes",  new ScoreCategory("Sixes")},

                { "Full_House",      new ScoreCategory("Full House")},
                { "Four_of_a_Kind",  new ScoreCategory("Four of a Kind")},
                { "Three_of_a_Kind", new ScoreCategory("Three of a Kind")},
                { "Small_Straight",  new ScoreCategory("Small Straight")},
                { "Large_Straight",  new ScoreCategory("Large Straight")},
                { "Yahtzee",         new ScoreCategory("Yahtzee")},
                { "Chance",          new ScoreCategory("Chance")},
            };

            // Create all Messages
            Messages = new Dictionary<GameMessages, string>()
            {
                { GameMessages.NoMoreRolls,
                    "Cannot roll anymore this turn. Choose a Score Category." },
                { GameMessages.ConfirmScoreCategorySelection,
                    "Confirm your selection." }
            };
            
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
        public static void resetTotalScore() => totalScore = 0;
        #endregion

        #region Other Methods
        // --------------------
        public static Dice getDiceByImageControl(Image targetImageControl)
        {
            Dice foundDice = null;

            foreach (KeyValuePair<int, Dice> testDice in RollableDice)
            {
                if (testDice.Value.imageControl == targetImageControl)
                    foundDice = testDice.Value;
            }

            foreach (KeyValuePair<int, Dice> testDice in HeldDice)
            {
                if (testDice.Value.imageControl == targetImageControl)
                    foundDice = testDice.Value;
            }

            if (foundDice == null)
                throw new NullReferenceException("Could not find current dice");


            return foundDice;
        }

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
