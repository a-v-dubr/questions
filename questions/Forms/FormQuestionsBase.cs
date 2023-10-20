namespace Presentation
{
    public abstract partial class FormQuestionsBase : Form
    {
        #region INITIALIZING
        protected const int _controlWidth = 350;

        public DataHandler DataHandler { get; protected set; }

        protected RadioButton? _answerInput;
        protected int _answerInputCounter = 0;

        protected bool _acceptTextBoxTextChanges = false;

        protected abstract void InitializeControls();

        public FormQuestionsBase()
        {
            DataHandler = new DataHandler();
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Displays controls and sets data handler statement for start interacting with a form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void DisplayMenuOptions(object? sender = default, EventArgs? e = default);

        /// <summary>
        /// Displays menu controls of the currently used form
        /// </summary>
        protected abstract void DisplayMenuControls();

        /// <summary>
        /// Sets values based on user input to default
        /// </summary>
        protected abstract void ResetInputValues();

        // <summary>
        /// Sets autoheight and fixed width for the control added to the flow layout panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetWidthWhenControlIsAdded(object sender, ControlEventArgs e)
        {
            int index = _flowLayoutPanel.Controls.Count - 1;
            var control = _flowLayoutPanel.Controls[index];
            control.AutoSize = true;
            control.Width = _controlWidth;
            if (control as Label is not null)
            {
                control.MaximumSize = new(_controlWidth, 0);
            }
        }

        /// <summary>
        /// Removes control from the flow layout panel controls' list
        /// </summary>
        /// <param name="control"></param>
        protected void RemoveControlFromFlowLayoutPanel<T>(List<T> controls) where T : Control
        {
            foreach (var control in controls)
            {
                if (_flowLayoutPanel.Controls.Contains(control))
                {
                    _flowLayoutPanel.Controls.Remove(control);
                }
            }
        }

        /// <summary>
        /// Disables visibility for form control elements
        /// </summary>
        /// <param name="controls"></param>
        protected static void HideControls(params Control[] controls)
        {
            foreach (var c in controls)
            {
                c.Visible = false;
            }
        }
        /// <summary>
        /// Enables visibility for form control elements
        /// </summary>
        /// <param name="controls"></param>
        public static void DisplayControls(params Control[] controls)
        {
            foreach (var c in controls)
            {
                c.Visible = true;
            }
        }

        /// <summary>
        /// Adds unique control to the flow layout panel
        /// </summary>
        /// <param name="control"></param>
        public void AddControlToFlowLayoutPanel(params Control[] controls)
        {
            foreach (var control in controls)
            {
                if (control is not null && !_flowLayoutPanel.Contains(control))
                {
                    _flowLayoutPanel.Controls.Add(control);
                }
            }
        }

        /// <summary>
        /// Displays a control below other panel elements
        /// </summary>
        /// <param name="control"></param>
        protected void DisplayControlBelowOthersInFlowPanel(Control control)
        {
            _flowLayoutPanel.Controls.SetChildIndex(control, _flowLayoutPanel.Controls.Count);
            if (control.Visible == false)
            {
                DisplayControls(control);
            }
        }
    }
}
