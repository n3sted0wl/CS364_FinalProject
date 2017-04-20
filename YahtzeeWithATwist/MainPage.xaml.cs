// Programmers:
// - Paul Antonio
// - Seth Neds
//
// Date:        April 27, 2017
// File Name:   MainPage.xaml.cs

#region Development Notes and TODOs
// --------------------
// TODO: Remove unnecessary using statements
// TODO: Remove unnecessary documentation
// TODO: Add exception documentation, if any
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using YahtzeeWithATwist.Classes;

namespace YahtzeeWithATwist
{
    /// <summary>
    ///     Manages the front-end main page of the Yahtzee Game
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Controls
        // Backend Control mapping to decouple front-end naming 
        public Image  img_rollDice_1,
                      img_rollDice_2,
                      img_rollDice_3,
                      img_rollDice_4,
                      img_rollDice_5,
                      img_heldDice_1,
                      img_heldDice_2,
                      img_heldDice_3,
                      img_heldDice_4,
                      img_heldDice_5;

        #endregion

        #region Fields
        // --------------------
        #endregion

        #region Properties
        // --------------------
        #endregion

        #region Structures
        // --------------------
        #endregion

        #region Enumerations
        // --------------------
        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        private List<Image> allDiceImages;
        #endregion

        #region Delegates
        // --------------------
        #endregion
        #endregion

        /*************************************************************/
        /*                Page Initialization Method                 */
        /*************************************************************/
        public MainPage()
        {
            this.InitializeComponent();

            GameBoard.initialize();
            this.mapControls();
            this.initializeDice();
            this.initializeDelegates();
        }

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Accessors
        // --------------------
        #endregion

        #region Mutators
        // --------------------
        #endregion

        #region Other Methods
        // --------------------
        private void mapControls()
        {
            // TODO: Map each image to the final naming used

            // Map dice to images
            this.img_rollDice_1 = rollDie1;
            this.img_rollDice_2 = rollDie2;
            this.img_rollDice_3 = rollDie3;
            this.img_rollDice_4 = rollDie4;
            this.img_rollDice_5 = rollDie5;

            this.img_heldDice_1 = heldDie1;
            this.img_heldDice_2 = heldDie2;
            this.img_heldDice_3 = heldDie3;
            this.img_heldDice_4 = heldDie4;
            this.img_heldDice_5 = heldDie5;

            // Map score categories to labels and score textblocks
            GameBoard.ScoreCategories["Aces"].descriptionTextBlock   = onesLabel;
            GameBoard.ScoreCategories["Twos"].descriptionTextBlock   = twosLabel;
            GameBoard.ScoreCategories["Threes"].descriptionTextBlock = threesLabel;
            GameBoard.ScoreCategories["Fours"].descriptionTextBlock  = foursLabel;
            GameBoard.ScoreCategories["Fives"].descriptionTextBlock  = fivesLabel;
            GameBoard.ScoreCategories["Sixes"].descriptionTextBlock  = sixesLabel;

            GameBoard.ScoreCategories["Full_House"].descriptionTextBlock      = fullHouseLabel;
            GameBoard.ScoreCategories["Four_of_a_Kind"].descriptionTextBlock  = fourOfKindLabel;
            GameBoard.ScoreCategories["Three_of_a_Kind"].descriptionTextBlock = threeOfKindLabel;
            GameBoard.ScoreCategories["Small_Straight"].descriptionTextBlock  = smallStraightLabel;
            GameBoard.ScoreCategories["Large_Straight"].descriptionTextBlock  = largeStraightLabel;
            GameBoard.ScoreCategories["Yahtzee"].descriptionTextBlock         = yahtzeeLabel;
            GameBoard.ScoreCategories["Chance"].descriptionTextBlock          = chanceLabel;

            GameBoard.ScoreCategories["Aces"].scoreTextBlock   = onesScore;
            GameBoard.ScoreCategories["Twos"].scoreTextBlock   = twosScore;
            GameBoard.ScoreCategories["Threes"].scoreTextBlock = threesScore;
            GameBoard.ScoreCategories["Fours"].scoreTextBlock  = foursScore;
            GameBoard.ScoreCategories["Fives"].scoreTextBlock  = fivesScore;
            GameBoard.ScoreCategories["Sixes"].scoreTextBlock  = sixesScore;

            GameBoard.ScoreCategories["Full_House"].scoreTextBlock      = fullHouseScore;
            GameBoard.ScoreCategories["Four_of_a_Kind"].scoreTextBlock  = fourOfKindScore;
            GameBoard.ScoreCategories["Three_of_a_Kind"].scoreTextBlock = threeOfKindScore;
            GameBoard.ScoreCategories["Small_Straight"].scoreTextBlock  = smallStraightScore;
            GameBoard.ScoreCategories["Large_Straight"].scoreTextBlock  = largeStraightScore;
            GameBoard.ScoreCategories["Yahtzee"].scoreTextBlock         = yahtzeeScore;
            GameBoard.ScoreCategories["Chance"].scoreTextBlock          = chanceScore;
            return;
        }

