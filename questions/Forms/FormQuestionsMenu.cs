using Domain;
using Infrastructure;
using Presentation.Forms;
using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    public class FormQuestionsMenu : FormQuestionsBase, ICategoriesListBoxForm, IAnswerRadioButtonsForm
    {
        #region INITIALIZING
        private ListBox _listBox = new();
        public ListBox ListBoxForCategories { get { return _listBox; } set { _listBox = new(); } }

        private List<RadioButton> _radioButtons = new();
        public List<RadioButton> RadioButtonsForAnswers { get { return _radioButtons; } set { _radioButtons = new(); } }

        private readonly Button _buttonAnswerQuestionsFromAvailableCategory = new();
        private readonly Button _buttonAddNewQuestion = new();
        private readonly Button _buttonForPickingAnswers = new();

        protected Button _buttonDisplayMenuOptions = new();
        private readonly Button _buttonChangeCategory = new();

        protected override void InitializeControls()
        {
            ListBoxForCategories.Visible = false;
            ListBoxForCategories.SelectionMode = SelectionMode.One;
            _flowLayoutPanel.Controls.Add(ListBoxForCategories);
            ListBoxForCategories.SelectedIndexChanged += OnListBoxForCategoriesSelectedIndexChanged!;

            _buttonAnswerQuestionsFromAvailableCategory.Visible = false;
            _flowLayoutPanel.Controls.Add(_buttonAnswerQuestionsFromAvailableCategory);
            _buttonAnswerQuestionsFromAvailableCategory.Click += OnButtonAnswerQuestionsFromAvailableCategoryClick!;

            _buttonForPickingAnswers.Text = ButtonTexts.CheckAnswer;
            _buttonForPickingAnswers.Visible = false;
            _buttonForPickingAnswers.Click += OnButtonForPickingAnswersClick!;
            _flowLayoutPanel.Controls.Add(_buttonForPickingAnswers);

            _buttonAddNewQuestion.Text = ButtonTexts.AddNewQuestion;
            _flowLayoutPanel.Controls.Add(_buttonAddNewQuestion);
            _buttonAddNewQuestion.Click += OnButtonAddNewQuestionClick!;

            _buttonDisplayMenuOptions.Text = ButtonTexts.ReturnToMenu;
            _flowLayoutPanel.Controls.Add(_buttonDisplayMenuOptions);
            _buttonDisplayMenuOptions.Click += DisplayMenuOptions!;

            _buttonChangeCategory.Text = ButtonTexts.ChangeCategory;
            _buttonChangeCategory.Visible = false;
            _flowLayoutPanel.Controls.Add(_buttonChangeCategory);
            _buttonChangeCategory.Click += OnButtonChangeCategoryClick!;
        }

        public FormQuestionsMenu()
        {
            DisplayMenuOptions();
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            InitializeControls();
        }
        #endregion

        private bool CategoryIsChosenInListBox => DataHandler.SelectedCategory is null && ListBoxForCategories.SelectedIndex != -1;
        private List<Question> AvailableQuestions => DataHandler.Repo.GetAvailableQuestions().Where(q => q.QuestionCategory.Id == DataHandler.SelectedCategory?.Id).ToList();

        protected override void DisplayMenuOptions(object? sender = default, EventArgs? e = default)
        {
            DisplayMenuControls();
            DataHandler.ResetSelectedCategoryToDefault();

            var availableCategories = DataHandler.GetAvailableCategories();
            if (availableCategories.Count > 0)
            {
                (this as ICategoriesListBoxForm).AddCategoriesToListBox(availableCategories);

                _label.Text = LabelTexts.AvailableCategoriesAndQuestions;
                DisplayControls(ListBoxForCategories, _label, _buttonAddNewQuestion);
            }
            else
            {
                HideControls(ListBoxForCategories);
                _label.Text = LabelTexts.NoQuestionsAvailable;
                DisplayControls(_buttonAddNewQuestion);
            }
        }

        protected override void DisplayMenuControls()
        {
            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == _buttonAddNewQuestion || c == ListBoxForCategories || c == _label)
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
            DataHandler.ResetQuestionDTOToDefault();
        }

        /// <summary>
        /// Displays the button for choosing a category with available questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnListBoxForCategoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ListBoxForCategories.SelectedIndex;

            if (selectedIndex != -1)
            {
                _buttonAnswerQuestionsFromAvailableCategory.Text = ButtonTexts.AnswerAllQuestionsFromCategory;
                DisplayControls(_buttonAnswerQuestionsFromAvailableCategory);
            }
        }

        /// <summary>
        /// Sets selected category and offers to answer all available questions in the category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAnswerQuestionsFromAvailableCategoryClick(object sender, EventArgs e)
        {
            RemoveControlFromFlowLayoutPanel(RadioButtonsForAnswers);

            if (CategoryIsChosenInListBox)
            {
                DataHandler.SetNewSelectedCategory(DataHandler.GetAvailableCategories()[ListBoxForCategories.SelectedIndex]);
            }

            if (DataHandler.SelectedCategory is not null)
            {
                HideControls(ListBoxForCategories);

                if (AvailableQuestions.Count > 0)
                {
                    DataHandler.SetSelectedQuestion(AvailableQuestions[0]);
                    if (DataHandler.SelectedQuestion is not null)
                    {
                        _label.Text = string.Format(LabelTexts.ChooseAnswer, DataHandler.SelectedQuestion.Text);

                        HideControls(_buttonAddNewQuestion, _buttonAnswerQuestionsFromAvailableCategory);
                        (this as IAnswerRadioButtonsForm).DisplayAnswersOfQuestionOrDTO();

                        DisplayControlBelowOthersInFlowPanel(_buttonChangeCategory);
                        DisplayControlBelowOthersInFlowPanel(_buttonDisplayMenuOptions);
                    }
                }
                else
                {
                    DataHandler.ResetSelectedCategoryToDefault();
                    HideControls(_buttonForPickingAnswers, _buttonAnswerQuestionsFromAvailableCategory);
                }
            }
        }

        /// <summary>
        /// Displays the button to accept answer input 
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
                        r.ForeColor = Color.Orange;
                    }
                    else
                    {
                        r.ForeColor = SystemColors.ControlText;
                    }
                }
                DisplayControls(_buttonForPickingAnswers);
            }
        }

        /// <summary>
        /// Checks if the answer input is correct and sets new availability datetime for the selected question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonForPickingAnswersClick(object sender, EventArgs e)
        {
            bool isAnswerCorrect = DataHandler.IsAnswerCorrect(_answerInput);
            DataHandler.CheckAnswer(isAnswerCorrect);
            DisplayAnswerAttemptResult(isAnswerCorrect);

            HideControls(_buttonForPickingAnswers);

            if (DataHandler.SelectedCategory is not null)
            {
                if (AvailableQuestions.Count > 0)
                {
                    _buttonAnswerQuestionsFromAvailableCategory.Text = ButtonTexts.AnswerNextQuestion;
                    DisplayControls(_buttonAnswerQuestionsFromAvailableCategory);
                }
                else
                {
                    _label.Text += string.Format(LabelTexts.NoMoreAvailableQuestionsInCategory, DataHandler.SelectedCategory.Title);
                    HideControls(_buttonAnswerQuestionsFromAvailableCategory, _buttonChangeCategory);
                }
            }

            DisplayControlBelowOthersInFlowPanel(_buttonDisplayMenuOptions);
        }

        public void DisplayAnswersOfQuestionOrDTO()
        {
            if (DataHandler.SelectedQuestion is not null)
            {
                for (int i = 0; i < DataHandler.SelectedQuestion.Answers.Count; i++)
                {
                    (this as IAnswerRadioButtonsForm).CreateAnswerRadiobutton(DataHandler.SelectedQuestion.Answers[i].Text);
                    (this as IAnswerRadioButtonsForm).EnableAnswerRadioButton(RadioButtonsForAnswers.Last());
                    DisplayControls(RadioButtonsForAnswers.ToArray());
                }
            }
        }

        /// <summary>
        /// Displays whether the answer is correct or incorrect 
        /// </summary>
        private void DisplayAnswerAttemptResult(bool answerIsCorrect)
        {
            HideControls(_buttonChangeCategory);
            RemoveControlFromFlowLayoutPanel(RadioButtonsForAnswers);
            RadioButtonsForAnswers.Clear();

            _label.Text = (answerIsCorrect ? LabelTexts.CorrectAnswer : LabelTexts.WrongAnswer) + string.Format(LabelTexts.WhenQuestionIsAvailable, DataHandler.SelectedQuestion?.AvailableAt);
        }

        /// <summary>
        /// Opens a form for creating new question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAddNewQuestionClick(object sender, EventArgs e)
        {
            var creatingForm = new FormCreateOrEditQuestion(this);
            creatingForm.FormClosing += OnFormsForEditingQuestionClosing!;
            creatingForm.ShowDialog();
        }

        /// <summary>
        /// Opens a form for changing category of the selected question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonChangeCategoryClick(object sender, EventArgs e)
        {
            var categoriesForm = new FormAddCategory(this);
            categoriesForm.FormClosing += OnFormsForEditingQuestionClosing!;
            categoriesForm.ShowDialog();
        }

        /// <summary>
        /// Get user to the form main menu when another form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormsForEditingQuestionClosing(object sender, FormClosingEventArgs e)
        {
            int defaultCapacity = QuestionRepository.DefaultCategory.Questions.Count;

            ResetInputValues();
            DisplayMenuOptions();
        }
    }
}
