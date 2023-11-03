using Domain;
using Infrastructure;
using Presentation.Forms;
using static Presentation.Helper.ControlMessages;
using static Presentation.Helper.Validator;

namespace Presentation
{
    public class FormCreateOrEditQuestion : FormQuestionsBase, ITitlesTextBox, IAnswerRadioButtonsForm
    {
        #region INITIALIZING
        private TextBox _textBoxForQuestionTitle = new();
        public TextBox TextBox { get { return _textBoxForQuestionTitle; } set { _textBoxForQuestionTitle = new(); } }

        private List<RadioButton> _radioButtons = new();
        public List<RadioButton> RadioButtonsForAnswers { get { return _radioButtons; } set { _radioButtons = new(); } }

        private readonly Button _buttonAcceptQuestionText = new();
        private readonly Button _buttonFinishAddingAnswers = new();
        private readonly Button _buttonChangeQuestionCategory = new();
        private readonly Button _buttonDisplayMenu = new();

        public FormCreateOrEditQuestion(FormQuestionsBase form)
        {
            DataHandler = form.DataHandler;
            DisplayMenuOptions();
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();

            _flowLayoutPanel.Controls.Add(_textBoxForQuestionTitle);
            TextBox.TextChanged += OnTextBoxTextChanged!;

            _flowLayoutPanel.Controls.Add(_buttonAcceptQuestionText);
            _buttonAcceptQuestionText.Visible = false;
            _buttonAcceptQuestionText.Enabled = false;
            _buttonAcceptQuestionText.Click += OnButtonAcceptQuestionTextClick!;

            _flowLayoutPanel.Controls.Add(_buttonFinishAddingAnswers);
            _buttonFinishAddingAnswers.Visible = false;
            _buttonFinishAddingAnswers.Enabled = false;
            _buttonFinishAddingAnswers.Click += OnButtonFinishAddingAnswersClick!;

            _flowLayoutPanel.Controls.Add(_buttonChangeQuestionCategory);
            _buttonChangeQuestionCategory.Text = ButtonTexts.ChooseNewCategory;
            _buttonChangeQuestionCategory.Visible = false;
            _buttonChangeQuestionCategory.Click += OnButtonChangeQuestionCategoryClick!;

            _flowLayoutPanel.Controls.Add(_buttonDisplayMenu);
            _buttonDisplayMenu.Text = ButtonTexts.ContinueCreatingQuestions;
            _buttonDisplayMenu.Visible = false;
            _buttonDisplayMenu.Click += OnButtonDisplayMenu!;
        }
        #endregion

        private bool QuestionTextAndAnswersReceived => DataHandler.QuestionDTO is not null && DataHandler.SelectedQuestion is null && _answerInput is null;
        private bool QuestionDTOIsReadyToMapping => DataHandler.QuestionDTO is not null && DataHandler.SelectedQuestion is null && _answerInput is not null;

        protected override void DisplayMenuOptions(object? sender = null, EventArgs? e = null)
        {
            ResetInputValues();
            DisplayMenuControls();

            _label.Text = LabelTexts.TypeQuestionText;

            if (DataHandler.SelectedQuestion is null)
            {
                _buttonAcceptQuestionText.Text = ButtonTexts.AcceptQuestionText;
                DisplayControls(TextBox);
            }
        }

