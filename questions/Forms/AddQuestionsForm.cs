﻿using static Presentation.Helper.ControlMessages;
using Presentation.Helper;
using Infrastructure;

namespace Presentation.Forms
{
    public partial class AddQuestionsForm : Form
    {
        private readonly DataHandler _dataHandler;
        protected RadioButton? _answerInput;
        private const int _minAnswerOptionsCount = 2;
        private const int _maxAnswerOptionsCount = 10;

        private bool QuestionTextAndAnswersReceived => _dataHandler.QuestionDTO is not null && _dataHandler.SelectedQuestion is null && _answerInput is null;
        private bool QuestionDTOIsReadyToMapping => _dataHandler.QuestionDTO is not null && _dataHandler.SelectedQuestion is null && _answerInput is not null;

        public AddQuestionsForm(DataHandler dataHandler)
        {
            InitializeComponent();
            _dataHandler = dataHandler;

            SetRequiredAnswersTextBoxes();
        }

        private void SetRequiredAnswersTextBoxes()
        {
            for (int i = 0; i < _minAnswerOptionsCount; i++)
            {
                AddAnswerControlsToAnswersTable();
            }
        }

        private void ResetInputValues()
        {
            _dataHandler.ResetSelectedQuestionToDefault();
            _dataHandler.ResetSelectedCategoryToDefault();
            _dataHandler.ResetQuestionDTOToDefault();
            _answerInput = default;
        }

        private void ReinitializeControls()
        {
            DisplayControlsForCreatingQuestion();
            ClearControlsForUserInput();
            SetRequiredAnswersTextBoxes();

            _labelTypeQuestionText.Text = LabelTexts.TypeQuestionText;
        }

        private void DisplayControlsForCreatingQuestion()
        {
            ControlsHelper.DisplayControls(_answersTable, _textBoxForQuestionTitle, _labelTypeAnswerOptions, _handlingAnswersButtonsTable);
            ControlsHelper.HideControls(_buttonChangeQuestionCategory, _buttonCreateAnotherQuestion);
        }

        private void ClearControlsForUserInput()
        {
            _textBoxForQuestionTitle.Text = string.Empty;
            _textBoxesForAnswers.Clear();

            _answersTable.SuspendLayout();
            _answersTable.Controls.Clear();
            _answersTable.RowCount = 0;
            _answersTable.ResumeLayout();
        }


        private void OnButtonAddNewAnswerOptionClick(object sender, EventArgs e)
        {
            if (_textBoxesForAnswers.Count < _maxAnswerOptionsCount)
            {
                AddAnswerControlsToAnswersTable();
                UpdateDeleteButtonsClickability();
            }
            else
            {
                var toolTip = new ToolTip();
                toolTip.Show(ToolTipsTexts.NoMoreAnswerOptionsAvailable, _buttonAddNewAnswerOption, _buttonAddNewAnswerOption.Width, 0, 2000);
            }
        }

        private void UpdateDeleteButtonsClickability()
        {
            Button? deleteButton;

            for (int i = 0; i < _answersTable.RowCount; i++)
            {
                deleteButton = _answersTable.GetControlFromPosition(1, i) as Button;
                if (deleteButton is not null)
                {
                    deleteButton.Enabled = _textBoxesForAnswers.Count > _minAnswerOptionsCount;
                }
            }
        }

        private void AddAnswerControlsToAnswersTable()
        {
            _answersTable.SuspendLayout();

            _answersTable.RowCount++;
            _answersTable.RowStyles.Add(new RowStyle());

            AddAnswerOptionTextBoxToAnswerTable();
            AddDeleteButtonToAnswerTable();

            _answersTable.ResumeLayout();
        }

