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
        private readonly List<Category> _categories = new();

        private QuestionRepetitionsHandler? _handler;
        private QuestionsEditor? _editor;
        private QuestionDTO? _questionDTO;
        private RadioButton? _answerInput;
        private Question? _selectedQuestion;
        private Category? _selectedCategory;

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

        /// <summary>
        /// Displays buttons to create a new question or choose an available question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAddNewQuestionClick(object sender, EventArgs e)
        {
            _questionCreatingInProcess = true;

            _buttonEditOrCreateQuestion.Text = ButtonTexts.CreateNewCategory;
            _buttonChooseAvailableQuestion.Text = ButtonTexts.ChooseAvailableCategory;

            HideMainMenuControls();

            if (_categories.Count > 0)
            {
                _labelUserActionsHelper.Text = LabelTexts.ChooseOrCreateCategory;
                DisplayControls(_buttonChooseAvailableQuestion, _buttonEditOrCreateQuestion);
            }
            else
            {
                _labelUserActionsHelper.Text = LabelTexts.CreateCategory;
                if (_selectedCategory is null)
                {
                    DisplayControls(_textBox);
                }
            }
            DisplayButtonReturnToMainMenu();
        }

        /// <summary>
        /// Gets user to picking a question category and then a question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonDisplayAvailableQuestionsClick(object sender, EventArgs e)
        {
            if (_repo.GetAvailableQuestions().Any())
            {
                _buttonChooseAvailableQuestion.Text = LabelTexts.AvailableCategories;
                if (_selectedCategory is null && _comboBox.Items.Count == 0)
                {
                    if (GetAvailableCategories().Any())
                    {
                        _labelUserActionsHelper.Text = LabelTexts.AvailableCategories;
                        AddCategoriesToComboBox(GetAvailableCategories());
                        DisplayControls(_comboBox);
                    }
                }
                HideMainMenuControls();
                DisplayButtonReturnToMainMenu();
            }
            else
            {
                _labelUserActionsHelper.Text = LabelTexts.NoQuestionsAvailable;
            }
        }

        /// <summary>
        /// Displays buttons to accept user choice of a category and then a question from combobox items
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void OnComboBoxSelectedIndexChanged(object obj, EventArgs e)
        {
            if (_selectedCategory is null && !_questionCreatingInProcess)
            {
                _buttonChooseAvailableQuestion.Text = ButtonTexts.AcceptCategoryChoice;
                DisplayControls(_buttonChooseAvailableQuestion);
            }

            if (_selectedCategory is null && _questionCreatingInProcess)
            {
                TrySetOrCreateCategory();
                _buttonEditOrCreateQuestion.Text = ButtonTexts.AcceptCategoryChoice;
                DisplayControls(_buttonEditOrCreateQuestion);
            }

            if (_selectedCategory is not null && !_questionCreatingInProcess)
            {
                _buttonChooseAvailableQuestion.Text = ButtonTexts.AcceptQuestionChoice;
                DisplayControls(_buttonChooseAvailableQuestion);
            }
        }

        /// <summary>
        /// Displays and allows to choose categories and questions and then answer the chosen question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonChooseAvailableQuestionClick(object sender, EventArgs e)
        {
            HideControls(_buttonDisplayAvailableQuestions, _buttonChooseAvailableQuestion, _buttonEditOrCreateQuestion);

            if (ComboBoxIsReadyToAddCategories)
            {
                _labelUserActionsHelper.Text = LabelTexts.AvailableCategories;
                AddCategoriesToComboBox(_categories);
                DisplayControls(_comboBox);
                TrySetOrCreateCategory();                
            }

            if (_selectedCategory is null && _comboBox.Items.Count != 0)
            {
                TrySetOrCreateCategory();                
            }

            if (_selectedCategory is not null)
            {
                if (!_questionCreatingInProcess)
                {
                    if (ComboBoxIsReadyToAddQuestions)
                    {
                        _labelUserActionsHelper.Text = string.Format(LabelTexts.ChooseQuestionInCategory, _selectedCategory.Title);
                        var availableQuestions = _repo.GetAvailableQuestions().Where(q => q.QuestionCategory.Id == _selectedCategory.Id);
                        AddQuestionsToComboBox(availableQuestions);
                    }

                    if (ComboBoxContainsQuestion)
                    {
                        TrySetSelectedQuestion();
                    }
                }

                if (_selectedQuestion is not null)
                {
                    HideControls(_comboBox);
                    _labelUserActionsHelper.Text = _selectedQuestion.Text;
                    DisplayAnswersOfQuestionOrDTO();
                    if (_questionCreatingInProcess)
                    {
                        HideControls(_textBox, _buttonEditOrCreateQuestion);
                        foreach (var r in _radioButtonsForAnswers)
                        {
                            EnableAnswerRadioButton(r);
                        }
                        _buttonForPickingAnswers.Text = ButtonTexts.AcceptCorrectAnswerInput;
                    }
                }
                
                if (_selectedQuestion is null && _questionDTO is not null)
                {
                    DisplayAnswersOfQuestionOrDTO();

                    if (_questionCreatingInProcess)
                    {
                        HideControls(_textBox, _buttonEditOrCreateQuestion);
                        foreach (var r in _radioButtonsForAnswers)
                        {
                            EnableAnswerRadioButton(r);
                        }
                        _buttonForPickingAnswers.Text = ButtonTexts.AcceptCorrectAnswerInput;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the defined answer as correct in case of creating a question; accepts user answer choice for verification in case of answering the question 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRadioButtonForAnswerCheckedChanged(object sender, EventArgs e)
        {
            _answerInput = (RadioButton)sender;
            if (_answerInput.Checked)
            {
                if (_questionCreatingInProcess && _questionDTO is not null)
                {
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.SetCorrectAnswer, _questionDTO.QuestionText);
                    _buttonForPickingAnswers.Text = ButtonTexts.AcceptCorrectAnswerInput;
                }
                else
                {
                    _buttonForPickingAnswers.Text = ButtonTexts.CheckAnswer;
                }
                DisplayControls(_buttonForPickingAnswers);
            }
        }

        /// <summary>
        /// Updates the defined question or saves new question after getting user answer (in case of answering the question) or picking a correct answer option (in case of creating or edition a question)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>com
        private void OnButtonForPickingAnswersClick(object sender, EventArgs e)
        {
            RemoveControlFromFlowLayoutPanel(_radioButtonsForAnswers);
            HideControls(_buttonForPickingAnswers);

            if (_questionCreatingInProcess)
            {
                if (_questionDTO is not null && _answerInput is not null)
                {
                    _questionDTO.CorrectAnswerIndex = _questionDTO.AnswersTexts.FindIndex(a => a == _answerInput.Text);
                    var question = _questionDTO.MapDTO();

                    if (question is not null)
                    {
                        if (!_categories.Any(c => c.Id == question.QuestionCategory.Id))
                        {
                            _categories.Add(question.QuestionCategory);
                        }                     

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

                        _labelUserActionsHelper.Text = LabelTexts.QuestionIsSavedAndAvailable;
                    }
                }
            }
            else
            {
                bool isAnswerCorrect = IsAnswerCorrect();
                CheckAnswer(isAnswerCorrect);
                _labelUserActionsHelper.Text = (isAnswerCorrect ? LabelTexts.CorrectAnswer : LabelTexts.WrongAnswer) + string.Format(LabelTexts.WhenQuestionIsAvailable, _selectedQuestion!.AvailableAt);
            }

            DisplayControls(_buttonReturnToMainMenu);
        }

        /// <summary>
        /// Creates a category and question DTO with the question's text and answers according to user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonCreateOrEditQuestionClick(object sender, EventArgs e)
        {           
            HideControls(_buttonChooseAvailableQuestion, _buttonEditOrCreateQuestion, _comboBox);
            DisplayControls(_textBox);

            if (_selectedCategory is null)
            {
                _labelUserActionsHelper.Text = LabelTexts.CreateCategory;
                if (UserInputIsValid(_textBox.Text))
                {
                    _selectedCategory = new Category(_textBox.Text);
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.CreateQuestion, _selectedCategory.Title);
                    _buttonEditOrCreateQuestion.Text = ButtonTexts.AcceptQuestionText;
                    _textBox.Clear();
                }
            }

            if (_selectedCategory is not null && _questionDTO is null)
            {                
                _labelUserActionsHelper.Text = string.Format(LabelTexts.CreateQuestion, _selectedCategory.Title);

                if (UserInputIsValid(_textBox.Text))
                {
                    _questionDTO = new QuestionDTO { QuestionCategory = _selectedCategory, QuestionText = _textBox.Text };
                    _labelUserActionsHelper.Text = string.Format(LabelTexts.DisplayQuestionWhileAddingAnswers, _questionDTO.QuestionText);
                    _buttonEditOrCreateQuestion.Text = ButtonTexts.AcceptQuestionText;
                    _textBox.Clear();
                }
            }

            if (_selectedCategory is not null && _questionDTO is not null)
            {
                _buttonEditOrCreateQuestion.Text = ButtonTexts.AcceptAnswerText;

                if (UserInputIsValid(_textBox.Text) && !_questionDTO.AnswersTexts.Contains(_textBox.Text))
                {
                    _questionDTO.AnswersTexts.Add(_textBox.Text);
                    CreateAnswerRadiobutton(_textBox.Text);
                    _textBox.Clear();
                    _answerInputCounter++;
                }
                if (_answerInputCounter >= Question.MinAnswersCount)
                {
                    DisplayControls(_buttonChooseAvailableQuestion);
                    _buttonChooseAvailableQuestion.Text = ButtonTexts.FinishAddingAnswers;
                }
            }
        }

        /// <summary>
        /// Saves user inputs for category and question titles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            HideControls(_buttonEditOrCreateQuestion);

            if (_selectedCategory is null)
            {
                _buttonEditOrCreateQuestion.Text = ButtonTexts.SaveCategoryTitle;
            }

            if (_selectedCategory is not null && _questionDTO is null)
            {
                _buttonEditOrCreateQuestion.Text = ButtonTexts.AcceptQuestionText;
            }

            if (UserInputIsValid(_textBox.Text))
            {
                DisplayControls(_buttonEditOrCreateQuestion);
            }
        }

        /// <summary>
        /// Sets selected category value according to user input for new category title or user choice of existing category 
        /// </summary>
        /// <param name="title"></param>
        private void TrySetOrCreateCategory()
        {
            if (_selectedCategory is null)
            {
                _buttonChooseAvailableQuestion.Text = LabelTexts.AvailableCategories;

                if (_comboBox.Items.Count == 0)
                {
                    if (_categories.Any())
                    {
                        _labelUserActionsHelper.Text = LabelTexts.AvailableCategories;
                        AddCategoriesToComboBox(_categories);
                        DisplayControls(_comboBox);
                    }
                }

                if (_comboBox.SelectedItem is not null)
                {
                    var s = _comboBox.SelectedItem.ToString();
                    if (_categories.FirstOrDefault(c => c.Title == s) is not null)
                    {
                        _selectedCategory = _categories.FirstOrDefault(c => c.Title == s);
                    }                    
                    if (!_questionCreatingInProcess)
                    {
                        ClearComboBox();
                    }
                }
            }
        }

        /// <summary>
        /// Sets selected question value according to user choice 
        /// </summary>
        private void TrySetSelectedQuestion()
        {
            if (_selectedCategory is not null && ComboBoxContainsQuestion)
            {
                _selectedQuestion = _repo.FirstOrDefault(q => q.Text == _comboBox.SelectedItem.ToString() && q.CategoryId == _selectedCategory.Id);

                if (_selectedQuestion is not null)
                {
                    ClearComboBox();
                }
            }
        }

        /// <summary>
        /// Displays answers' options of the defined question 
        /// </summary>
        private void DisplayAnswersOfQuestionOrDTO()
        {
            if (_selectedQuestion is not null)
            {
                for (int i = 0; i < _selectedQuestion.Answers.Count; i++)
                {
                    CreateAnswerRadiobutton(_selectedQuestion.Answers[i].Text);
                    EnableAnswerRadioButton(_radioButtonsForAnswers.Last());
                }
                DisplayControls(_radioButtonsForAnswers.ToArray());
            }

            if (_selectedQuestion is null && _questionDTO is not null)
            {
                if (AnswerTextsAreUnique(_questionDTO.AnswersTexts))
                {
                    _textBox.Clear();
                    _answerInputCounter = default;

                    foreach (var r in _radioButtonsForAnswers)
                    {
                        EnableAnswerRadioButton(r);
                    }

                    _labelUserActionsHelper.Text = string.Format(LabelTexts.SetCorrectAnswer, _questionDTO.QuestionText);
                }
            }
        }
    }
}
