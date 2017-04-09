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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

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
        private const string IMG_PATH_DICE_1 = "/Assets/Dice/Dice1.png";
        private const string IMG_PATH_DICE_2 = "/Assets/Dice/Dice2.png";
        private const string IMG_PATH_DICE_3 = "/Assets/Dice/Dice3.png";
        private const string IMG_PATH_DICE_4 = "/Assets/Dice/Dice4.png";
        private const string IMG_PATH_DICE_5 = "/Assets/Dice/Dice5.png";
        private const string IMG_PATH_DICE_6 = "/Assets/Dice/Dice6.png";
        private const int    MIN_FACE_VALUE = 1;
        private const int    MAX_FACE_VALUE = 6;

        public  DiceType type;
        public  Image    imageControl;

        private int          _faceValue;
        private Availability _availability;

        private static Random randomSeed = new Random();
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
        
        public string imagePath
        {
            get
            {
                return ImageLocations[this.faceValue];
            }
        }

        public Availability availability
        {
            get { return _availability; }
            set
            {
                _availability = value;
                if (availability == Availability.Available &&
                    this.imageControl != null)
                    imageControl.Visibility = Visibility.Visible;
                else if (availability == Availability.Unavailable &&
                    this.imageControl != null)
                    imageControl.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Structures
        // --------------------
        #endregion

        #region Enumerations
        // --------------------
        public enum DiceType     { Rollable, Held }
        public enum Availability { Available, Unavailable}
        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        private static Dictionary<int, string> ImageLocations =
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
            DiceType   initialType      = DiceType.Rollable,
            Image      initialImage     = null)
        {
            this.faceValue    = initialFaceValue;
            this.type         = initialType;
            this.imageControl = initialImage;
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
            diceDescription += $"Status:     { this.type.ToString() }";
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
            this.faceValue =
                randomSeed.Next(MIN_FACE_VALUE, MAX_FACE_VALUE + 1);
            return;
        }
        #endregion

        #region Other Methods
        // --------------------
        #endregion
        #endregion
    }
}
