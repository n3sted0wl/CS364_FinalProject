// Programmers: Paul Antonio
//              Seth Neds
//
// Date:        April 27, 2017
// File Name:   Dice.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWithATwist.Classes
{
    public class Dice
    {
        /*************************************************************/
        /*                       Documentation                       */
        /*************************************************************/
        #region Documentation
        // Programmer: Paul Antonio

        #region Overview
        // --------------------
        /// <summary>
        ///     This is a dice object that will be used by the
        ///     gameboard manager. Each dice has a face value and
        ///     an image associated with each face value. These are
        ///     stored in a static dictionary with the paths defined
        ///     in static constant fields.
        ///     Each dice is can be either rollable or held (enum).
        ///     Rollable dice can run the "roll" method. Otherwise, 
        ///     the method should fail and be handled.
        /// </summary>
        #endregion

        #region Exceptions
        // --------------------
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when trying to set the dice value outside the 
        ///     valid range of dice face values.
        /// </exception>
        #endregion

        #region Development
        // --------------------
        // TODO:
        #endregion
        #endregion

        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        // --------------------
        public enum DiceStatus { Rollable, Held }
        public      DiceStatus status;

        private const string IMG_PATH_DICE_1 = @"/Assets/Dice/dice_1.png";
        private const string IMG_PATH_DICE_2 = @"/Assets/Dice/dice_2.png";
        private const string IMG_PATH_DICE_3 = @"/Assets/Dice/dice_3.png";
        private const string IMG_PATH_DICE_4 = @"/Assets/Dice/dice_4.png";
        private const string IMG_PATH_DICE_5 = @"/Assets/Dice/dice_5.png";
        private const string IMG_PATH_DICE_6 = @"/Assets/Dice/dice_6.png";
        private const int    MIN_FACE_VALUE  = 1;
        private const int    MAX_FACE_VALUE  = 6;

        private int _faceValue;
        #endregion

        #region Properties
        // --------------------
        public int faceValue
        {
            get { return _faceValue; }
            set
            {
                if (value < MIN_FACE_VALUE ||
                    value > MAX_FACE_VALUE)
                {
                    throw new ArgumentOutOfRangeException(
                        paramName: "value");
                }

                _faceValue = value;
            }
        }
        #endregion

        #region Structures
        // --------------------
        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        public static Dictionary<int, string> ImageLocations =
            new Dictionary<int, string>()
            {
                { 1, IMG_PATH_DICE_1 },
                { 2, IMG_PATH_DICE_2 },
                { 3, IMG_PATH_DICE_3 },
                { 4, IMG_PATH_DICE_4 },
                { 5, IMG_PATH_DICE_5 },
                { 6, IMG_PATH_DICE_6 }
            };
        #endregion

        #region Delegates
        // --------------------
        #endregion
        #endregion

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Constructors
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
