using Domain;
using static Presentation.Helper.ControlMessages;
using static Presentation.Helper.Validator;

namespace Presentation
{
    public class FormAddCategory : FormQuestionsBase, ICategoriesListBoxForm, ITitlesTextBox
    {
        #region INITIALIZING
        private TextBox _textBoxForCategoryTitle = new();
        public TextBox TextBox { get { return _textBoxForCategoryTitle; } set { _textBoxForCategoryTitle = new(); } }

        private readonly Button _buttonAcceptCategory = new();
        private readonly Button _buttonCreateNewCategory = new();
        private readonly Button _buttonReturnToAddingQuestions = new();

        private ListBox _listBox = new();
        public ListBox ListBoxForCategories { get { return _listBox; } set { _listBox = new(); } }

        protected override void InitializeControls()
        {
            _flowLayoutPanel.Controls.Add(ListBoxForCategories);
            ListBoxForCategories.SelectedIndexChanged += OnListBoxForCategoriesSelectedIndexChanged!;

            TextBox.Visible = false;
            _flowLayoutPanel.Controls.Add(TextBox);
            TextBox.TextChanged += OnTextBoxTextChanged!;

            _buttonAcceptCategory.Text = ButtonTexts.AcceptAvailableCategory;
            _buttonAcceptCategory.Visible = false;
            _buttonAcceptCategory.Enabled = false;
            _flowLayoutPanel.Controls.Add(_buttonAcceptCategory);
            _buttonAcceptCategory.Click += OnButtonAcceptCategoryClick!;

            _buttonCreateNewCategory.Text = ButtonTexts.CreateNewCategory;
            _buttonCreateNewCategory.Visible = true;
            _flowLayoutPanel.Controls.Add(_buttonCreateNewCategory);
            _buttonCreateNewCategory.Click += OnButtonCreateCategoryClick!;

            _buttonReturnToAddingQuestions.Text = "Вернуться к вопросам";
            _buttonReturnToAddingQuestions.Visible = false;
            _flowLayoutPanel.Controls.Add(_buttonReturnToAddingQuestions);
            _buttonReturnToAddingQuestions.Click += OnButtonReturnToAddingQuestionsClick!;
        }

        public FormAddCategory(FormQuestionsBase form)
        {
            DataHandler = form.DataHandler;
            DisplayMenuOptions();
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            InitializeControls();
        }
        #endregion

        protected override void DisplayMenuOptions(object? sender = default, EventArgs? e = default)
        {
            if (DataHandler.Categories.Count > 0)
            {
                foreach (var category in DataHandler.Categories)
                {
                    ListBoxForCategories.Items.Add(category.Title);
                }

                _label.Text = LabelTexts.AvailableCategories;
                DisplayControls(ListBoxForCategories);
            }
            else
            {
                _label.Text = LabelTexts.NoCategoriesAvailable;
            }
            DisplayControls(_buttonCreateNewCategory);
        }

        protected override void DisplayMenuControls()
        {
            foreach (Control c in _flowLayoutPanel.Controls)
            {
                if (c == _buttonCreateNewCategory || c == _label)
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
            DataHandler.ResetSelectedCategoryToDefault();
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
                DisplayControls(_buttonAcceptCategory);
            }
        }

        /// <summary>
        /// Sets new category from the listbox of available categories for the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAcceptCategoryClick(object sender, EventArgs e)
        {
            if (DataHandler.SelectedQuestion is not null && ListBoxForCategories.SelectedIndex != -1)
            {
                DataHandler.SetNewSelectedCategory(DataHandler.Categories[ListBoxForCategories.SelectedIndex]);
                DataHandler.EditSelectedQuestionCategory();

                _label.Text = string.Format(LabelTexts.ActualQuestionCategory, DataHandler.SelectedQuestion.Text, DataHandler.SelectedQuestion.QuestionCategory.Title);

                HideControls(_buttonAcceptCategory, _buttonCreateNewCategory, ListBoxForCategories);
                DisplayControls(_buttonReturnToAddingQuestions);
            }
        }

        /// <summary>
        /// Gets user input for new category title and sets newly created category for the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonCreateCategoryClick(object sender, EventArgs e)
        {
            HideControls(_buttonAcceptCategory, ListBoxForCategories);

            if (DataHandler.SelectedQuestion is not null)
            {
                _label.Text = string.Format(LabelTexts.TypeCategoryTitle, DataHandler.SelectedQuestion.Text);

                if (!UserInputIsValid(TextBox.Text))
                {
                    _buttonCreateNewCategory.Text = ButtonTexts.AcceptCategoryTitle;
                    _buttonCreateNewCategory.Enabled = false;
                    DisplayControls(TextBox, _buttonCreateNewCategory);
                }
                else
                {
                    Category? category;
                    if (DataHandler.Categories.Any(c => c.Title == TextBox.Text))
                    {
                        category = DataHandler.Categories.SingleOrDefault(c => c.Title == TextBox.Text);
                    }
                    else
                    {
                        category = new Category(TextBox.Text);
                        DataHandler.Categories.Add(category);
                    }

                    if (category is not null)
                    {
                        DataHandler.SetNewSelectedCategory(category);
                        DataHandler.EditSelectedQuestionCategory();
                    }

                    if (DataHandler.SelectedCategory is not null && DataHandler.SelectedQuestion is not null)
                    {
                        _label.Text = string.Format(LabelTexts.ActualQuestionCategory, DataHandler.SelectedQuestion.Text, DataHandler.SelectedQuestion.QuestionCategory.Title);
                    }

                    HideControls(TextBox, _buttonCreateNewCategory);
                    DisplayControls(_buttonReturnToAddingQuestions);
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
            if (UserInputIsValid(TextBox.Text))
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
