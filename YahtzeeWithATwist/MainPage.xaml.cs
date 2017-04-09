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
            /* ---------------------------------------------------------
             * 
             *                     Initializations
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
             * ------------------------------------------------------ */
        }

        /*************************************************************/
        /*                       Functionality                       */
        /*************************************************************/
        #region Methods
        #region Constructors
        // --------------------
        #endregion

        #region Overrides
        // --------------------
        public override string ToString()
        {
            throw new NotImplementedException(
                message: "ToString() override not implemented");
        }
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
