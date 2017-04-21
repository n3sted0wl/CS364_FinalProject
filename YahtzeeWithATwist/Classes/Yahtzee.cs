// Programmers: Paul Antonio
//              Seth Neds
//
// Date:        April 27, 2017
// File Name:   Yahtzee.cs

#region Development Notes and TODOs
// --------------------
#endregion

using System;
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

        public struct BonusGroup
        {
            public Dice.BonusFaces? bonusFace;
            public int              count;

            public BonusGroup(
                Dice.BonusFaces? initialBonusFace, 
                int              initialCount)
            {
                bonusFace = initialBonusFace;
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

        private static IEnumerable<BonusGroup> groupDiceByBonuses(List<Dice> diceToGroup)
        {
            IEnumerable<BonusGroup> groupedDice =
                from dice in diceToGroup
                group dice by dice.bonusFace into byBonusFace
                select new BonusGroup(byBonusFace.Key, byBonusFace.Count());

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
            int currentRunLength = 1;
            int previousFaceValue = 0;
            int currentFaceValue = 0;
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
                    currentRunLength += 1;
                    if (currentRunLength >= longestDetectedRun)
                        longestDetectedRun = currentRunLength;
                }
                else if (currentFaceValue - previousFaceValue == 0)
                {
                    if (currentRunLength >= longestDetectedRun)
                        longestDetectedRun = currentRunLength;
                }
                else
                {
                    currentRunLength = 1;
                }
                previousFaceValue = dice.faceValue;
            }
            #endregion

            return (longestDetectedRun - 1) >= targetStraightLength;
        }

        private static int getCategoryCount(bool hasStemen, BonusGroup currentBonusGroup)
        {
            #region Data
            int categoryCount;
            #endregion

            #region Logic
            categoryCount = currentBonusGroup.count;
            if (hasStemen) categoryCount += 1;
            #endregion

            return categoryCount;
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

        public static int calculateBonus(List<Dice> heldDice)
        {
            #region Data
            int              bonusScore    = 0;
            int              categoryCount = 0;
            bool             hasStemen     = false;
            List<BonusGroup> diceByBonus;
            BonusGroup       currentBonusGroup;
            #endregion

            #region Logic
            // Get the counts of each bonus grouping
            diceByBonus = (List<BonusGroup>) groupDiceByBonuses(heldDice);

            // Check for all five of a kind or one of each
            if (diceByBonus.Count() == 5) // One of each teacher
                bonusScore += 600;
            else if (diceByBonus.Any(x => x.count == 5)) // Five of a teacher
                bonusScore += 1000;

            // Apply dice bonuses (preserver order of operations)
            #region STEMEN BONUS: Team Player; increase each category count by one
            if (diceByBonus.Any(group => group.bonusFace == Dice.BonusFaces.Stemen))
                hasStemen = true;
            #endregion

            #region HOWELL BONUS: Risky; Lose or multiply total score
            if (diceByBonus.Any(group => group.bonusFace == Dice.BonusFaces.Howell))
            {
                currentBonusGroup =
                    diceByBonus.First(group => group.bonusFace == Dice.BonusFaces.Howell);

                categoryCount = getCategoryCount(hasStemen, currentBonusGroup);

                switch (categoryCount)
                {
                    case 1:
                        GameBoard.totalScore = 0;
                        break;
                    case 2:
                        GameBoard.totalScore *= 2;
                        break;
                    case 3:
                        GameBoard.totalScore *= 3;
                        break;
                    case 4:
                        GameBoard.totalScore *= 4;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region GEARY BONUS: guaranteed points
            if (diceByBonus.Any(group => group.bonusFace == Dice.BonusFaces.Geary))
            {
                currentBonusGroup =
                    diceByBonus.First(group => group.bonusFace == Dice.BonusFaces.Geary);

                categoryCount = getCategoryCount(hasStemen, currentBonusGroup);

                switch (categoryCount)
                {
                    case 1:
                        bonusScore += 20;
                        break;
                    case 2:
                        bonusScore += 50;
                        break;
                    case 3:
                        bonusScore += 100;
                        break;
                    case 4:
                        bonusScore += 200;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region HALSEY BONUS: Multiply category score
            if (diceByBonus.Any(group => group.bonusFace == Dice.BonusFaces.Halsey))
            {
                currentBonusGroup =
                    diceByBonus.First(group => group.bonusFace == Dice.BonusFaces.Halsey);

                categoryCount = getCategoryCount(hasStemen, currentBonusGroup);

                switch (categoryCount)
                {
                    case 1:
                        bonusScore *= 2;
                        break;
                    case 2:
                        bonusScore *= 3;
                        break;
                    case 3:
                        bonusScore *= 4;
                        break;
                    case 4:
                        bonusScore *= 5;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region SPARKS BONUS: "Caveat"; Multiply bonus category, lose one roll
            if (diceByBonus.Any(group => group.bonusFace == Dice.BonusFaces.Sparks))
            {
                currentBonusGroup =
                    diceByBonus.First(group => group.bonusFace == Dice.BonusFaces.Sparks);

                categoryCount = getCategoryCount(hasStemen, currentBonusGroup);

                switch (categoryCount)
                {
                    case 1:
                        bonusScore *= 2;
                        break;
                    case 2:
                        bonusScore *= 3;
                        break;
                    case 3:
                        bonusScore *= 4;
                        break;
                    case 4:
                        bonusScore *= 5;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            // End of bonus calculations
            #endregion

            return bonusScore;
        }
        #endregion
    }
}