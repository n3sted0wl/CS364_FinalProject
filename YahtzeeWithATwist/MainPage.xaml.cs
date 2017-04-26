// Programmers:
// - Paul Antonio
// - Seth Neds
//
// Date:        April 27, 2017
// File Name:   MainPage.xaml.cs

#region Development Notes and TODOs
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
        #region Settings
        public const bool TESTING_MODE = false;
        #endregion

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
        public Button bt_rollDice;
        #endregion

        #region Collections
        // --------------------
        private List<Image> allDiceImages;
        #endregion
        #endregion

        /*************************************************************/
        /*                Page Initialization Method                 */
        /*************************************************************/
        public MainPage()
        {
            this.InitializeComponent();
            GameBoard.initialize(); // Includes score categories
            this.mapControls();
            this.initializePageControls();
            this.initializeDice();
            this.initializeDelegates();
        }

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Other Methods
        // --------------------
        private void mapControls()
        {
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

            // Map the gameboard controls
            GameBoard.totalScoreTextBlock = totalScore;
            GameBoard.bonusScoreTextBlock = bonusScore;

            // Buttons
            bt_rollDice = rollDieButton;
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
            foreach (Dice dice in GameBoard.RollableDice.Values)
            {
                dice.availability = Dice.Availability.Available;
            }
            foreach (Dice dice in GameBoard.HeldDice.Values)
            {
                dice.availability = Dice.Availability.Unavailable;
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

        private void initializePageControls()
        {
            #region Data
            #endregion

            #region Logic
            resetRollDiceButton();
            #endregion

            return;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            helpDialog.Visibility = Visibility.Collapsed;
        }

        private void updateImageSource(Image imageToUpdate, string newImagePath)
        {
            imageToUpdate.Source =
                new BitmapImage(new Uri(this.BaseUri, newImagePath));
        }
        #endregion

        #region Event Handlers
        // --------------------
        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            helpDialog.Visibility = Visibility.Visible;
        }

        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            GameBoard.resetScoreCategories();
            GameBoard.resetTotalScore();
            GameBoard.resetDice();
            GameBoard.calculateAllScores();
            GameBoard.rollsRemaining = GameBoard.ROLLS_PER_TURN;

            this.resetRollDiceButton();
            gameBoardMessageBox.Text = String.Empty;
            bt_rollDice.IsEnabled    = true;
        }

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

            GameBoard.calculateAllScores();

            return;
        }

        private async void rollDieButton_Click(object sender, RoutedEventArgs e)
        {
            Button rollButton = (sender as Button);

            // Add Dice Sound
            try
            {
                var element = new MediaElement();
                string soundFile = @"Assets\Sounds\roll-dice.wav";
                StorageFolder folderLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFile file = await folderLocation.GetFileAsync(soundFile);
                var stream = await file.OpenAsync(FileAccessMode.Read);
                element.SetSource(stream, file.ContentType);
                element.Play();
            }
            catch (FileNotFoundException ex)
            {
            }
            // For if the sound causes a crash
            catch (Exception)
            { }

            if (GameBoard.rollsRemaining >= 1) // TODO: Check for rollable dice
            {
                // Roll the dice
                foreach (Dice dice in GameBoard.RollableDice.Values)
                {
                    dice.roll();
                }

                // Calculate the possible values
                GameBoard.calculateAllScores();

                if (GameBoard.rollsRemaining == 1)
                {
                    rollButton.IsEnabled = false;
                }

                // Update the roll counter
                if (!TESTING_MODE)
                {
                    GameBoard.rollsRemaining -= 1;
                }
                rollButton.Content = $" Rolls Remaining: ";
                rollButton.Content += GameBoard.rollsRemaining.ToString();
            }

            return;
        }

        private void scoreLabel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ScoreCategory currentCategory = 
                GameBoard.getScoreCategoryByTextBlockControl(sender as TextBlock);

            if (currentCategory.status == ScoreCategory.Status.Unused)
            {
                currentCategory.descriptionTextBlock.Foreground =
                    new SolidColorBrush(Colors.Black);
            }

            // Remove possible bonus points from the display
            GameBoard.bonusScoreTextBlock.Text = String.Empty;

            return;
        }

        private void scoreLabel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ScoreCategory currentCategory = 
                GameBoard.getScoreCategoryByTextBlockControl(sender as TextBlock);
            if (currentCategory.status == ScoreCategory.Status.Unused)
            {
                currentCategory.descriptionTextBlock.Foreground =
                    new SolidColorBrush(Colors.OrangeRed);

                // Display possible bonus points
                GameBoard.bonusScoreTextBlock.Text =
                    Yahtzee.calculateBonus(GameBoard.ScoreableDice,
                        currentCategory.scoreValue).ToString();
            }

            return;
        }

        private void scoreLabel_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            #region Data
            ScoreCategory currentCategory =
                GameBoard.getScoreCategoryByTextBlockControl(sender as TextBlock);
            #endregion

            #region Logic
            if (currentCategory.status == ScoreCategory.Status.Unused)
            {
                currentCategory.status = ScoreCategory.Status.Used;

                // Apply points
                GameBoard.totalScore += currentCategory.scoreValue;
                GameBoard.totalScore += 
                    Yahtzee.calculateBonus(GameBoard.ScoreableDice, currentCategory.scoreValue);

                // Reset all the dice
                GameBoard.resetDice();
                foreach (Dice dice in GameBoard.RollableDice.Values)
                {
                    dice.roll();
                }

                // Enable the roll button
                resetRollDiceButton();

                // Calculate all the scores
                GameBoard.calculateAllScores();
            }

            if (GameBoard.isGameOver())
            {
                gameBoardMessageBox.Text = $"Game Over. Final Score: {GameBoard.totalScore.ToString()}";
                bt_rollDice.IsEnabled    = false;
                GameBoard.hideAllDice();
            }

            #endregion

            return;
        }

        private void resetRollDiceButton()
        {
            #region Logic
            bt_rollDice.IsEnabled    = true;
            GameBoard.rollsRemaining = GameBoard.ROLLS_PER_TURN - 1;
            bt_rollDice.Content      = $" Rolls Remaining: ";
            bt_rollDice.Content     += GameBoard.rollsRemaining.ToString();
            #endregion

            return;
        }
        #endregion
        #endregion
    }
}
