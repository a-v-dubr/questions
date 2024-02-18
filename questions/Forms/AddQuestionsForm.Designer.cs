namespace Presentation.Forms
{
    partial class AddQuestionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _labelTypeQuestionText = new Label();
            _textBoxForQuestionTitle = new TextBox();
            _buttonChangeQuestionCategory = new Button();
            _flowLayoutPanel = new FlowLayoutPanel();
            _labelTypeAnswerOptions = new Label();
            _answersTable = new TableLayoutPanel();
            _handlingAnswersButtonsTable = new TableLayoutPanel();
            _buttonAddNewAnswerOption = new Button();
            _buttonAcceptQuestionText = new Button();
            _buttonSaveQuestionFinally = new Button();
            _textBoxesForAnswers = new List<TextBox>();
            _toolTip = new ToolTip(components);
            _buttonCreateAnotherQuestion = new Button();
            _radioButtons = new List<RadioButton>();
            _flowLayoutPanel.SuspendLayout();
            _handlingAnswersButtonsTable.SuspendLayout();
            SuspendLayout();
            // 
            // _labelTypeQuestionText
            // 
            _labelTypeQuestionText.AutoSize = true;
            _labelTypeQuestionText.Location = new Point(3, 10);
            _labelTypeQuestionText.Margin = new Padding(3, 10, 3, 0);
            _labelTypeQuestionText.Name = "_labelTypeQuestionText";
            _labelTypeQuestionText.Size = new Size(169, 20);
            _labelTypeQuestionText.TabIndex = 0;
            _labelTypeQuestionText.Text = "Введите текст вопроса:";
            // 
            // _textBoxForQuestionTitle
            // 
            _textBoxForQuestionTitle.Location = new Point(3, 33);
            _textBoxForQuestionTitle.Name = "_textBoxForQuestionTitle";
            _textBoxForQuestionTitle.Size = new Size(407, 27);
            _textBoxForQuestionTitle.TabIndex = 1;
            // 
            // _buttonChangeQuestionCategory
            // 
            _buttonChangeQuestionCategory.Location = new Point(3, 186);
            _buttonChangeQuestionCategory.Name = "_buttonChangeQuestionCategory";
            _buttonChangeQuestionCategory.Size = new Size(257, 40);
            _buttonChangeQuestionCategory.TabIndex = 4;
            _buttonChangeQuestionCategory.Text = "Выбрать категорию для вопроса";
            _buttonChangeQuestionCategory.Visible = false;
            _buttonChangeQuestionCategory.Click += OnButtonChangeQuestionCategoryClick;
            // 
            // _flowLayoutPanel
            // 
            _flowLayoutPanel.Controls.Add(_labelTypeQuestionText);
            _flowLayoutPanel.Controls.Add(_textBoxForQuestionTitle);
            _flowLayoutPanel.Controls.Add(_labelTypeAnswerOptions);
            _flowLayoutPanel.Controls.Add(_answersTable);
            _flowLayoutPanel.Controls.Add(_handlingAnswersButtonsTable);
            _flowLayoutPanel.Controls.Add(_buttonChangeQuestionCategory);
            _flowLayoutPanel.Controls.Add(_buttonSaveQuestionFinally);
            _flowLayoutPanel.Controls.Add(_buttonCreateAnotherQuestion);
            _flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            _flowLayoutPanel.Location = new Point(2, 1);
            _flowLayoutPanel.Name = "_flowLayoutPanel";
            _flowLayoutPanel.Size = new Size(488, 507);
            _flowLayoutPanel.TabIndex = 0;
            // 
            // _typeAnswerOptionsLabel
            // 
            _labelTypeAnswerOptions.AutoSize = true;
            _labelTypeAnswerOptions.Location = new Point(3, 73);
            _labelTypeAnswerOptions.Margin = new Padding(3, 10, 3, 0);
            _labelTypeAnswerOptions.Name = "_typeAnswerOptionsLabel";
            _labelTypeAnswerOptions.Size = new Size(198, 20);
            _labelTypeAnswerOptions.TabIndex = 6;
            _labelTypeAnswerOptions.Text = "Введите варианты ответов:";
            // 
            // _answersTable
            // 
            _answersTable.ColumnCount = 2;
            _answersTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 400F));
            _answersTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            _answersTable.AutoSize = true;
            _answersTable.Location = new Point(3, 96);
            _answersTable.Name = "_answersTable";
            _answersTable.Size = new Size(485, 32);
            _answersTable.TabIndex = 7;
            // 
            // _handlingAnswersButtonsTable
            // 
            _handlingAnswersButtonsTable.ColumnCount = 2;
            _handlingAnswersButtonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _handlingAnswersButtonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _handlingAnswersButtonsTable.Controls.Add(_buttonAddNewAnswerOption, 0, 0);
            _handlingAnswersButtonsTable.Controls.Add(_buttonAcceptQuestionText, 1, 0);
            _handlingAnswersButtonsTable.Location = new Point(3, 135);
            _handlingAnswersButtonsTable.Margin = new Padding(3, 4, 3, 4);
            _handlingAnswersButtonsTable.Name = "_handlingAnswersButtonsTable";
            _handlingAnswersButtonsTable.RowCount = 1;
            _handlingAnswersButtonsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _handlingAnswersButtonsTable.Size = new Size(400, 44);
            _handlingAnswersButtonsTable.TabIndex = 8;
            // 
            // _buttonAddNewAnswerOption
            // 
            _buttonAddNewAnswerOption.Dock = DockStyle.Top;
            _buttonAddNewAnswerOption.Location = new Point(3, 4);
            _buttonAddNewAnswerOption.Margin = new Padding(3, 4, 3, 4);
            _buttonAddNewAnswerOption.Name = "_buttonAddNewAnswerOption";
            _buttonAddNewAnswerOption.Size = new Size(194, 36);
            _buttonAddNewAnswerOption.TabIndex = 0;
            _buttonAddNewAnswerOption.Text = "Добавить вариант";
            _buttonAddNewAnswerOption.UseVisualStyleBackColor = true;
            _buttonAddNewAnswerOption.Click += OnButtonAddNewAnswerOptionClick;
            // 
            // _buttonAcceptQuestionText
            // 
            _buttonAcceptQuestionText.Dock = DockStyle.Top;
            _buttonAcceptQuestionText.Location = new Point(203, 3);
            _buttonAcceptQuestionText.Name = "_buttonAcceptQuestionText";
            _buttonAcceptQuestionText.Size = new Size(194, 38);
            _buttonAcceptQuestionText.TabIndex = 1;
            _buttonAcceptQuestionText.Text = "Сохранить вопрос";
            _buttonAcceptQuestionText.UseVisualStyleBackColor = true;
            _buttonAcceptQuestionText.Click += OnButtonAcceptQuestionTextClick;
            // 
            // _buttonSaveQuestionFinally
            // 
            _buttonSaveQuestionFinally.Enabled = false;
            _buttonSaveQuestionFinally.Location = new Point(3, 232);
            _buttonSaveQuestionFinally.Name = "_buttonSaveQuestionFinally";
            _buttonSaveQuestionFinally.Size = new Size(257, 40);
            _buttonSaveQuestionFinally.TabIndex = 5;
            _buttonSaveQuestionFinally.Text = "Сохранить вопрос";
            _buttonSaveQuestionFinally.Visible = false;
            _buttonSaveQuestionFinally.Click += OnButtonSaveQuestionClick;
            // 
            // _createAnotherQuestion
            // 
            _buttonCreateAnotherQuestion.Location = new Point(3, 278);
            _buttonCreateAnotherQuestion.Name = "_createAnotherQuestion";
            _buttonCreateAnotherQuestion.Size = new Size(257, 40);
            _buttonCreateAnotherQuestion.TabIndex = 9;
            _buttonCreateAnotherQuestion.Text = "Создать новый вопрос";
            _buttonCreateAnotherQuestion.UseVisualStyleBackColor = true;
            _buttonCreateAnotherQuestion.Visible = false;
            _buttonCreateAnotherQuestion.Click += OnButtonCreateAnotherQuestionClick;
            // 
            // AddQuestionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 511);
            Controls.Add(_flowLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddQuestionsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Добавление новых вопросов";
            _flowLayoutPanel.ResumeLayout(false);
            _flowLayoutPanel.PerformLayout();
            _handlingAnswersButtonsTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel _flowLayoutPanel;
        private Label _labelTypeQuestionText;
        private TextBox _textBoxForQuestionTitle;
        private Button _buttonChangeQuestionCategory;
        private List<RadioButton> _radioButtons;
        private List<TextBox> _textBoxesForAnswers;
        private Button _buttonSaveQuestionFinally;
        private ToolTip _toolTip;
        private Label _labelTypeAnswerOptions;
        private TableLayoutPanel _answersTable;
        private TableLayoutPanel _handlingAnswersButtonsTable;
        private Button _buttonAddNewAnswerOption;
        private Button _buttonAcceptQuestionText;
        private Button _buttonCreateAnotherQuestion;
    }
}