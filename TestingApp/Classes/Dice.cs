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

        private const string IMG_BONUS_GEARY  = "/Assets/Dice/Dice1_Geary.png";
        private const string IMG_BONUS_HALSEY = "/Assets/Dice/Dice1_Halsey.png";
        private const string IMG_BONUS_SPARKS = "/Assets/Dice/Dice1_Sparks.png";
        private const string IMG_BONUS_STEMEN = "/Assets/Dice/Dice1_Stemen.png";
        private const string IMG_BONUS_HOWELL = "/Assets/Dice/Dice1_Howell.png";

        private const int    MIN_FACE_VALUE   = 1;
        private const int    MAX_FACE_VALUE   = 6;

        public  DiceType     type;
        public Image         _imageControl;

        private int          _faceValue;
        private Availability _availability;
        private BonusFaces?  _bonusFace;

        private static UpdateImageSource _updateImage;

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

                if (faceValue != 1)
                {
                    this.bonusFace = null;
                }
                else
                {
                    // Depending on probabilities, set a bonus face
                    int probability = randomSeed.Next(1, 20);
                    if (probability < 2)
                        this.bonusFace = BonusFaces.Howell;
                    else if (probability < 5)
                        this.bonusFace = BonusFaces.Sparks;
                    else if (probability < 7)
                        this.bonusFace = BonusFaces.Geary;
                    else if (probability < 10)
                        this.bonusFace = BonusFaces.Halsey;
                    else if (probability < 12)
                        this.bonusFace = BonusFaces.Stemen;
                    else
                        this.bonusFace = null;
                }

                if (imageControl != null)
                {
                    updateImageDelegate?.Invoke(this.imageControl, this.imagePath);
                }
            }
        }
        
        /// <summary>
        ///     get: standard
        ///     set: if not set to null, update the image path
        /// </summary>
        public Image imageControl
        {
            get { return _imageControl; }
            set
            {
                this._imageControl = value;
                if (imageControl != null)
                {
                    updateImageDelegate?.Invoke(this.imageControl, this.imagePath);
                }
            }
        } 

        /// <summary>
        ///     Set: not defined
        ///     Get: if a bonusFace is assigned, use the bonus image locations dictionary
        ///          otherwise, use the default image path collection
        /// </summary>
        public string imagePath
        {
            get
            {
                string imageLocation;

                if (this.bonusFace != null && this.faceValue == 1)
                {
                    imageLocation = BonusImageLocations[bonusFace];
                }
                else
                {
                    imageLocation = ImageLocations[this.faceValue];
                }

                return imageLocation;
            }
        } 

        /// <summary>
        ///     get: standard
        ///     set: update the visibility of the image control
        /// </summary>
        public Availability availability
        {
            get { return _availability; }
            set
            {
                _availability = value;

                if (this.availability == Availability.Available &&
                    this.imageControl != null)
                    imageControl.Visibility = Visibility.Visible;
                else if (this.availability == Availability.Unavailable &&
                    this.imageControl != null)
                    imageControl.Visibility = Visibility.Collapsed;
            }
        } 

        /// <summary>
        ///     Get: standard
        ///     Set: Check if the faceValue == 1 (probably should not)
        /// </summary>
        public BonusFaces? bonusFace
        {
            get { return this._bonusFace; }

            set
            {
                if (this.faceValue == 1)
                {
                    this._bonusFace = value;
                    updateImageDelegate?.Invoke(this.imageControl, this.imagePath);
                }
                else
                {
                    this._bonusFace = null;
                }
            }
        } 

        /// <summary>
        ///     Get: standard
        ///     Set: Limit to 1 delegated method only at any time
        /// </summary>
        public static UpdateImageSource updateImageDelegate
        {
            get { return _updateImage; }
            set
            {
                _updateImage = null;
                _updateImage += value;
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
        public enum BonusFaces   { Geary, Halsey, Sparks, Stemen, Howell }
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

        private static Dictionary<BonusFaces?, string> BonusImageLocations =
            new Dictionary<BonusFaces?, string>()
            {
                { BonusFaces.Geary,  IMG_BONUS_GEARY },
                { BonusFaces.Halsey, IMG_BONUS_HALSEY },
                { BonusFaces.Howell, IMG_BONUS_HOWELL },
                { BonusFaces.Sparks, IMG_BONUS_SPARKS },
                { BonusFaces.Stemen, IMG_BONUS_STEMEN }
            };
        #endregion

        #region Delegates
        // --------------------
        public delegate void UpdateImageSource (Image image, string path);
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
            int               initialFaceValue     = MIN_FACE_VALUE,
            DiceType          initialType          = DiceType.Rollable,
            Image             initialImage         = null,
            Availability      initialAvailability  = Availability.Available,
            UpdateImageSource initialImageModifier = null)
        {
            this.faceValue      = initialFaceValue;
            this.type           = initialType;
            this.imageControl   = initialImage;
            this.availability   = initialAvailability;
            updateImageDelegate = initialImageModifier;
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
            // Set the face value
            if (this.availability == Availability.Available)
            {
                this.faceValue = randomSeed.Next(
                    MIN_FACE_VALUE,
                    MAX_FACE_VALUE + 1);
            }
            return;
        }
        #endregion

        #region Other Methods
        // --------------------
        #endregion
        #endregion
    }
}