        private void AddAnswerOptionTextBoxToAnswerTable()
        {
            var answerOptionTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 4, 3, 4),
                Size = new Size(399, 25),
            };
            _answersTable.Controls.Add(answerOptionTextBox, 0, _answersTable.RowCount - 1);
            _answersTable.SetCellPosition(answerOptionTextBox, new TableLayoutPanelCellPosition(0, _answersTable.RowCount - 1));
            _textBoxesForAnswers.Add(answerOptionTextBox);
        }

        private void AddDeleteButtonToAnswerTable()
        {
            var deleteButton = new Button()
            {
                Margin = new Padding(3, 4, 3, 4),
                Size = new Size(29, 25),
                Text = "X",
                Enabled = false
            };
            _answersTable.Controls.Add(deleteButton, 1, _answersTable.RowCount - 1);
            _answersTable.SetCellPosition(deleteButton, new TableLayoutPanelCellPosition(1, _answersTable.RowCount - 1));
            deleteButton.Click += OnAnswerOptionDeleteButtonClick!;
        }

        private void OnAnswerOptionDeleteButtonClick(object sender, EventArgs e)
        {
            if (sender is Button b)
            {
                var position = _answersTable.GetCellPosition(b);
                int row = position.Row;
                RemoveContentsFromAnswersTableRow(row);
                UpdateDeleteButtonsClickability();
            }
        }

        private void RemoveContentsFromAnswersTableRow(int row)
        {
            _answersTable.SuspendLayout();

            DeleteAnswerTableRowContents(row);
            RaiseRowsInAnswersTable(row);

            _answersTable.RowCount--;
            UpdateDeleteButtonsClickability();

            _answersTable.ResumeLayout();
        }

        private void DeleteAnswerTableRowContents(int row)
        {
            for (int column = 0; column < _answersTable.ColumnCount; column++)
            {
                var c = _answersTable.GetControlFromPosition(column, row);
                if (c is not null)
                {
                    _answersTable.Controls.Remove(c);
                    if (c is TextBox t)
                    {
                        _textBoxesForAnswers.Remove(t);
                    }
                }
            }
        }

        private void RaiseRowsInAnswersTable(int emptyRowIndex)
        {
            int nextRowIndex = emptyRowIndex + 1;

            for (int i = nextRowIndex; i < _answersTable.RowCount; i++)
            {
                for (int column = 0; column < _answersTable.ColumnCount; column++)
                {
                    var c = _answersTable.GetControlFromPosition(column, i);
                    if (c is not null)
                    {
                        _answersTable.Controls.Remove(c);
                        _answersTable.Controls.Add(c, column, i - 1);
                    }
                }
            }
        }

        private void OnButtonAcceptQuestionTextClick(object sender, EventArgs e)
        {
            if (ValidateTextBoxesContents())
            {
                ControlsHelper.HideControls(_answersTable);

                var dto = new QuestionDTO() { QuestionCategory = QuestionRepository.DefaultCategory, QuestionText = _textBoxForQuestionTitle.Text };
                _dataHandler.SetQuestionDTO(dto);

                var texts = from tb in _textBoxesForAnswers select tb.Text;
                foreach (string t in texts)
                {
                    dto.AnswersTexts.Add(t);
                    CreateAnswerRadiobutton(t);
                }

                if (QuestionTextAndAnswersReceived)
                {
                    DisplayAnswersOfQuestionOrDTO();
                    ControlsHelper.HideControls(_textBoxForQuestionTitle, _labelTypeAnswerOptions, _handlingAnswersButtonsTable);

                    _radioButtons.ForEach(EnableAnswerRadioButton);
                    _labelTypeQuestionText.Text = string.Format(LabelTexts.ChooseCorrectAnswer, _dataHandler?.QuestionDTO?.QuestionText);
                    _flowLayoutPanel.DisplayControlBelowOthersInFlowPanel(_buttonSaveQuestionFinally);
                }
            }
        }

        private void OnButtonSaveQuestionClick(object sender, EventArgs e)
        {
            if (QuestionDTOIsReadyToMapping)
            {
                _dataHandler!.QuestionDTO!.CorrectAnswerIndex = _dataHandler.QuestionDTO.AnswersTexts.FindIndex(a => a == _answerInput?.Text);
                var question = _dataHandler.QuestionDTO.MapDTO();

                if (question is not null)
                {
                    _dataHandler.AddNewQuestion(question);
                    _dataHandler.SetSelectedQuestion(question);

                    _labelTypeQuestionText.Text = string.Format(LabelTexts.QuestionIsSavedAndAvailable, _dataHandler.SelectedQuestion?.Text);

                    _flowLayoutPanel.RemoveControlFromFlowLayoutPanel(_radioButtons);
                    _radioButtons.Clear();

                    ControlsHelper.HideControls(_buttonSaveQuestionFinally);
                    ControlsHelper.DisplayControls(_buttonChangeQuestionCategory, _buttonCreateAnotherQuestion);
                }
            }
        }

        private bool ValidateTextBoxesContents()
        {
            var tb = new List<TextBox> { _textBoxForQuestionTitle };
            tb.AddRange(_textBoxesForAnswers);

            return UserInputsAreNotEmpty(tb) && AnswerTextsAreUnique(tb);
        }

        private static bool UserInputsAreNotEmpty(List<TextBox> tb)
        {
            var emptyTextBoxes = tb.Where(tb => string.IsNullOrWhiteSpace(tb.Text));
            if (emptyTextBoxes.Any())
            {
                foreach (var emptyTextBox in emptyTextBoxes)
                {
                    DisplayEmptyTextBoxToolTip(emptyTextBox);
                }
                return false;
            }
            return true;
        }

        private bool AnswerTextsAreUnique(List<TextBox> tb)
        {
            tb.Remove(_textBoxForQuestionTitle);
            var repeats = tb.GroupBy(tb => tb.Text)
                        .Where(g => g.Count() > 1)
                        .SelectMany(g => g.ToList())
                        .ToList();

            if (repeats.Any())
            {
                foreach (var r in repeats)
                {
                    DisplayRepeatedAnswerTextsToolTip(r);
                }
                return false;
            }
            return true;
        }

        private static void DisplayEmptyTextBoxToolTip(TextBox emptyTextBox)
        {
            var toolTip = new ToolTip();
            toolTip.Show(ToolTipsTexts.TextBoxContentsIsNotFound, emptyTextBox, emptyTextBox.Width, 0, 2000);
        }

        private static void DisplayRepeatedAnswerTextsToolTip(TextBox repeatedAnswerTextBox)
        {
            var toolTip = new ToolTip();
            toolTip.Show(ToolTipsTexts.AnswerOptionCannotBeEqual, repeatedAnswerTextBox, repeatedAnswerTextBox.Width, 0, 2000);
        }

        /// <summary>
        /// Displays button to save correct answer option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRadioButtonForAnswerCheckedChanged(object sender, EventArgs e)
        {
            _answerInput = (RadioButton)sender;
            if (_answerInput.Checked)
            {
                foreach (var r in _radioButtons)
                {
                    if (r == _answerInput)
                    {
                        r.ForeColor = Color.ForestGreen;
                    }
                    else
                    {
                        r.ForeColor = SystemColors.ControlText;
                    }
                }
                _buttonSaveQuestionFinally.Enabled = true;
            }
        }

        /// <summary>
        /// Opens a form for creating or choosing the question category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonChangeQuestionCategoryClick(object sender, EventArgs e)
        {
            var categoriesForm = new AddCategoryForm(_dataHandler);
            categoriesForm.ShowDialog();

            _dataHandler.Repo.RetrieveQuestionsFromDb();
            ResetInputValues();
            ReinitializeControls();
        }

        public void DisplayAnswersOfQuestionOrDTO()
        {
            if (_dataHandler.QuestionDTO is not null)
            {
                foreach (var r in _radioButtons)
                {
                    EnableAnswerRadioButton(r);
                }
                _labelTypeQuestionText.Text = string.Format(LabelTexts.ChooseCorrectAnswer, _dataHandler.QuestionDTO.QuestionText);
            }
        }

        /// <summary>
        /// Creates disabled radiobutton with defined text and adds it to the flow layout panel
        /// </summary>
        /// <param name="text"></param>
        private void CreateAnswerRadiobutton(string text)
        {
            var r = new RadioButton() { Enabled = false, Text = text };
            _radioButtons.Add(r);
            _flowLayoutPanel.AddControlToFlowLayoutPanel(r);
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

        private void OnButtonCreateAnotherQuestionClick(object sender, EventArgs e)
        {
            ResetInputValues();
            ReinitializeControls();
        }
    }
}
