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

        public static int calculateBonus(List<Dice> heldDice, int baseScore = 0)
        {
            #region Data
            IEnumerable<BonusGroup> diceByBonus;
            int                     bonusScore        = 0;
            int                     modifiedBaseScore = baseScore;
            bool                    lostBonus         = false;
            #endregion

            #region Logic
            // Get the counts of each bonus type in the hand
            diceByBonus = groupDiceByBonuses(heldDice);

            if (diceByBonus.Count() == 5) // One of each teacher
            {
                bonusScore += 600;
            }
            else if (diceByBonus.Any(x => x.count == 5)) // Five of a teacher
            {
                bonusScore += 1000;
            }
            else
            {
                foreach (BonusGroup bonusCategory in diceByBonus)
                {
                    switch (bonusCategory.bonusFace)
                    {
                        case Dice.BonusFaces.Geary: // Double base score for each
                            #region Geary Bonus
                            for (int bonus = 1; bonus <= bonusCategory.count; bonus += 1)
                                modifiedBaseScore *= 2;
                            #endregion
                            break;
                        case Dice.BonusFaces.Halsey: // Flat 10 point bonus
                            #region Halsey Bonus
                            bonusScore += 10;
                            #endregion
                            break;
                        case Dice.BonusFaces.Howell: // Base score multiplier
                            #region Howell Bonus
                            switch (bonusCategory.count)
                            {
                                case 1:
                                    lostBonus = true;
                                    break;
                                case 2:
                                    modifiedBaseScore *= 4;
                                    break;
                                case 3:
                                    modifiedBaseScore *= 10;
                                    break;
                                default:
                                    modifiedBaseScore *= 50;
                                    break;
                            }
                            #endregion
                            break;
                        case Dice.BonusFaces.Sparks:
                            #region Sparks Bonus
                            
                            #endregion
                            break;
                        case Dice.BonusFaces.Stemen:
                            #region Stemen Bonus
                            if (diceByBonus.Any(dice => 
                                dice.bonusFace == Dice.BonusFaces.Geary))
                            {
                                bonusScore += 50;
                            }
                            if (diceByBonus.Any(dice => 
                                dice.bonusFace == Dice.BonusFaces.Halsey))
                            {
                                bonusScore += 100;
                            }
                            if (diceByBonus.Any(dice => 
                                dice.bonusFace == Dice.BonusFaces.Howell))
                            {
                                // Negate the negative effects of Howell
                                lostBonus   = false;
                                bonusScore += 150;
                            }
                            if (diceByBonus.Any(dice => 
                                dice.bonusFace == Dice.BonusFaces.Sparks))
                            {

                            }
                            #endregion
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion

            if (lostBonus)
            {
                bonusScore = 0;
            }
            else
            {
                if (modifiedBaseScore > baseScore)
                    bonusScore += (modifiedBaseScore - baseScore);
            }

            return bonusScore;
        }
        #endregion
    }
}
