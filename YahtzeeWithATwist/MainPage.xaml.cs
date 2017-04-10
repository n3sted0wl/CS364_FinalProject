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
            this.mapControls();
            GameBoard.initialize();
            this.initializeDice();

            /* --------------------------------------------------------
             * 
             *                     Initializations
             * 
             * --------------------------------------------------------
             * 
             * The only logic that should go into here is the stuff to 
             * link the front end to the code behind.
             * 
             * Foreach score category, create a ScoreCategory object
             * Each object needs a single CalculateValue method defined
             * and assigned to the object's CalculateValue delegate
             *    I should probably create a static class with all the 
             *    game calculations in it. 
             *    And a static GAMEBOARD class that has all the data
             *    of the current game in it...
             * Then, each object needs assigned to it all the methods
             * that will enable and disable the associated controls
             * 
             * For each dice, (haven't thought this entirely through)
             * should probably initialize the data structures two 
             * arrays that hold them (in the static GameBoard class?)
             * 
             * ----------------------------------------------------- */
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

            // Swap out the dice positions
            if (GameBoard.HeldDice.Values.Contains(currentDice))
            {
                GameBoard.RollableDice[diceIndex].availability =
                    Dice.Availability.Available;
            }
            else if (GameBoard.RollableDice.Values.Contains(currentDice))
            {
                GameBoard.HeldDice[diceIndex].faceValue =
                    currentDice.faceValue;
                GameBoard.HeldDice[diceIndex].availability =
                    Dice.Availability.Available;
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
            foreach (KeyValuePair<int, Dice> rollableDice in GameBoard.RollableDice)
            {
                rollableDice.Value.roll();
            }
            return;
        }
        #endregion
        #endregion
    }
}