        protected override void DisplayMenuControls()
        {
            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == TextBox || c == _label)
                {
                    DisplayControls(c);
                }
                else
                {
                    HideControls(c);
                }
            }
        }

        protected override void ResetInputValues()
        {
            DataHandler.ResetSelectedQuestionToDefault();
            DataHandler.ResetSelectedCategoryToDefault();
            DataHandler.ResetQuestionDTOToDefault();
            _answerInput = default;
            _answerInputCounter = default;
        }

        /// <summary>
        /// Enables button for accepting text input or disables button if input is incorrect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            if (UserInputIsValid(TextBox.Text))
            {
                _acceptTextBoxTextChanges = true;
                _buttonAcceptQuestionText.Enabled = true;
                DisplayControls(_buttonAcceptQuestionText);
            }

            if (!UserInputIsValid(TextBox.Text))
            {
                _acceptTextBoxTextChanges = false;
                _buttonAcceptQuestionText.Enabled = false;
            }
        }

        /// <summary>
        /// Handles question DTO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAcceptQuestionTextClick(object sender, EventArgs e)
        {
            if (DataHandler.QuestionDTO is null)
            {
                DataHandler.SetQuestionDTO(new() { QuestionCategory = QuestionRepository.DefaultCategory, QuestionText = TextBox.Text });
                TextBox.Clear();
            }

            if (DataHandler.QuestionDTO is not null)
            {
                _label.Text = string.Format(LabelTexts.DisplayQuestionWhileAddingAnswers, DataHandler.QuestionDTO.QuestionText);
                _buttonAcceptQuestionText.Text = ButtonTexts.AcceptAnswerText;

                if (_acceptTextBoxTextChanges && DataHandler.QuestionDTO.TryAddAnswerText(TextBox.Text))
                {
                    (this as IAnswerRadioButtonsForm).CreateAnswerRadiobutton(TextBox.Text);
                    TextBox.Clear();
                    _answerInputCounter++;
                }

                if (_answerInputCounter >= Question.MinAnswersCount)
                {
                    DisplayControls(_buttonFinishAddingAnswers);
                    _buttonFinishAddingAnswers.Text = ButtonTexts.FinishAddingAnswers;
                    _buttonFinishAddingAnswers.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Creates new question instance and gets user to the form menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonFinishAddingAnswersClick(object sender, EventArgs e)
        {
            if (QuestionTextAndAnswersReceived)
            {
                DisplayAnswersOfQuestionOrDTO();
                HideControls(TextBox, _buttonFinishAddingAnswers);
                RadioButtonsForAnswers.ForEach((this as IAnswerRadioButtonsForm).EnableAnswerRadioButton);
                _label.Text = string.Format(LabelTexts.ChooseCorrectAnswer, DataHandler?.QuestionDTO?.QuestionText);
                _buttonAcceptQuestionText.Text = ButtonTexts.AcceptCorrectAnswerInput;
            }

            if (QuestionDTOIsReadyToMapping)
            {
                DataHandler!.QuestionDTO!.CorrectAnswerIndex = DataHandler.QuestionDTO.AnswersTexts.FindIndex(a => a == _answerInput?.Text);
                var question = DataHandler.QuestionDTO.MapDTO();

                if (question is not null)
                {
                    DataHandler.AddNewQuestion(question);
                    DataHandler.SetSelectedQuestion(question);

                    _label.Text = string.Format(LabelTexts.QuestionIsSavedAndAvailable, DataHandler.SelectedQuestion?.Text);

                    RemoveControlFromFlowLayoutPanel(RadioButtonsForAnswers);
                    RadioButtonsForAnswers.Clear();

                    HideControls(_buttonFinishAddingAnswers, _buttonAcceptQuestionText);
                    DisplayControls(_buttonChangeQuestionCategory, _buttonDisplayMenu);
                }
            }
        }

        private void OnButtonDisplayMenu(object sender, EventArgs e)
        {
            ResetInputValues();
            DisplayMenuOptions();
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
                foreach (var r in RadioButtonsForAnswers)
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
                _buttonFinishAddingAnswers.Text = ButtonTexts.AcceptCorrectAnswerInput;
                _buttonFinishAddingAnswers.Enabled = true;
                HideControls(_buttonAcceptQuestionText);
                DisplayControls(_buttonFinishAddingAnswers);
            }
        }

        /// <summary>
        /// Opens a form for creating or choosing the question category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonChangeQuestionCategoryClick(object sender, EventArgs e)
        {
            var categoriesForm = new FormAddCategory(this);
            categoriesForm.FormClosing += OnFormsForEditingQuestionClosing!;
            categoriesForm.ShowDialog();
        }

        public void DisplayAnswersOfQuestionOrDTO()
        {
            if (DataHandler.QuestionDTO is not null)
            {
                if (AnswerTextsAreUnique(DataHandler.QuestionDTO.AnswersTexts))
                {
                    foreach (var r in RadioButtonsForAnswers)
                    {
                        (this as IAnswerRadioButtonsForm).EnableAnswerRadioButton(r);
                    }
                    _label.Text = string.Format(LabelTexts.ChooseCorrectAnswer, DataHandler.QuestionDTO.QuestionText);
                }
            }
        }

        private void OnFormsForEditingQuestionClosing(object sender, EventArgs e)
        {
            DataHandler.Repo.RetrieveQuestionsFromDb();
            DisplayMenuOptions();
        }
    }
}
