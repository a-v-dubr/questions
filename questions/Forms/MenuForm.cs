using Domain;
using Presentation.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Presentation.Helper.ControlMessages;

namespace Presentation.Forms
{
    public partial class MenuForm : Form
    {
        private DataHandler _dataHandler;
        protected RadioButton? _answerInput;
        protected int _answerInputCounter = 0;

        private bool CategoryIsChosenInListBox => _dataHandler.SelectedCategory is null && _listBox.SelectedIndex != -1;
        private List<Question> AvailableQuestions => _dataHandler.Repo.GetAvailableQuestions().Where(q => q.QuestionCategory.Id == _dataHandler.SelectedCategory?.Id).ToList();

        public MenuForm()
        {
            InitializeComponent();
            _dataHandler = new DataHandler();
            DisplayMenuOptions(null, null);
            _radioButtons = new();
            _listBox.AutoSize = true;
        }

       private void DisplayMenuOptions(object? obj, EventArgs? e)
        {
            DisplayMenuControls();
            _dataHandler.ResetSelectedCategoryToDefault();

            var availableCategories = _dataHandler.GetAvailableCategories();
            if (availableCategories.Count > 0)
            {
                AddCategoriesToListBox(availableCategories);

                _label.Text = "Выберите категорию вопросов:";
                ControlsHelper.DisplayControls(_listBox, _label, _buttonAddNewQuestion);
            }
            else
            {
                ControlsHelper.HideControls(_listBox);
                _label.Text = LabelTexts.NoQuestionsAvailable;
                ControlsHelper.DisplayControls(_buttonAddNewQuestion);
            }
        }

        private void DisplayMenuControls()
        {
            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == _buttonAddNewQuestion || c == _listBox || c == _label)
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
            _dataHandler.ResetQuestionDTOToDefault();
        }

        /// <summary>
        /// Displays the button for choosing a category with available questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnListBoxForCategoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = _listBox.SelectedIndex;

            if (selectedIndex != -1)
            {
                _buttonAnswerQuestionsFromAvailableCategory.Text = ButtonTexts.AnswerAllQuestionsFromCategory;
                ControlsHelper.DisplayControls(_buttonAnswerQuestionsFromAvailableCategory);
            }
        }

        /// <summary>
        /// Sets selected category and offers to answer all available questions in the category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAnswerQuestionsFromAvailableCategoryClick(object sender, EventArgs e)
        {
            ControlsHelper.RemoveControlFromFlowLayoutPanel(_radioButtons, _flowLayoutPanel);

            if (CategoryIsChosenInListBox)
            {
                _dataHandler.SetNewSelectedCategory(_dataHandler.GetAvailableCategories()[_listBox.SelectedIndex]);                
            }

            if (_dataHandler.SelectedCategory is not null)
            {
                ControlsHelper.HideControls(_listBox);

                if (AvailableQuestions.Count > 0)
                {
                    _dataHandler.SetSelectedQuestion(AvailableQuestions[0]);
                    if (_dataHandler.SelectedQuestion is not null)
                    {
                        _label.Text = string.Format(LabelTexts.ChooseAnswer, _dataHandler.SelectedQuestion.Text);

                        ControlsHelper.HideControls(_buttonAddNewQuestion, _buttonAnswerQuestionsFromAvailableCategory);
                        DisplayAnswersOfQuestionOrDTO();

                        ControlsHelper.DisplayControlBelowOthersInFlowPanel(_flowLayoutPanel, _buttonForPickingAnswers);
                        ControlsHelper.DisplayControlBelowOthersInFlowPanel(_flowLayoutPanel, _buttonChangeCategory);
                        ControlsHelper.DisplayControlBelowOthersInFlowPanel(_flowLayoutPanel, _buttonDisplayMenuOptions);
                    }
                }
                else
                {
                    _dataHandler.ResetSelectedCategoryToDefault();
                    ControlsHelper.HideControls(_buttonForPickingAnswers, _buttonAnswerQuestionsFromAvailableCategory);
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
                foreach (var r in _radioButtons)
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
                _buttonForPickingAnswers.Enabled = true;
            }
        }

        /// <summary>
        /// Checks if the answer input is correct and sets new availability datetime for the selected question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonForPickingAnswersClick(object sender, EventArgs e)
        {
            bool isAnswerCorrect = _dataHandler.IsAnswerCorrect(_answerInput);
            _dataHandler.CheckAnswer(isAnswerCorrect);
            DisplayAnswerAttemptResult(isAnswerCorrect);

            _buttonForPickingAnswers.Enabled = false;
            ControlsHelper.HideControls(_buttonForPickingAnswers);

            if (_dataHandler.SelectedCategory is not null)
            {
                if (AvailableQuestions.Count > 0)
                {
                    _buttonAnswerQuestionsFromAvailableCategory.Text = ButtonTexts.AnswerNextQuestion;
                    ControlsHelper.DisplayControls(_flowLayoutPanel, _buttonAnswerQuestionsFromAvailableCategory);
                }
                else
                {
                    _label.Text += string.Format(LabelTexts.NoMoreAvailableQuestionsInCategory, _dataHandler.SelectedCategory.Title);
                    ControlsHelper.HideControls(_buttonAnswerQuestionsFromAvailableCategory, _buttonChangeCategory);
                }
            }

            ControlsHelper.DisplayControlBelowOthersInFlowPanel(_flowLayoutPanel, _buttonDisplayMenuOptions);
        }

        public void DisplayAnswersOfQuestionOrDTO()
        {
            if (_dataHandler.SelectedQuestion is not null)
            {
                for (int i = 0; i < _dataHandler.SelectedQuestion.Answers.Count; i++)
                {
                    CreateAnswerRadiobutton(_dataHandler.SelectedQuestion.Answers[i].Text);
                    EnableAnswerRadioButton(_radioButtons.Last());
                    ControlsHelper.DisplayControls(_radioButtons.ToArray());
                }
            }
        }

        /// <summary>
        /// Displays whether the answer is correct or incorrect 
        /// </summary>
        private void DisplayAnswerAttemptResult(bool answerIsCorrect)
        {
            ControlsHelper.HideControls(_buttonChangeCategory);
            ControlsHelper.RemoveControlFromFlowLayoutPanel(_radioButtons, _flowLayoutPanel);
            _radioButtons.Clear();

            _label.Text = (answerIsCorrect ? LabelTexts.CorrectAnswer : LabelTexts.WrongAnswer) + string.Format(LabelTexts.WhenQuestionIsAvailable, _dataHandler.SelectedQuestion?.AvailableAt);
        }

        /// <summary>
        /// Opens a form for creating new question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAddNewQuestionClick(object sender, EventArgs e)
        {
            var creatingForm = new AddQuestionsForm(_dataHandler);
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
            var categoriesForm = new AddCategoryForm(_dataHandler);
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
            ResetInputValues();
            DisplayMenuOptions(null, null);
        }

        /// <summary>
        /// Adds unique categories to the listbox
        /// </summary>
        /// <param name="categories"></param>
        private void AddCategoriesToListBox(List<Category> categories)
        {
            if (_listBox.Items.Count > 0)
            {
                _listBox.Items.Clear();
            }

            int questionsCount;
            foreach (var c in categories)
            {
                questionsCount = c.Questions.Where(q => q.AvailableAt <= DateTime.Now).Count();
                if (questionsCount > 0)
                {
                    _listBox.Items.Add(string.Format(ListBoxTexts.AvailableCategories, c.Title, questionsCount));
                }
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
    }
}
