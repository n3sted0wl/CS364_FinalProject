// Programmers: Paul Antonio
//              Seth Neds
//
// Date:        April 27, 2017
// File Name:   Dice.cs

#region Development Notes and TODOs
// --------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWithATwist.Classes
{
    #region IntelliSense Documentation
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when trying to set the dice value outside the 
    ///     valid range of dice face values.
    /// </exception>
    #endregion
    public class Dice
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        // --------------------
        public DiceStatus status;

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

        #region Enumerations
        // --------------------
        public enum DiceStatus { Rollable, Held }
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
        /// <summary>
        ///     Constructor for a dice object
        /// </summary>
        /// 
        /// <param name="initialFaceValue">
        ///     Optional face value of the new dice
        /// </param>
        ///
        /// <param name="initialStatus">
        ///     Optional status of the new dice of type 
        ///     "enum DiceStatus"
        /// </param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when trying to set the dice value outside the 
        ///     valid range of dice face values.
        /// </exception>
        /// 
        /// <returns>
        ///     Nothing. This is a constructor.
        /// </returns>
        public Dice(
            int        initialFaceValue = MIN_FACE_VALUE, 
            DiceStatus initialStatus    = DiceStatus.Rollable)
        {
            this.faceValue = initialFaceValue;
            this.status    = initialStatus;
        }
        #endregion

        #region Overrides
        // --------------------
        /// <summary>
        ///     Overriden ToString method
        /// </summary>
        /// 
        /// <returns>
        ///     The face value of the dice and it's current status
        ///     in a two-line string
        /// </returns>
        public override string ToString()
        {
            #region Data
            string diceDescription = string.Empty;
            #endregion

            #region Logic
            diceDescription += $"Face Value: { this.faceValue.ToString() }\n";
            diceDescription += $"Status:     { this.status.ToString() }";
            #endregion

            return diceDescription;
        }
        #endregion

        #region Accessors
        // --------------------
        #endregion

        #region Mutators
        // --------------------
        /// <summary>
        ///     Rolls this dice. The faceValue property will be set
        ///     to a random value.
        /// </summary>
        /// 
        /// <returns>
        ///     void
        /// </returns>
        public void roll()
        {
            Random randomNumber = new Random();
            this.faceValue = 
                randomNumber.Next(MIN_FACE_VALUE, MAX_FACE_VALUE + 1);
            return;
        }
        #endregion

        #region Other Methods
        // --------------------
        #endregion
        #endregion
    }
}
