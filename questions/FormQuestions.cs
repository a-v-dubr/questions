using BusinessLogic;
using Domain;
using Infrastructure;
using Presentation.DTOClasses;
using static Presentation.Helper.ControlMessages;
using static Presentation.Helper.Validator;

namespace Presentation
{
    public partial class FormQuestions : Form
    {
        private readonly QuestionRepository _repo = new();
        private QuestionRepetitionsHandler? _handler;
        private QuestionDTO? _questionDTO;
        private RadioButton? _input;
        private Question? _selectedQuestion;
        private Category? _selectedCategory;
        private readonly List<Category> _categories = new();
        private int _answerInputCounter = 0;
        private bool _questionCreatingInProcess = false;

        public FormQuestions()
        {
            InitializeComponent();
            InitializeControls();

            _repo.RetrieveQuestionsFromDb();
            _categories = _repo.Select(q => q.QuestionCategory).Distinct().ToList();
        }

        #region ADDING QUESTIONS & CATEGORIES
        /// <summary>
        /// Displays buttons to create new category or choose existing category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddNewQuestion_Click(object sender, EventArgs e)
        {
            _questionCreatingInProcess = true;
            HideControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions, _buttonAddNewQuestion);
            if (_categories.Count > 0)
            {
                _labelUserActionsHelper.Text = LabelTexts.ChooseOrCreateCategory;
                DisplayControls(_buttonCreateNewCategory, _buttonChooseExistingCategory);
            }
            else
            {
                _labelUserActionsHelper.Text = LabelTexts.CreateCategory;
                DisplayControls(_buttonCreateNewCategory);
            }
        }

        /// <summary>
        /// Diplays textbox to create category's title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreateNewCategory_Click(object sender, EventArgs e)
        {
            _labelUserActionsHelper.Text = LabelTexts.CreateCategory;
            HideControls(_buttonCreateNewCategory, _buttonChooseExistingCategory);
            DisplayControls(_textBoxForCreatingCategory);
        }

        /// <summary>
        /// Diplays button to accept user input for category title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxForCreatingCategory_TextChanged(object sender, EventArgs e)
        {
            DisplayControls(_buttonSaveCategoryTitle);
        }

        /// <summary>
        /// Displays textbox for adding new category's questions if user input for category's title is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveCategoryTitle_Click(object sender, EventArgs e)
        {
            if (UserInputIsValid(_textBoxForCreatingCategory.Text))
            {
                _selectedCategory = new Category(_textBoxForCreatingCategory.Text);
                if (!_categories.Any(c => c.Id == _selectedCategory.Id))
                {
                    _categories.Add(_selectedCategory);
                }

                _textBoxForCreatingCategory.Clear();
                HideControls(_textBoxForCreatingCategory, _buttonSaveCategoryTitle);

                _labelUserActionsHelper.Text = string.Format(LabelTexts.CreateQuestion, _selectedCategory.Title);
                DisplayControls(_labelUserActionsHelper, _textBoxForQuestionInput);
            }
        }

        /// <summary>
        /// Diplays button to accept user input for question's text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxForQuestionInput_TextChanged(object sender, EventArgs e)
        {
            DisplayControls(_buttonAcceptQuestionText);
        }

        /// <summary>
        /// Displays textbox for adding answers' texts if the question's text is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAcceptQuestionText_Click(object sender, EventArgs e)
        {
            if (UserInputIsValid(_textBoxForQuestionInput.Text))
            {
                _questionDTO = new QuestionDTO { QuestionCategory = _selectedCategory, QuestionText = _textBoxForQuestionInput.Text };

                _textBoxForQuestionInput.Clear();
                HideControls(_textBoxForQuestionInput, _buttonAcceptQuestionText);

                _labelUserActionsHelper.Text = string.Format(LabelTexts.DisplayQuestionWhileAddingAnswers, _questionDTO.QuestionText);
                DisplayControls(_textBoxForAnswerInput, _buttonAcceptAnswerText);
            }
        }

        /// <summary>
        /// Gets user input for unique answer texts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAcceptAnswerText_Click(object sender, EventArgs e)
        {
            HideControls(_labelErrorMessages);

            if (_questionDTO is not null)
            {
                if (UserInputIsValid(_textBoxForAnswerInput.Text))
                {
                    _questionDTO.AnswersTexts.Add(_textBoxForAnswerInput.Text);
                    var r = new RadioButton() { Enabled = false, Text = _textBoxForAnswerInput.Text };
                    _radioButtonsForPickingAnswer.Add(r);
                    AddControlToFlowLayoutPanel(r);
                    _textBoxForAnswerInput.Clear();
                    _answerInputCounter++;
                }
            }

            if (_answerInputCounter == Question.MinAnswersCount)
            {
                DisplayControls(_buttonFinishAddingAnswers);
            }
        }

