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
        private const int NUMBER_OF_DICE   = 5;
        public  const int ROLLS_PER_TURN   = 3;

        private static int _rollsRemaining = ROLLS_PER_TURN;
        private static int _totalScore     = 0;

        public static TextBlock totalScoreTextBlock;
        public static TextBlock bonusScoreTextBlock;
        #endregion

        #region Properties
        // --------------------
        public static int rollsRemaining
        {
            get { return _rollsRemaining; }
            set
            {
                if (value < 0 || value > ROLLS_PER_TURN)
                    throw new ArgumentOutOfRangeException();

                _rollsRemaining = value;
            }
        }

        public static int totalScore
        {
            get { return _totalScore; }

            set
            {
                _totalScore = value;
                if (totalScoreTextBlock != null)
                {
                    totalScoreTextBlock.Text = totalScore.ToString();
                }

            }
        }

        public static List<Dice> ScoreableDice
        {
            get
            {
                _scoreableDice = new List<Dice>();
                foreach (Dice dice in GameBoard.HeldDice.Values)
                {
                    if (dice.availability == Dice.Availability.Available)
                    {
                        _scoreableDice.Add(dice);
                    }
                }

                foreach (Dice dice in GameBoard.RollableDice.Values)
                {
                    if (dice.availability == Dice.Availability.Available)
                    {
                        _scoreableDice.Add(dice);
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

        public enum Categories
        {
            Aces,
            Twos,
            Threes,
            Fours,
            Fives,
            Sixes,
            Full_House,
            Four_of_a_Kind,
            Three_of_a_Kind,
            Small_Straight,
            Large_Straight,
            Yahtzee,
            Chance
        }

        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        // Dice are not zero-indexed
        public  static Dictionary<int, Dice>                 RollableDice;
        public  static Dictionary<int, Dice>                 HeldDice;
        public  static Dictionary<GameMessages, string>      Messages;
        public  static Dictionary<Categories, ScoreCategory> ScoreCategories;
        private static List<Dice>                            _scoreableDice;
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
            ScoreCategories = new Dictionary<Categories, ScoreCategory>()
            {
                { Categories.Aces,   new ScoreCategory("Aces")},
                { Categories.Twos,   new ScoreCategory("Twos")},
                { Categories.Threes, new ScoreCategory("Threes")},
                { Categories.Fours,  new ScoreCategory("Fours")},
                { Categories.Fives,  new ScoreCategory("Fives")},
                { Categories.Sixes,  new ScoreCategory("Sixes")},

                { Categories.Full_House,      new ScoreCategory("Full House")},
                { Categories.Four_of_a_Kind,  new ScoreCategory("Four of a Kind")},
                { Categories.Three_of_a_Kind, new ScoreCategory("Three of a Kind")},
                { Categories.Small_Straight,  new ScoreCategory("Small Straight")},
                { Categories.Large_Straight,  new ScoreCategory("Large Straight")},
                { Categories.Yahtzee,         new ScoreCategory("Yahtzee")},
                { Categories.Chance,          new ScoreCategory("Chance")},
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

        #region Mutators
        // --------------------
        public static void resetTotalScore() => totalScore = 0;
        #endregion

        #region Other Methods
        // --------------------
        public static ScoreCategory getScoreCategoryByTextBlockControl(TextBlock targetTextBlock)
        {
            #region Data
            ScoreCategory foundScoreCategory = null;
            #endregion

            #region Logic
            foreach (ScoreCategory category in ScoreCategories.Values)
            {
                if (targetTextBlock == category.descriptionTextBlock)
                {
                    foundScoreCategory = category;
                    break;
                }

            }
            #endregion

            return foundScoreCategory;
        }

        public static Dice getDiceByImageControl(Image targetImageControl)
        {
            Dice foundDice = null;

            foreach (Dice dice in RollableDice.Values)
            {
                if (dice.imageControl == targetImageControl)
                    foundDice = dice;
            }

            foreach (Dice dice in HeldDice.Values)
            {
                if (dice.imageControl == targetImageControl)
                    foundDice = dice;
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

        public static void resetDice()
        {
            // Put all the dice back into the roll section
            for (int diceIndex = 1; diceIndex <= NUMBER_OF_DICE; diceIndex += 1)
            {
                RollableDice[diceIndex].availability = Dice.Availability.Available;
                HeldDice[diceIndex].availability     = Dice.Availability.Unavailable;
            }

            return;
        }

        public static void calculateAllScores()
        {
            #region Logic
            foreach (ScoreCategory category in GameBoard.ScoreCategories.Values)
            {
                category.scoreValue = category.CalculateValue(GameBoard.ScoreableDice);
            }
            #endregion

            return;
        }

        public static void resetScoreCategories()
        {
            foreach (Categories category in ScoreCategories.Keys)
            {
                ScoreCategories[category].status = ScoreCategory.Status.Unused;
            }

            return;
        }

        public static bool isGameOver()
        {
            bool isGameOver = true;

            foreach (ScoreCategory category in ScoreCategories.Values)
            {
                if (category.status == ScoreCategory.Status.Unused)
                {
                    isGameOver = false;
                    break;
                }
            }

            return isGameOver;
        }

        public static void hideAllDice()
        {
            foreach (Dice dice in HeldDice.Values)
            {
                dice.availability = Dice.Availability.Unavailable;
            }

            foreach (Dice dice in RollableDice.Values)
            {
                dice.availability = Dice.Availability.Unavailable;
            }
        }
        #endregion
        #endregion
    }
}
