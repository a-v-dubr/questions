using Domain;
using static Presentation.Helper.ControlMessages;
using Presentation.Helper;
using Infrastructure;

namespace Presentation.Forms
{
    public partial class AddQuestionsForm : Form
    {
        private readonly DataHandler _dataHandler;
        protected RadioButton? _answerInput;
        protected int _answerInputCounter = 0;
        protected bool _acceptTextBoxTextChanges = false;

        private bool QuestionTextAndAnswersReceived => _dataHandler.QuestionDTO is not null && _dataHandler.SelectedQuestion is null && _answerInput is null;
        private bool QuestionDTOIsReadyToMapping => _dataHandler.QuestionDTO is not null && _dataHandler.SelectedQuestion is null && _answerInput is not null;


        public AddQuestionsForm(DataHandler dataHandler)
        {
            InitializeComponent();
            _dataHandler = dataHandler;
            DisplayMenuOptions();
        }       
        
        private void DisplayMenuOptions(object? sender = null, EventArgs? e = null)
        {
            ResetInputValues();
            DisplayMenuControls();            

            if (_dataHandler.SelectedQuestion is null)
            {
                _buttonAcceptQuestionText.Text = ButtonTexts.AcceptQuestionText;
                ControlsHelper.DisplayControls(_textBoxForQuestionTitle);
            }
        }

        private void DisplayMenuControls()
        {
            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == _textBoxForQuestionTitle || c == _label)
                {
                    ControlsHelper.DisplayControls(c);
                }
                else
                {
                    ControlsHelper.HideControls(c);
                }
            }
        }

       private void ResetInputValues()
        {
            _dataHandler.ResetSelectedQuestionToDefault();
            _dataHandler.ResetSelectedCategoryToDefault();
            _dataHandler.ResetQuestionDTOToDefault();
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
            if (Validator.UserInputIsValid(_textBoxForQuestionTitle.Text))
            {
                _acceptTextBoxTextChanges = true;
                _buttonAcceptQuestionText.Enabled = true;
                ControlsHelper.DisplayControls(_buttonAcceptQuestionText);
            }

            if (!Validator.UserInputIsValid(_textBoxForQuestionTitle.Text))
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
            if (_dataHandler.QuestionDTO is null)
            {
                _dataHandler.SetQuestionDTO(new() { QuestionCategory = QuestionRepository.DefaultCategory, QuestionText = _textBoxForQuestionTitle.Text });
                _textBoxForQuestionTitle.Clear();
            }

            if (_dataHandler.QuestionDTO is not null)
            {
                _label.Text = string.Format(LabelTexts.DisplayQuestionWhileAddingAnswers, _dataHandler.QuestionDTO.QuestionText);
                _buttonAcceptQuestionText.Text = ButtonTexts.AcceptAnswerText;

                if (_acceptTextBoxTextChanges && _dataHandler.QuestionDTO.TryAddAnswerText(_textBoxForQuestionTitle.Text))
                {
                    CreateAnswerRadiobutton(_textBoxForQuestionTitle.Text);
                    _textBoxForQuestionTitle.Clear();
                    _answerInputCounter++;
                }

                if (_answerInputCounter >= Question.MinAnswersCount)
                {
                    ControlsHelper.DisplayControls(_buttonFinishAddingAnswers);
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
                ControlsHelper.HideControls(_textBoxForQuestionTitle, _buttonFinishAddingAnswers);
                _radioButtons.ForEach(EnableAnswerRadioButton);
                _label.Text = string.Format(LabelTexts.ChooseCorrectAnswer, _dataHandler?.QuestionDTO?.QuestionText);
                _buttonAcceptQuestionText.Text = ButtonTexts.AcceptCorrectAnswerInput;
            }

            if (QuestionDTOIsReadyToMapping)
            {
                _dataHandler!.QuestionDTO!.CorrectAnswerIndex = _dataHandler.QuestionDTO.AnswersTexts.FindIndex(a => a == _answerInput?.Text);
                var question = _dataHandler.QuestionDTO.MapDTO();

                if (question is not null)
                {
                    _dataHandler.AddNewQuestion(question);
                    _dataHandler.SetSelectedQuestion(question);

                    _label.Text = string.Format(LabelTexts.QuestionIsSavedAndAvailable, _dataHandler.SelectedQuestion?.Text);

                    ControlsHelper.RemoveControlFromFlowLayoutPanel(_radioButtons, _flowLayoutPanel);
                    _radioButtons.Clear();

                    ControlsHelper.HideControls(_buttonFinishAddingAnswers, _buttonAcceptQuestionText);
                    ControlsHelper.DisplayControls(_buttonChangeQuestionCategory, _buttonDisplayMenu);
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
                _buttonFinishAddingAnswers.Text = ButtonTexts.AcceptCorrectAnswerInput;
                _buttonFinishAddingAnswers.Enabled = true;
                ControlsHelper.HideControls(_buttonAcceptQuestionText);
                ControlsHelper.DisplayControls(_buttonFinishAddingAnswers);
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
            categoriesForm.FormClosing += OnFormsForEditingQuestionClosing!;
            categoriesForm.ShowDialog();
        }

        public void DisplayAnswersOfQuestionOrDTO()
        {
            if (_dataHandler.QuestionDTO is not null)
            {
                if (Validator.AnswerTextsAreUnique(_dataHandler.QuestionDTO.AnswersTexts))
                {
                    foreach (var r in _radioButtons)
                    {
                        EnableAnswerRadioButton(r);
                    }
                    _label.Text = string.Format(LabelTexts.ChooseCorrectAnswer, _dataHandler.QuestionDTO.QuestionText);
                }
            }
        }

        private void OnFormsForEditingQuestionClosing(object sender, EventArgs e)
        {
            _dataHandler.Repo.RetrieveQuestionsFromDb();
            DisplayMenuOptions();
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
    }
}