        /// <summary>
        /// Enables radiobuttons for picking correct answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFinishAddingAnswers_Click(object sender, EventArgs e)
        {
            HideControls(_buttonFinishAddingAnswers);
            if (_questionDTO is not null)
            {
                if (AnswerTextsAreUnique(_questionDTO.AnswersTexts))
                {
                    _textBoxForAnswerInput.Clear();
                    _answerInputCounter = default;

                    HideControls(_buttonAcceptAnswerText, _textBoxForAnswerInput);
                    foreach (var r in _radioButtonsForPickingAnswer)
                    {
                        r.CheckedChanged += RadiobuttonToSetCorrectAnswer_CheckedChanged!;
                        r.Enabled = true;                       
                    }
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.SetCorrectAnswer, _questionDTO.QuestionText);
                }
                else
                {
                    _labelErrorMessages.Text = LabelTexts.DuplicateQuestionsError;
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.DisplayQuestionWhileAddingAnswers, _questionDTO.QuestionText);
                    _questionDTO.AnswersTexts.Clear();
                    HideControls(_radioButtonsForPickingAnswer.ToArray());
                    _radioButtonsForPickingAnswer.Clear();
                    _answerInputCounter = default;
                    DisplayControls(_labelErrorMessages, _textBoxForAnswerInput, _buttonAcceptAnswerText);
                }
            }
        }

        /// <summary>
        /// Displays button to pick correct answer option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadiobuttonToSetCorrectAnswer_CheckedChanged(object sender, EventArgs e)
        {
            _input = (RadioButton)sender;
            if (_input.Checked)
            {
                DisplayControls(_buttonSaveCorrectAnswerIndex);
            }
        }

        /// <summary>
        /// Creates and saves question instance and then gets user to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveCorrectAnswerIndex_Click(object sender, EventArgs e)
        {
            if (_questionDTO is not null && _input is not null)
            {
                _questionDTO.CorrectAnswerIndex = _questionDTO.AnswersTexts.FindIndex(a => a == _input.Text);
                var question = _questionDTO.MapDTO();

                if (question is not null)
                {
                    _repo.AddQuestionToRepository(question);
                    _repo.AddQuestionToDb(question);

                    _questionCreatingInProcess = false;
                    HideControls(_radioButtonsForPickingAnswer.ToArray());
                    _radioButtonsForPickingAnswer.Clear();
                    HideControls(_buttonSaveCorrectAnswerIndex);                    
                    _labelUserActionsHelper.Text = LabelTexts.QuestionIsSavedAndAvailable;
                }
            }
            DisplayControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions);
        }

        /// <summary>
        /// Displays combobox for picking a category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChooseExistingCategory_Click(object sender, EventArgs e)
        {
            HideControls(_buttonCreateNewCategory, _buttonChooseExistingCategory);
            _labelUserActionsHelper.Text = LabelTexts.AvailableCategories;

            foreach (var c in _categories)
            {
                _comboBoxChooseAvailableCategory.Items.Add(c.Title);
            }

            DisplayControls(_comboBoxChooseAvailableCategory);
        }

        /// <summary>
        /// Diplays button for accepting category choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxChooseAvailableCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayControls(_buttonAcceptCategoryChoice);
        }

        /// <summary>
        /// Displays controls to add new question or choose existing question in the chosen category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAcceptCategoryChoice_Click(object sender, EventArgs e)
        {
            var s = _comboBoxChooseAvailableCategory.SelectedItem.ToString();

            if (_categories.FirstOrDefault(c => c.Title == s) is not null)
            {
                _selectedCategory = _categories.FirstOrDefault(c => c.Title == s);
            }

            _comboBoxChooseAvailableCategory.Items.Clear();
            _comboBoxChooseAvailableCategory.Text = string.Empty;
            HideControls(_comboBoxChooseAvailableCategory, _buttonChooseExistingCategory, _buttonAcceptCategoryChoice);

            if (_selectedCategory is not null && _questionCreatingInProcess)
            {
                _labelUserActionsHelper.Text = string.Format(LabelTexts.CreateQuestion, _selectedCategory.Title);
                DisplayControls(_textBoxForQuestionInput);
            }

            if (_selectedCategory is not null && !_questionCreatingInProcess)
            {
                _labelUserActionsHelper.Text = string.Format(LabelTexts.ChooseQuestionInCategory, _selectedCategory.Title);
                var availableQuestions = _repo.GetAvailableQuestions().Where(q => q.QuestionCategory.Id == _selectedCategory.Id);
                foreach (var q in availableQuestions)
                {
                    _comboBoxChooseAvailableQuestion.Items.Add(q.Text);
                }
                DisplayControls(_comboBoxChooseAvailableQuestion);
            }
        }
        #endregion

        #region DISPLAY AVAILABLE QUESTIONS
        /// <summary>
        /// Displays categories which are available for user and asks for category input if at least one category is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDisplayAvailableQuestions_Click(object sender, EventArgs e)
        {
            var availableQuestions = _repo.GetAvailableQuestions();
            if (availableQuestions.Any())
            {
                var availableCategories = new List<Category>();
                foreach (var c in _categories)
                {
                    if (c is not null && availableQuestions.Any(q => q.CategoryId == c.Id))
                    {
                        availableCategories.Add(c);
                    }
                }

                foreach (var c in availableCategories)
                {
                    _comboBoxChooseAvailableCategory.Items.Add(c.Title);
                }

                _labelUserActionsHelper.Text = LabelTexts.AvailableCategories;
                HideControls(_buttonDisplayAvailableQuestions, _buttonAddNewQuestion);
                DisplayControls(_comboBoxChooseAvailableCategory);
            }
            else
            {
                _labelUserActionsHelper.Text = LabelTexts.NoQuestionsAvailable;
            }
        }

        /// <summary>
        /// Displays button to confirm question choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxChooseAvailableQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayControls(_buttonChooseAvailableQuestion);
        }

        /// <summary>
        /// Displays answer options for the selected question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChooseAvailableQuestion_Click(object sender, EventArgs e)
        {
            if (_selectedCategory is not null)
            {
                _selectedQuestion = _repo.FirstOrDefault(q => q.Text == _comboBoxChooseAvailableQuestion.SelectedItem.ToString() && q.CategoryId == _selectedCategory.Id);

                if (_selectedQuestion is not null)
                {
                    _comboBoxChooseAvailableQuestion.Items.Clear();
                    _comboBoxChooseAvailableQuestion.Text = string.Empty;
                    HideControls(_buttonChooseAvailableQuestion, _comboBoxChooseAvailableQuestion);
                    _labelUserActionsHelper.Text = _selectedQuestion.Text;

                    for (int i = 0; i < _selectedQuestion.Answers.Count; i++)
                    {
                        var r = new RadioButton() { Text = _selectedQuestion.Answers[i].Text };
                        _radioButtonsForPickingAnswer.Add(r);
                        AddControlToFlowLayoutPanel(r);
                        r.Visible = true;
                        r.CheckedChanged += RadioButtonsForPickingAnswer_CheckedChanged!;
                    }

                    DisplayControls(_radioButtonsForPickingAnswer.ToArray());
                }
            }
        }

        /// <summary>
        /// Displays button for accepting answer when an anwer option radiobutton is picked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButtonsForPickingAnswer_CheckedChanged(object sender, EventArgs e)
        {
            _input = (RadioButton)sender;
            if (_input.Checked)
            {
                DisplayControls(_buttonAcceptAnswerInput);
            }
        }

        /// <summary>
        /// Reduces or resets next time for displaying question according to correct or incorrect user answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAcceptAnswerInput_Click(object sender, EventArgs e)
        {            
            HideControls(_radioButtonsForPickingAnswer.ToArray());
            _radioButtonsForPickingAnswer.Clear();
            HideControls(_buttonAcceptAnswerInput);
            DisplayControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions);

            if (_selectedQuestion is not null && _input is not null)
            {
                _handler = new(_selectedQuestion, _repo);
                if (_selectedQuestion.Answers.FirstOrDefault(a => a.Text == _input.Text && a.IsCorrect) is not null)
                {
                    _handler.ReduceRepetitions();
                    _labelUserActionsHelper.Text = LabelTexts.CorrectAnswer + string.Format(LabelTexts.WhenQuestionIsAvailable, _selectedQuestion.AvailableAt);
                }
                else
                {
                    _handler.ResetRepetitions();
                    _labelUserActionsHelper.Text = LabelTexts.WrongAnswer + string.Format(LabelTexts.WhenQuestionIsAvailable, _selectedQuestion.AvailableAt);
                }
            }
        }
        #endregion
    }
}