        private void initializeDice()
        {
            // Assign the delegate method that updates the image sourcess
            Dice.updateImageDelegate += updateImageSource;

            // Put all dice images into the collection
            allDiceImages = new List<Image>();

            allDiceImages.Add(img_rollDice_1);
            allDiceImages.Add(img_rollDice_2);
            allDiceImages.Add(img_rollDice_3);
            allDiceImages.Add(img_rollDice_4);
            allDiceImages.Add(img_rollDice_5);

            allDiceImages.Add(img_heldDice_1);
            allDiceImages.Add(img_heldDice_2);
            allDiceImages.Add(img_heldDice_3);
            allDiceImages.Add(img_heldDice_4);
            allDiceImages.Add(img_heldDice_5);

            // Link up the GameBoard Dice to the image controls
            GameBoard.RollableDice[1].imageControl = img_rollDice_1;
            GameBoard.RollableDice[2].imageControl = img_rollDice_2;
            GameBoard.RollableDice[3].imageControl = img_rollDice_3;
            GameBoard.RollableDice[4].imageControl = img_rollDice_4;
            GameBoard.RollableDice[5].imageControl = img_rollDice_5;

            GameBoard.HeldDice[1].imageControl = img_heldDice_1;
            GameBoard.HeldDice[2].imageControl = img_heldDice_2;
            GameBoard.HeldDice[3].imageControl = img_heldDice_3;
            GameBoard.HeldDice[4].imageControl = img_heldDice_4;
            GameBoard.HeldDice[5].imageControl = img_heldDice_5;

            // Make all rollable dice images visible and each held dice invisible
            foreach (KeyValuePair<int, Dice> item in GameBoard.RollableDice)
            {
                item.Value.availability = Dice.Availability.Available;
            }
            foreach (KeyValuePair<int, Dice> item in GameBoard.HeldDice)
            {
                item.Value.availability = Dice.Availability.Unavailable;
            }

            // Connect the click event handler
            foreach (Image diceImage in allDiceImages)
            {
                diceImage.PointerReleased += new PointerEventHandler(diceClicked);
            }

            return;
        }

        private void initializeDelegates()
        {
            GameBoard.ScoreCategories["Aces"].setCalculateValueMethod(Yahtzee.calculateAces);
            GameBoard.ScoreCategories["Twos"].setCalculateValueMethod(Yahtzee.calculateTwos);
            GameBoard.ScoreCategories["Threes"].setCalculateValueMethod(Yahtzee.calculateThrees);
            GameBoard.ScoreCategories["Fours"].setCalculateValueMethod(Yahtzee.calculateFours);
            GameBoard.ScoreCategories["Fives"].setCalculateValueMethod(Yahtzee.calculateFives);
            GameBoard.ScoreCategories["Sixes"].setCalculateValueMethod(Yahtzee.calculateSixes);

            GameBoard.ScoreCategories["Full_House"].setCalculateValueMethod(Yahtzee.calculateFullHouse);
            GameBoard.ScoreCategories["Four_of_a_Kind"].setCalculateValueMethod(Yahtzee.calculateFourOfAKind);
            GameBoard.ScoreCategories["Three_of_a_Kind"].setCalculateValueMethod(Yahtzee.calculateThreeOfAKind);
            GameBoard.ScoreCategories["Small_Straight"].setCalculateValueMethod(Yahtzee.calculateSmallStraight);
            GameBoard.ScoreCategories["Large_Straight"].setCalculateValueMethod(Yahtzee.calculateLargeStraight);
            GameBoard.ScoreCategories["Yahtzee"].setCalculateValueMethod(Yahtzee.calculateYahtzee);
            GameBoard.ScoreCategories["Chance"].setCalculateValueMethod(Yahtzee.calculateChance);
            return;
        }

        private void updateImageSource(Image imageToUpdate, string newImagePath)
        {
            imageToUpdate.Source =
                new BitmapImage(new Uri(this.BaseUri, newImagePath));
        }
        #endregion

        #region Event Handlers
        // --------------------
        private void diceClicked(object sender, RoutedEventArgs e)
        {
            Dice currentDice = GameBoard.getDiceByImageControl((Image) sender);
            int  diceIndex   = GameBoard.getDiceIndex(currentDice);

            currentDice.availability = Dice.Availability.Unavailable;

            // Swap out the dice positions with availability and bonuses
            if (GameBoard.HeldDice.Values.Contains(currentDice))
            {
                GameBoard.RollableDice[diceIndex].availability =
                    Dice.Availability.Available;
                GameBoard.RollableDice[diceIndex].bonusFace =
                    currentDice.bonusFace;
            }
            else if (GameBoard.RollableDice.Values.Contains(currentDice))
            {
                GameBoard.HeldDice[diceIndex].faceValue =
                    currentDice.faceValue;
                GameBoard.HeldDice[diceIndex].availability =
                    Dice.Availability.Available;
                GameBoard.HeldDice[diceIndex].bonusFace =
                    currentDice.bonusFace;
            }
            else
            {
                throw new InvalidOperationException(
                    message: "An unmapped dice image has been clicked");
            }

            return;
        }

        private void rollDieButton_Click(object sender, RoutedEventArgs e)
        {
            // Roll the dice
            foreach (KeyValuePair<int, Dice> rollableDice in GameBoard.RollableDice)
            {
                rollableDice.Value.roll();
            }

            // Calculate the possible values
            foreach (KeyValuePair<string, ScoreCategory> category in GameBoard.ScoreCategories)
            {
                category.Value.scoreValue = category.Value.CalculateValue(GameBoard.ScoreableDice);
            }

            return;
        }
        #endregion
        #endregion
    }
}
