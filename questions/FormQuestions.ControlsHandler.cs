using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    public partial class FormQuestions
    {
        /// <summary>
        /// Disables visibility for form control elements
        /// </summary>
        /// <param name="controls"></param>
        private static void HideControls(params Control[] controls)
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
        private static void DisplayControls(params Control[] controls)
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
        private void AddControlToFlowLayoutPanel(params Control[] controls)
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
        /// Sets autoheight and fixed width for the control added to the flow layout panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlowLayoutPanel_SetWidthWhenControlIsAdded(object sender, ControlEventArgs e)
        {
            int index = _flowLayoutPanel.Controls.Count - 1;
            var control = _flowLayoutPanel.Controls[index];
            control.AutoSize = true;
            control.Width = _controlWidth;
        }


        /// <summary>
        /// Displays main menu buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReturnToMainMenu_Click(object sender, EventArgs e)
        {
            ReturnToMainMenu();
        }

        /// <summary>
        /// Displays main menu buttons and user helper label
        /// </summary>
        private void ReturnToMainMenu()
        {
            if (_repo.GetAvailableQuestions().Count == 0)
            {
                _buttonDisplayAvailableQuestions.Enabled = false;
            }
            else
            {
                _buttonDisplayAvailableQuestions.Enabled = true;
            }
            HideControls(_buttonReturnToMainMenu);
            _labelUserActionsHelper.Text = LabelTexts.ChooseMainMenuAction;
            DisplayControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions, _buttonExitProgram);
        }

        /// <summary>
        /// Hides main menu buttons
        /// </summary>
        private void HideMainMenuControls()
        {
            HideControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions, _buttonExitProgram);
        }
    }
}
