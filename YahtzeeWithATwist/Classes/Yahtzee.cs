// Programmers: Paul Antonio
//              Seth Neds
//
// Date:        April 27, 2017
// File Name:   Yahtzee.cs

#region Development Notes and TODOs
// --------------------
#endregion

using System.Collections.Generic;
using System.Linq;

namespace YahtzeeWithATwist.Classes
{
    public static class Yahtzee
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        // --------------------
        private static IEnumerable<DiceGroup> diceCountsByValue;

        private const int FULL_HOUSE_SCORE_VALUE  = 25;
        private const int SM_STRAIGHT_SCORE_VALUE = 30;
        private const int LG_STRAIGHT_SCORE_VALUE = 40;
        private const int YAHTZEE_SCORE_VALUE     = 50;

        private const int SM_STRAIGHT_SIZE = 4;
        private const int LG_STRAIGHT_SIZE = 5;
        #endregion
        
        #region Structures
        // --------------------
        public struct DiceGroup
        {
            public int faceValue;
            public int count;

            public DiceGroup(int initialFaceValue, int initialCount)
            {
                faceValue = initialFaceValue;
                count = initialCount;
            }
        }
        #endregion
        #endregion

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Other Methods
        // --------------------
        private static IEnumerable<DiceGroup> groupDiceByValue(List<Dice> diceToGroup)
        {
            IEnumerable<DiceGroup> groupedDice =
                from dice in diceToGroup
                group dice by dice.faceValue into byValue
                select new DiceGroup(byValue.Key, byValue.Count());

            return groupedDice;
        }

        private static int getSumOfDice(IEnumerable<DiceGroup> diceToSum)
        {
            #region Data
            int score = 0;
            #endregion

            #region Logic
            foreach (var dice in diceToSum)
            {
                score += (dice.faceValue * dice.count);
            }
            #endregion

            return score;
        }

        private static bool hasStraight(List<Dice> diceToScan, int targetStraightLength)
        {
            #region Data
            int longestDetectedRun = 1;
            int currentRunLength   = 1;
            int previousFaceValue  = 0;
            int currentFaceValue   = 0;
            List<Dice> orderedDice;
            #endregion

            #region Logic
            // Order the list
            orderedDice = diceToScan.OrderBy(dice => dice.faceValue).ToList();

            // Loop through looking for gaps in the list
            foreach (Dice dice in orderedDice)
            {
                currentFaceValue = dice.faceValue;
                if (currentFaceValue - previousFaceValue == 1)
                {
                    currentRunLength   += 1;
                    if (currentRunLength > longestDetectedRun)
                        longestDetectedRun = currentRunLength;
                }
                else if (currentFaceValue - previousFaceValue == 0)
                {
                    // IDK if I need this...
                }
                else
                {
                    currentRunLength = 1;
                }
                previousFaceValue = dice.faceValue;
            }
            #endregion

            return longestDetectedRun >= targetStraightLength;
        }

        public static int calculateAces(List<Dice> heldDice)
        {
            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            foreach (DiceGroup record in diceCountsByValue)
            {
                if (record.faceValue == 1)
                    score += (record.faceValue * record.count);
            }
            #endregion

            return score;
        }

        public static int calculateTwos(List<Dice> heldDice)
        {

            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            foreach (DiceGroup record in diceCountsByValue)
            {
                if (record.faceValue == 2)
                    score += (record.faceValue * record.count);
            }
            #endregion

            return score;
        }

        public static int calculateThrees(List<Dice> heldDice)
        {

            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            foreach (DiceGroup record in diceCountsByValue)
            {
                if (record.faceValue == 3)
                    score += (record.faceValue * record.count);
            }
            #endregion

            return score;
        }

        public static int calculateFours(List<Dice> heldDice)
        {

            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            foreach (DiceGroup record in diceCountsByValue)
            {
                if (record.faceValue == 4)
                    score += (record.faceValue * record.count);
            }
            #endregion

            return score;
        }

        public static int calculateFives(List<Dice> heldDice)
        {

            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            foreach (DiceGroup record in diceCountsByValue)
            {
                if (record.faceValue == 5)
                    score += (record.faceValue * record.count);
            }
            #endregion

            return score;
        }

        public static int calculateSixes(List<Dice> heldDice)
        {

            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            foreach (DiceGroup record in diceCountsByValue)
            {
                if (record.faceValue == 6)
                    score += (record.faceValue * record.count);
            }
            #endregion

            return score;
        }

        public static int calculateFullHouse(List<Dice> heldDice)
        {
            #region Data
            int score;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);

            if (diceCountsByValue.Any(record => record.count == 3) &&
                diceCountsByValue.Any(record => record.count == 2))
            {
                score = FULL_HOUSE_SCORE_VALUE;
            }
            else
            {
                score = 0;
            }
            #endregion

            return score;
        }

        public static int calculateFourOfAKind(List<Dice> heldDice)
        {
            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);
            if (diceCountsByValue.Any(record => record.count >= 4))
            {
                score = getSumOfDice(diceCountsByValue);
            }            
            #endregion

            return score;
        }

        public static int calculateThreeOfAKind(List<Dice> heldDice)
        {
            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);
            if (diceCountsByValue.Any(record => record.count >= 3))
            {
                score = getSumOfDice(diceCountsByValue);
            }
            #endregion

            return score;
        }

        public static int calculateSmallStraight(List<Dice> heldDice)
        {
            #region Data
            bool hasSmStraight = hasStraight(heldDice, SM_STRAIGHT_SIZE);
            #endregion

            return hasSmStraight ? SM_STRAIGHT_SCORE_VALUE : 0;
        }

        public static int calculateLargeStraight(List<Dice> heldDice)
        {
            #region Data
            bool hasSmStraight = hasStraight(heldDice, LG_STRAIGHT_SIZE);
            #endregion
            
            return hasSmStraight ? LG_STRAIGHT_SCORE_VALUE : 0;
        }

        public static int calculateYahtzee(List<Dice> heldDice)
        {
            #region Data
            int score = 0;
            #endregion

            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);
            if (diceCountsByValue.Any(record => record.count >= 5))
            {
                score = YAHTZEE_SCORE_VALUE;
            }
            #endregion

            return score;
        }

        public static int calculateChance(List<Dice> heldDice)
        {
            #region Logic
            diceCountsByValue = groupDiceByValue(heldDice);
            #endregion

            return getSumOfDice(diceCountsByValue);
        }
        #endregion
    }
}
