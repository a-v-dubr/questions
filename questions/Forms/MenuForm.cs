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
    public partial class MenuForm : Form, ICategoriesListBoxForm, IAnswerRadioButtonsForm
    {
        private DataHandler _dataHandler;
        protected RadioButton? _answerInput;
        protected int _answerInputCounter = 0;

        public ListBox ListBoxForCategories { get { return _listBox; } }        
        public List<System.Windows.Forms.RadioButton> RadioButtonsForAnswers { get { return _radioButtons; } }
        private bool CategoryIsChosenInListBox => _dataHandler.SelectedCategory is null && ListBoxForCategories.SelectedIndex != -1;
        private List<Question> AvailableQuestions => _dataHandler.Repo.GetAvailableQuestions().Where(q => q.QuestionCategory.Id == _dataHandler.SelectedCategory?.Id).ToList();


        public MenuForm()
        {
            InitializeComponent();
            _dataHandler = new DataHandler();
            DisplayMenuOptions(null, null);
        }

       private void DisplayMenuOptions(object? obj, EventArgs? e)
        {
            DisplayMenuControls();
            _dataHandler.ResetSelectedCategoryToDefault();

            var availableCategories = _dataHandler.GetAvailableCategories();
            if (availableCategories.Count > 0)
            {
                (this as ICategoriesListBoxForm).AddCategoriesToListBox(availableCategories);

                _label.Text = LabelTexts.AvailableCategoriesAndQuestions;
                ControlsHelper.DisplayControls(ListBoxForCategories, _label, _buttonAddNewQuestion);
            }
            else
            {
                ControlsHelper.HideControls(ListBoxForCategories);
                _label.Text = LabelTexts.NoQuestionsAvailable;
                ControlsHelper.DisplayControls(_buttonAddNewQuestion);
            }
        }

        private void DisplayMenuControls()
        {
            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == _buttonAddNewQuestion || c == ListBoxForCategories || c == _label)
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
            int selectedIndex = ListBoxForCategories.SelectedIndex;

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
            ControlsHelper.RemoveControlFromFlowLayoutPanel(RadioButtonsForAnswers, _flowLayoutPanel);

            if (CategoryIsChosenInListBox)
            {
                _dataHandler.SetNewSelectedCategory(_dataHandler.GetAvailableCategories()[ListBoxForCategories.SelectedIndex]);
            }

            if (_dataHandler.SelectedCategory is not null)
            {
                ControlsHelper.HideControls(ListBoxForCategories);

                if (AvailableQuestions.Count > 0)
                {
                    _dataHandler.SetSelectedQuestion(AvailableQuestions[0]);
                    if (_dataHandler.SelectedQuestion is not null)
                    {
                        _label.Text = string.Format(LabelTexts.ChooseAnswer, _dataHandler.SelectedQuestion.Text);

                        ControlsHelper.HideControls(_buttonAddNewQuestion, _buttonAnswerQuestionsFromAvailableCategory);
                        (this as IAnswerRadioButtonsForm).DisplayAnswersOfQuestionOrDTO();

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
                ControlsHelper.DisplayControls(_flowLayoutPanel, _buttonForPickingAnswers);
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
                    (this as IAnswerRadioButtonsForm).CreateAnswerRadiobutton(_dataHandler.SelectedQuestion.Answers[i].Text);
                    (this as IAnswerRadioButtonsForm).EnableAnswerRadioButton(RadioButtonsForAnswers.Last());
                    ControlsHelper.DisplayControls(RadioButtonsForAnswers.ToArray());
                }
            }
        }

        /// <summary>
        /// Displays whether the answer is correct or incorrect 
        /// </summary>
        private void DisplayAnswerAttemptResult(bool answerIsCorrect)
        {
            ControlsHelper.HideControls(_buttonChangeCategory);
            ControlsHelper.RemoveControlFromFlowLayoutPanel(RadioButtonsForAnswers, _flowLayoutPanel);
            RadioButtonsForAnswers.Clear();

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
    }
}
