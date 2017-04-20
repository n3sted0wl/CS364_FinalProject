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
using static YahtzeeWithATwist.Classes.GameBoard;

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
            ScoreCategories[Categories.Aces].descriptionTextBlock            = onesLabel;
            ScoreCategories[Categories.Twos].descriptionTextBlock            = twosLabel;
            ScoreCategories[Categories.Threes].descriptionTextBlock          = threesLabel;
            ScoreCategories[Categories.Fours].descriptionTextBlock           = foursLabel;
            ScoreCategories[Categories.Fives].descriptionTextBlock           = fivesLabel;
            ScoreCategories[Categories.Sixes].descriptionTextBlock           = sixesLabel;

            ScoreCategories[Categories.Full_House].descriptionTextBlock      = fullHouseLabel;
            ScoreCategories[Categories.Four_of_a_Kind].descriptionTextBlock  = fourOfKindLabel;
            ScoreCategories[Categories.Three_of_a_Kind].descriptionTextBlock = threeOfKindLabel;
            ScoreCategories[Categories.Small_Straight].descriptionTextBlock  = smallStraightLabel;
            ScoreCategories[Categories.Large_Straight].descriptionTextBlock  = largeStraightLabel;
            ScoreCategories[Categories.Yahtzee].descriptionTextBlock         = yahtzeeLabel;
            ScoreCategories[Categories.Chance].descriptionTextBlock          = chanceLabel;

            ScoreCategories[Categories.Aces].scoreTextBlock                  = onesScore;
            ScoreCategories[Categories.Twos].scoreTextBlock                  = twosScore;
            ScoreCategories[Categories.Threes].scoreTextBlock                = threesScore;
            ScoreCategories[Categories.Fours].scoreTextBlock                 = foursScore;
            ScoreCategories[Categories.Fives].scoreTextBlock                 = fivesScore;
            ScoreCategories[Categories.Sixes].scoreTextBlock                 = sixesScore;

            ScoreCategories[Categories.Full_House].scoreTextBlock            = fullHouseScore;
            ScoreCategories[Categories.Four_of_a_Kind].scoreTextBlock        = fourOfKindScore;
            ScoreCategories[Categories.Three_of_a_Kind].scoreTextBlock       = threeOfKindScore;
            ScoreCategories[Categories.Small_Straight].scoreTextBlock        = smallStraightScore;
            ScoreCategories[Categories.Large_Straight].scoreTextBlock        = largeStraightScore;
            ScoreCategories[Categories.Yahtzee].scoreTextBlock               = yahtzeeScore;
            ScoreCategories[Categories.Chance].scoreTextBlock                = chanceScore;
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
            ScoreCategories[Categories.Aces].setCalculateValueMethod(Yahtzee.calculateAces);
            ScoreCategories[Categories.Twos].setCalculateValueMethod(Yahtzee.calculateTwos);
            ScoreCategories[Categories.Threes].setCalculateValueMethod(Yahtzee.calculateThrees);
            ScoreCategories[Categories.Fours].setCalculateValueMethod(Yahtzee.calculateFours);
            ScoreCategories[Categories.Fives].setCalculateValueMethod(Yahtzee.calculateFives);
            ScoreCategories[Categories.Sixes].setCalculateValueMethod(Yahtzee.calculateSixes);

            ScoreCategories[Categories.Full_House].setCalculateValueMethod(Yahtzee.calculateFullHouse);
            ScoreCategories[Categories.Four_of_a_Kind].setCalculateValueMethod(Yahtzee.calculateFourOfAKind);
            ScoreCategories[Categories.Three_of_a_Kind].setCalculateValueMethod(Yahtzee.calculateThreeOfAKind);
            ScoreCategories[Categories.Small_Straight].setCalculateValueMethod(Yahtzee.calculateSmallStraight);
            ScoreCategories[Categories.Large_Straight].setCalculateValueMethod(Yahtzee.calculateLargeStraight);
            ScoreCategories[Categories.Yahtzee].setCalculateValueMethod(Yahtzee.calculateYahtzee);
            ScoreCategories[Categories.Chance].setCalculateValueMethod(Yahtzee.calculateChance);
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
            foreach (KeyValuePair<Categories, ScoreCategory> category in GameBoard.ScoreCategories)
            {
                category.Value.scoreValue = category.Value.CalculateValue(GameBoard.ScoreableDice);
            }

            return;
        }
        #endregion
        #endregion
    }
}
