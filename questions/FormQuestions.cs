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
        private QuestionsEditor? _editor;
        private QuestionDTO? _questionDTO;
        private RadioButton? _answerInput;
        private Question? _selectedQuestion;
        private Category? _selectedCategory;
        private readonly List<Category> _categories = new();
        private int _answerInputCounter = 0;
        private bool _questionCreatingInProcess = false;
        private bool _questionEditingInProcess = false;

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

            HideMainMenuControls();
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
            DisplayButtonReturnToMainMenu();
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
        /// Displays textbox for adding new category's questions if category has been created successfully
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveCategoryTitle_Click(object sender, EventArgs e)
        {
            if (TryCreateCategory(_textBoxForCreatingCategory.Text) && _selectedCategory is not null)
            {
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
            if (_questionEditingInProcess && _textBoxForQuestionInput.Visible == true && UserInputIsValid(_textBoxForQuestionInput.Text))
            {
                DisplayControls(_buttonSaveTextsChanges);
            }
            if (_questionCreatingInProcess && _textBoxForQuestionInput.Visible == true)
            {
                DisplayControls(_buttonAcceptQuestionText);
            }
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
                    CreateAnswerRadiobutton(_textBoxForAnswerInput.Text);
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
        /// Hides controls and deletes them from the control's list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controls"></param>
        private static void HideAndClearControls<T>(List<T> controls) where T : Control
        {
            if (controls.Count > 0)
            {
                foreach (var c in controls)
                {
                    if (c.Visible)
                    {
                        c.Visible = false;
                    }
                }
                controls.Clear();
            }
        }

        /// <summary>
        /// Enables radiobuttons for picking correct answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFinishAddingAnswers_Click(object sender, EventArgs e)
        {
            HideControls(_buttonFinishAddingAnswers, _buttonSaveTextsChanges, _textBoxForQuestionInput);

            if (_questionDTO is not null)
            {
                if (AnswerTextsAreUnique(_questionDTO.AnswersTexts))
                {
                    _textBoxForAnswerInput.Clear();
                    _answerInputCounter = default;

                    HideControls(_buttonAcceptAnswerText, _textBoxForAnswerInput);

                    foreach (var r in _radioButtonsForPickingAnswer)
                    {
                        EnableAnswerRadioButtonToSetCorrectAnswer(r);
                    }

                    _labelUserActionsHelper.Text = string.Format(LabelTexts.SetCorrectAnswer, _questionDTO.QuestionText);
                }
                else
                {
                    _labelErrorMessages.Text = LabelTexts.DuplicateQuestionsError;
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.DisplayQuestionWhileAddingAnswers, _questionDTO.QuestionText);

                    _questionDTO.AnswersTexts.Clear();
                    _answerInputCounter = default;

                    HideAndClearControls(_radioButtonsForPickingAnswer);
                    foreach (var r in _radioButtonsForPickingAnswer)
                    {
                        RemoveControlFromFlowLayoutPanel(r);
                    }

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
            _answerInput = (RadioButton)sender;
            if (_answerInput.Checked)
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
            HideControls(_buttonSaveTextsChanges);
            HideAndClearControls(_radioButtonsForPickingAnswer);
            RemoveControlFromFlowLayoutPanel(_radioButtonsForPickingAnswer);

            if (_questionDTO is not null && _answerInput is not null)
            {
                _questionDTO.CorrectAnswerIndex = _questionDTO.AnswersTexts.FindIndex(a => a == _answerInput.Text);
                var question = _questionDTO.MapDTO();

                if (question is not null)
                {
                    if (_questionEditingInProcess)
                    {
                        EditSelectedQuestion(question);
                        _questionEditingInProcess = false;
                    }
                    if (_questionCreatingInProcess)
                    {
                        AddNewQuestion(question);
                        _questionCreatingInProcess = false;
                    }

                    HideControls(_buttonSaveCorrectAnswerIndex);
                    _labelUserActionsHelper.Text = LabelTexts.QuestionIsSavedAndAvailable;
                }
            }
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

            _comboBoxChooseAvailableCategory.Text = string.Empty;
            if (_comboBoxChooseAvailableCategory.Items.Count > 0)
            {
                _comboBoxChooseAvailableCategory.Items.Clear();
            }
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

            ClearComboBox(_comboBoxChooseAvailableCategory);
            HideControls(_comboBoxChooseAvailableCategory, _buttonChooseExistingCategory, _buttonAcceptCategoryChoice);

            if (_selectedCategory is not null)
            {
                if (_questionCreatingInProcess)
                {
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.CreateQuestion, _selectedCategory.Title);
                    DisplayControls(_textBoxForQuestionInput);
                }
                else
                {
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.ChooseQuestionInCategory, _selectedCategory.Title);
                    var availableQuestions = _repo.GetAvailableQuestions().Where(q => q.QuestionCategory.Id == _selectedCategory.Id);

                    ClearComboBox(_comboBoxChooseAvailableQuestion);
                    foreach (var q in availableQuestions)
                    {
                        _comboBoxChooseAvailableQuestion.Items.Add(q.Text);
                    }

                    DisplayControls(_comboBoxChooseAvailableQuestion);
                }
            }
        }
        #endregion

        #region DISPLAY & EDIT AVAILABLE QUESTIONS
        /// <summary>
        /// Displays categories which are available for user and asks for category input if at least one category is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDisplayAvailableQuestions_Click(object sender, EventArgs e)
        {
            var availableCategories = GetAvailableCategories(_repo.GetAvailableQuestions());
            if (availableCategories.Any())
            {
                ClearComboBox(_comboBoxChooseAvailableCategory);
                foreach (var c in availableCategories)
                {
                    _comboBoxChooseAvailableCategory.Items.Add(c.Title);
                }

                _labelUserActionsHelper.Text = LabelTexts.AvailableCategories;
                HideMainMenuControls();
                DisplayControls(_comboBoxChooseAvailableCategory);
                DisplayButtonReturnToMainMenu();
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
            DisplayControls(_buttonChooseAvailableQuestion, _buttonEditQuestion);
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
                    ClearComboBox(_comboBoxChooseAvailableQuestion);
                    HideControls(_buttonChooseAvailableQuestion, _comboBoxChooseAvailableQuestion, _buttonEditQuestion);

                    _labelUserActionsHelper.Text = _selectedQuestion.Text;

                    for (int i = 0; i < _selectedQuestion.Answers.Count; i++)
                    {
                        CreateAnswerRadiobutton(_selectedQuestion.Answers[i].Text);
                        EnableAnswerRadioButtonToPickAnswer(_radioButtonsForPickingAnswer.Last());
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
            _answerInput = (RadioButton)sender;
            if (_answerInput.Checked)
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
            HideAndClearControls(_radioButtonsForPickingAnswer);
            RemoveControlFromFlowLayoutPanel(_radioButtonsForPickingAnswer);

            HideControls(_buttonAcceptAnswerInput);

            if (_selectedQuestion is not null && _answerInput is not null)
            {
                _handler = new(_selectedQuestion, _repo);
                if (_selectedQuestion.Answers.FirstOrDefault(a => a.Text == _answerInput.Text && a.IsCorrect) is not null)
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

        /// <summary>
        /// Displays textboxes for editing texts of question and its answers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEditQuestion_Click(object sender, EventArgs e)
        {
            _comboBoxChooseAvailableQuestion.Text = string.Empty;
            HideControls(_buttonChooseAvailableQuestion, _buttonEditQuestion, _buttonAcceptQuestionText, _comboBoxChooseAvailableQuestion);
            _questionEditingInProcess = true;

            if (_selectedCategory is not null)
            {
                _selectedQuestion = _repo.FirstOrDefault(q => q.Text == _comboBoxChooseAvailableQuestion.SelectedItem.ToString() && q.CategoryId == _selectedCategory.Id);

                if (_selectedQuestion is not null)
                {
                    _labelUserActionsHelper.Text = LabelTexts.TypeNewTextValues;

                    _textBoxForQuestionInput.Text = _selectedQuestion.Text;
                    DisplayControls(_textBoxForQuestionInput);
                    _textBoxForQuestionInput.TextChanged += TextBoxesForEditingTexts_TextChanged!;

                    for (int i = 0; i < _selectedQuestion.Answers.Count; i++)
                    {
                        var tb = new TextBox() { Text = _selectedQuestion.Answers[i].Text };
                        _textBoxesForEditingAnswers.Add(tb);
                        _flowLayoutPanel.Controls.Add(tb);
                        tb.TextChanged += TextBoxesForEditingTexts_TextChanged!;
                    }
                }
            }
            DisplayButtonReturnToMainMenu();
        }

        /// <summary>
        /// Displays button to save text changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxesForEditingTexts_TextChanged(object sender, EventArgs e)
        {
            if (_questionEditingInProcess && _textBoxForQuestionInput.Visible == true)
            {
                DisplayControls(_buttonSaveTextsChanges);

                if (!_textBoxesForEditingAnswers.Any(tb => tb.Text == string.Empty) && !_textBoxesForEditingAnswers.GroupBy(tb => tb.Text).Any(t => t.Count() > 1) && UserInputIsValid(_textBoxForQuestionInput.Text))
                {
                    _buttonSaveTextsChanges.Enabled = true;
                }
            }
            DisplayButtonReturnToMainMenu();
        }

        /// <summary>
        /// Saves updated qustion and answers texts and provides user to picking correct answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveTextChanges_Click(object sender, EventArgs e)
        {
            if (_textBoxesForEditingAnswers.Any(tb => tb.Text == string.Empty) || _textBoxForQuestionInput.Text == string.Empty)
            {
                _labelErrorMessages.Text = LabelTexts.EmptyFormsError;
                DisplayControls(_labelErrorMessages);
            }
            else
            {
                HideControls(_labelErrorMessages);
                HideControls(_textBoxesForEditingAnswers.ToArray());
                HideControls(_textBoxForQuestionInput, _buttonSaveTextsChanges);

                _questionDTO = new() { QuestionText = _textBoxForQuestionInput.Text, QuestionCategory = _selectedCategory };
                foreach (var tb in _textBoxesForEditingAnswers)
                {
                    _questionDTO.AnswersTexts.Add(tb.Text);
                }

                _textBoxForQuestionInput.Text = string.Empty;
                _labelUserActionsHelper.Text = string.Format(LabelTexts.SetCorrectAnswer, _questionDTO.QuestionText);

                foreach (var tb in _textBoxesForEditingAnswers)
                {
                    var r = new RadioButton() { Text = tb.Text };
                    _radioButtonsForPickingAnswer.Add(r);
                    AddControlToFlowLayoutPanel(r);
                    r.CheckedChanged += RadiobuttonToSetCorrectAnswer_CheckedChanged!;
                }
            }
        }
        #endregion        
    }
}