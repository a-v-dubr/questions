using Domain;
using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    public partial class FormQuestions
    {
        private const int _controlWidth = 400;
        private bool ComboBoxContainsQuestion => _selectedCategory is not null && _comboBox.Items.Count > 0 && _comboBox.SelectedItem is not null;
        private bool ComboBoxIsReadyToAddQuestions => _selectedCategory is not null && _selectedQuestion is null && _comboBox.Items.Count == 0;
        private bool ComboBoxIsReadyToAddCategories => _selectedCategory is null && _comboBox.Items.Count == 0;

        private bool _pauseControlAction = false;

        private bool _acceptTextBoxTextChanges = false;

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
            if (!_pauseControlAction)
            {
                var r = new RadioButton() { Enabled = false, Text = text };
                _radioButtonsForAnswers.Add(r);
                AddControlToFlowLayoutPanel(r);
                DisplayButtonReturnToMainMenu();
            }
        }

        /// <summary>
        /// Enables radiobutton and subscribes it to method for defining a correct answer or picking an answer 
        /// </summary>
        /// <param name="r"></param>
        private void EnableAnswerRadioButton(RadioButton r)
        {
            r.CheckedChanged += OnRadioButtonForAnswerCheckedChanged!;
            r.Enabled = true;
        }

        /// <summary>
        /// Deletes combobox's items and clears its text
        /// </summary>
        private void ClearComboBox()
        {
            if (_comboBox.Items.Count > 0)
            {
                _comboBox.Items.Clear();
            }
            _comboBox.Text = string.Empty;
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
        /// Adds categories' titles to the combobox items if their list is empty
        /// </summary>
        /// <param name="categories"></param>
        private void AddCategoriesToComboBox(IEnumerable<Category> categories)
        {
            if (categories is not null && categories.Any() && _comboBox.Items.Count == 0)
            {
                foreach (var c in categories)
                {
                    _comboBox.Items.Add(c.Title);
                }
            }
        }

        /// <summary>
        /// Adds questions' texts to the combobox items if their list is empty
        /// </summary>
        /// <param name="categories"></param>
        private void AddQuestionsToComboBox(IEnumerable<Question> questions)
        {
            if (questions is not null && questions.Any() && _comboBox.Items.Count == 0)
            {
                foreach (var q in questions)
                {
                    _comboBox.Items.Add(q.Text);
                }
            }
        }

        /// <summary>
        /// Gets user input values to default and displays main menu buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonReturnToMainMenuClick(object sender, EventArgs e)
        {
            _selectedCategory = default;
            _selectedQuestion = default;

            _questionDTO = default;

            _questionCreatingInProcess = default;
            _questionEditingInProcess = default;
            _acceptTextBoxTextChanges = false;

            ClearComboBox();
            _textBox.Clear();

            _buttonDisplayAvailableQuestions.Enabled = _repo.GetAvailableQuestions().Count != 0;

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
        /// Displays button to return to main menu and places it under other panel elements
        /// </summary>
        private void DisplayButtonReturnToMainMenu()
        {
            _flowLayoutPanel.Controls.SetChildIndex(_buttonReturnToMainMenu, _flowLayoutPanel.Controls.Count);
            if (_buttonReturnToMainMenu.Visible == false)
            {
                DisplayControls(_buttonReturnToMainMenu);
            }
        }

        private void DisplayPreviousQuestionInTextBox()
        {
            _pauseControlAction = true;
            if (string.IsNullOrWhiteSpace(_textBox.Text) && _questionEditingInProcess && _selectedQuestion is not null)
            {
                _textBox.Text = _selectedQuestion.Text;
            }
            _pauseControlAction = false;
        }

        private void DisplayPreviousAnswerInTextBox()
        {
            _pauseControlAction = true;
            if (string.IsNullOrWhiteSpace(_textBox.Text) && _questionEditingInProcess && _selectedQuestion is not null)
            {
                if (_selectedQuestion.Answers.Count > _answerInputCounter)
                {
                    _textBox.Text = _selectedQuestion.Answers[_answerInputCounter].Text;
                }
            }
            _pauseControlAction = false;
        }

        /// <summary>
        /// Exits application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonExitProgramClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
