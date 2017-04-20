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
        private string    _description = string.Empty;
        private int       _scoreValue  = 0;
        private Status    _status;
        private TextBlock _descriptionTextBlock;
        private TextBlock _scoreTextBlock;

        private ValueCalculator          _CalculateValue; // Only assign one
        private EnableAssociatedControl  _EnableControl;  // Can assign multiple
        private DisableAssociatedControl _DisableControl; // Can assign muliple
        #endregion

        #region Properties
        // --------------------
        public string description
        {
            get { return this._description; }
            private set
            {
                this._description = value;

                // Update the associated textbox
                if (this.descriptionTextBlock != null &&
                    this.status == Status.Available)
                    descriptionTextBlock.Text = this.description;
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

        public TextBlock descriptionTextBlock
        {
            get { return this._descriptionTextBlock; }
            set
            {
                this._descriptionTextBlock      = value;
                if (_descriptionTextBlock != null)
                    this._descriptionTextBlock.Text = description;
            }
        }

        public TextBlock scoreTextBlock
        {
            get { return _scoreTextBlock; }
            set
            {
                _scoreTextBlock = value;

                // Update the textbox value
                if (_scoreTextBlock != null)
                    _scoreTextBlock.Text = this.scoreValue.ToString();
            }
        }

        public int scoreValue
        {
            get { return _scoreValue; }
            set
            {
                _scoreValue = value;

                // Update the associated textbox
                if (scoreTextBlock != null)
                    scoreTextBlock.Text = _scoreValue.ToString();
            }
        }

        public ValueCalculator CalculateValue
        {
            get { return this._CalculateValue; }
            set { this._CalculateValue = value; }
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
        public delegate int  ValueCalculator(List<Dice> scoreableDice);
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
            string    initialDescription,
            Status    initialStatus               = Status.Available,
            TextBlock initialdescriptionTextBlock = null,
            TextBlock initialScoreTextBlock       = null)
        {
            this.description          = initialDescription;
            this.status               = initialStatus;
            this.descriptionTextBlock = initialdescriptionTextBlock;
            this.scoreTextBlock       = initialScoreTextBlock;

            this.clearCalculateValueMethod();
            this.clearEnableControlMethods();
            this.clearDisablingControlMethods();
        }
        #endregion

        #region Overrides
        // --------------------
        public override string ToString()
        {
            return $"Score Description: {this.description}";
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
