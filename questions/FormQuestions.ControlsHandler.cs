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
            DisplayButtonReturnToMainMenu();
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
        /// Displays button to return to main menu and places it under other panel elements
        /// </summary>
        private void DisplayButtonReturnToMainMenu()
        {
            _flowLayoutPanel.Controls.SetChildIndex(_buttonReturnToMainMenu, _flowLayoutPanel.Controls.Count);
            DisplayControls(_buttonReturnToMainMenu);
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
            _questionCreatingInProcess = default;
            _questionEditingInProcess = default;
            _selectedCategory = default;
            _selectedQuestion = default;
            _questionDTO = default;

            if (_repo.GetAvailableQuestions().Count == 0)
            {
                _buttonDisplayAvailableQuestions.Enabled = false;
            }
            else
            {
                _buttonDisplayAvailableQuestions.Enabled = true;
            }

            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == _buttonAddNewQuestion || c == _buttonDisplayAvailableQuestions || c == _buttonExitProgram || c == _labelUserActionsHelper)
                {
                    DisplayControls(c);
                }
                else
                {
                    HideControls(c);
                }
            }
            _labelUserActionsHelper.Text = LabelTexts.ChooseMainMenuAction;
        }

        /// <summary>
        /// Hides main menu buttons
        /// </summary>
        private void HideMainMenuControls()
        {
            HideControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions, _buttonExitProgram);
        }



        /// <summary>
        /// Creates disabled radiobutton with defined text and adds it to the flow layout panel
        /// </summary>
        /// <param name="text"></param>
        private void CreateAnswerRadiobutton(string text)
        {
            var r = new RadioButton() { Enabled = false, Text = text };
            _radioButtonsForPickingAnswer.Add(r);
            AddControlToFlowLayoutPanel(r);
        }

        /// <summary>
        /// Enables radiobutton and subscribes it to method for defining correct answer
        /// </summary>
        /// <param name="r"></param>
        private void EnableAnswerRadioButtonToSetCorrectAnswer(RadioButton r)
        {
            r.CheckedChanged += RadiobuttonToSetCorrectAnswer_CheckedChanged!;
            r.Enabled = true;
        }

        /// <summary>
        /// Enables radiobutton and subscribes it to method for answering the question
        /// </summary>
        /// <param name="r"></param>
        private void EnableAnswerRadioButtonToPickAnswer(RadioButton r)
        {
            r.CheckedChanged += RadioButtonsForPickingAnswer_CheckedChanged!;
            r.Enabled = true;
        }



        /// <summary>
        /// Deletes combobox's items and clears its text
        /// </summary>
        /// <param name="comboBox"></param>
        private static void ClearComboBox(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.Items.Clear();
            }
            comboBox.Text = string.Empty;
        }

        /// <summary>
        /// Removes control from the flow layout panel controls' list
        /// </summary>
        /// <param name="control"></param>
        private void RemoveControlFromFlowLayoutPanel(Control control)
        {
            if (_flowLayoutPanel.Controls.Contains(control))
            {
                _flowLayoutPanel.Controls.Remove(control);
            }
        }

        /// <summary>
        /// Removes control from the flow layout panel controls' list
        /// </summary>
        /// <param name="control"></param>
        private void RemoveControlFromFlowLayoutPanel<T>(List<T> controls) where T : Control
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
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExitProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
