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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace YahtzeeWithATwist.Classes
{
    #region IntelliSense Documentation
    /// <exception cref="NullReferenceException">
    ///     Thrown if a delegate with no methods assigned is called
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
        private string  _description;
        private Status  _status;
        private TextBox _textBox;

        private ValueCalculator          _CalculateValue; // Only assign one
        private EnableAssociatedControl  _EnableControl;  // Can assign multiple
        private DisableAssociatedControl _DisableControl; // Can assign muliple
        #endregion

        #region Properties
        // --------------------
        public string description
        {
            get         { return this._description; }
            private set
            {
                this._description = value;
                if (this.textBox != null)
                {
                    textBox.Text = description;
                }
            }
        }

        public Status status
        {
            get { return this._status; }
            set
            {
                // When the status is set, call the delegate that will
                // either enable or disable the associated front-end
                // control.
                this._status = value;
                if (value == Status.Available)
                {
                    if (EnableControl != null)
                        EnableControl();
                }
                else
                {
                    if (DisableControl != null)
                        DisableControl();
                }
            }
        }

        public TextBox textBox
        {
            get { return this._textBox; }
            set
            {
                this.textBox      = value;
                this.textBox.Text = description;
            }
        }

        public ValueCalculator CalculateValue
        {
            get { return this._CalculateValue; }
            private set { this._CalculateValue = value; }
        }

        public EnableAssociatedControl EnableControl
        {
            get { return this._EnableControl; }
            set { this._EnableControl = value; }
        }

        public DisableAssociatedControl DisableControl
        {
            get { return this._DisableControl; }
            set { this._DisableControl = value; }
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
        public ScoreCategory(
            string  initialDescription,
            Status  initialStatus  = Status.Available,
            TextBox initialTextBox = null)
        {
            this.description = initialDescription;
            this.status      = initialStatus;

            this.clearCalculateValueMethod();
            this.clearEnableControlMethods();
            this.clearDisablingControlMethods();
        }
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
        public void setCalculateValueMethod(ValueCalculator calculatingMethod)
        {
            this.clearCalculateValueMethod();
            this.CalculateValue += calculatingMethod;
            return;
        }

        public void clearCalculateValueMethod() =>
            this.CalculateValue = null;

        public void addEnableControlMethod(EnableAssociatedControl enablingMethod) =>
            this.EnableControl += enablingMethod;

        public void removeEnableControlMethod(EnableAssociatedControl enablingMethod) =>
            this.EnableControl -= enablingMethod;

        public void clearEnableControlMethods() =>
            this.EnableControl = null;

        public void addDisableControlMethod(DisableAssociatedControl disablingMethod) =>
            this.DisableControl += disablingMethod;

        public void removeDisableControlMethod(DisableAssociatedControl disablingMethod) =>
            this.DisableControl -= disablingMethod;

        public void clearDisablingControlMethods() =>
            this.DisableControl = null;
        #endregion

        #region Other Methods
        // --------------------
        #endregion
        #endregion
    }
}
