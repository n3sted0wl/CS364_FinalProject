// Programmers: Paul Antonio
//              Seth Neds
//
// Date:        April 27, 2017
// File Name:   ScoreCategory.cs

#region Development Notes and TODOs
// --------------------
// TODO: Remove unnecessary using statements
// TODO: Insert IntelliSense Documentation template before class
// TODO: Remove unnecessary documentation
// TODO: Override ToString()

// Each score category is going to contain a description, point value,
// 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWithATwist.Classes
{
    #region IntelliSense Documentation
    /// <exception cref="">
    /// </exception>
    #endregion
    public class ScoreCategory
    {
        /*************************************************************/
        /*                           Data                            */
        /*************************************************************/
        #region Data Elements
        #region Fields
        // --------------------
        private string _description;
        private Status _status;

        public ValueCalculator          CalculateValue;
        public EnableAssociatedControl  EnableControl;
        public DisableAssociatedControl DisableControl;
        #endregion

        #region Properties
        // --------------------
        public string description
        {
            get         { return this._description; }
            private set { this._description = value; }
        }

        public Status status
        {
            get { return this._status; }
            set
            {
                this._status = value;
                if (value == Status.Available)
                    EnableControl();
                else
                    DisableControl();
            }
        }
        #endregion

        #region Structures
        // --------------------
        #endregion

        #region Enumerations
        // --------------------
        public enum Status { Available, Unavailable }
        #endregion

        #region Objects
        // --------------------
        #endregion

        #region Collections
        // --------------------
        #endregion

        #region Delegates
        // --------------------
        public delegate int  ValueCalculator();
        public delegate void EnableAssociatedControl();
        public delegate void DisableAssociatedControl();
        #endregion
        #endregion

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
