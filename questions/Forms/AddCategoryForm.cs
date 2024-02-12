using Domain;
using Presentation.Helper;
using static Presentation.Helper.ControlMessages;

namespace Presentation.Forms
{
    public partial class AddCategoryForm : Form, ITitlesTextBox, ICategoriesListBoxForm
    {
        private readonly DataHandler _dataHandler;
        protected bool _acceptTextBoxTextChanges = false;

        public TextBox TextBox { get { return _textBoxForCategoryTitle; } }
        public ListBox ListBoxForCategories { get { return _listBox; } }

        public AddCategoryForm(DataHandler handler)
        {
            InitializeComponent();
            _dataHandler = handler;
            DisplayMenuOptions();
        }

        private void DisplayMenuOptions(object? sender = default, EventArgs? e = default)
        {
            if (_dataHandler.Categories.Count > 0)
            {
                foreach (var category in _dataHandler.Categories)
                {
                    ListBoxForCategories.Items.Add(category.Title);
                }

                _label.Text = LabelTexts.AvailableCategories;
                ControlsHelper.DisplayControls(ListBoxForCategories);
            }
            else
            {
                _label.Text = LabelTexts.NoCategoriesAvailable;
            }
            ControlsHelper.DisplayControls(_buttonCreateNewCategory);
        }

        private void ResetInputValues()
        {
            _dataHandler.ResetSelectedCategoryToDefault();
            ListBoxForCategories.Items.Clear();
            TextBox.Clear();
        }

        /// <summary>
        /// Displays the button for setting category for the chosen question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnListBoxForCategoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ListBoxForCategories.SelectedIndex;

            if (selectedIndex != -1)
            {
                _buttonAcceptCategory.Text = ButtonTexts.AcceptAvailableCategory;
                _buttonAcceptCategory.Enabled = true;
                ControlsHelper.DisplayControls(_buttonAcceptCategory);
            }
        }

        /// <summary>
        /// Sets new category from the listbox of available categories for the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAcceptCategoryClick(object sender, EventArgs e)
        {
            if (_dataHandler.SelectedQuestion is not null && ListBoxForCategories.SelectedIndex != -1)
            {
                _dataHandler.SetNewSelectedCategory(_dataHandler.Categories[ListBoxForCategories.SelectedIndex]);
                _dataHandler.EditSelectedQuestionCategory();

                _label.Text = string.Format(LabelTexts.ActualQuestionCategory, _dataHandler.SelectedQuestion.Text, _dataHandler.SelectedQuestion.QuestionCategory.Title);

                ControlsHelper.HideControls(_buttonAcceptCategory, _buttonCreateNewCategory, ListBoxForCategories);
                ControlsHelper.DisplayControls(_buttonReturnToAddingQuestions);
            }
        }

        /// <summary>
        /// Gets user input for new category title and sets newly created category for the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonCreateCategoryClick(object sender, EventArgs e)
        {
            ControlsHelper.HideControls(_buttonAcceptCategory, ListBoxForCategories);

            if (_dataHandler.SelectedQuestion is not null)
            {
                _label.Text = string.Format(LabelTexts.TypeCategoryTitle, _dataHandler.SelectedQuestion.Text);

                if (!Validator.UserInputIsValid(TextBox.Text))
                {
                    _buttonCreateNewCategory.Text = ButtonTexts.AcceptCategoryTitle;
                    _buttonCreateNewCategory.Enabled = false;
                    ControlsHelper.DisplayControls(TextBox, _buttonCreateNewCategory);
                }
                else
                {
                    Category? category;
                    if (_dataHandler.Categories.Any(c => c.Title == TextBox.Text))
                    {
                        category = _dataHandler.Categories.SingleOrDefault(c => c.Title == TextBox.Text);
                    }
                    else
                    {
                        category = new Category(TextBox.Text);
                        _dataHandler.Categories.Add(category);
                    }

                    if (category is not null)
                    {
                        _dataHandler.SetNewSelectedCategory(category);
                        _dataHandler.EditSelectedQuestionCategory();
                    }

                    if (_dataHandler.SelectedCategory is not null && _dataHandler.SelectedQuestion is not null)
                    {
                        _label.Text = string.Format(LabelTexts.ActualQuestionCategory, _dataHandler.SelectedQuestion.Text, _dataHandler.SelectedQuestion.QuestionCategory.Title);
                    }

                    ControlsHelper.HideControls(TextBox, _buttonCreateNewCategory);
                    ControlsHelper.DisplayControls(_buttonReturnToAddingQuestions);
                }
            }
        }

        /// <summary>
        /// Validates user category title input and displays button to save it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            if (Validator.UserInputIsValid(TextBox.Text))
            {
                _acceptTextBoxTextChanges = true;
                _buttonCreateNewCategory.Enabled = true;
            }
            else
            {
                _acceptTextBoxTextChanges = false;
                _buttonCreateNewCategory.Enabled = false;
            }
        }

        /// <summary>
        /// Resets the selected category and the selected question to default and closes the form for adding categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonReturnToAddingQuestionsClick(object sender, EventArgs e)
        {
            ResetInputValues();
            Close();
        }
    }
}
