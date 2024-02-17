using Presentation.Helper;
using System.Windows.Forms;
using static Presentation.Helper.ControlMessages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Presentation.Forms
{
    partial class MenuForm
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
            _buttonDisplayMenuOptions = new Button();
            _buttonForPickingAnswers = new Button();
            _buttonChangeCategory = new Button();
            _label = new Label();
            _listBox = new ListBox();
            _buttonAnswerQuestionsFromAvailableCategory = new Button();
            _buttonAddNewQuestion = new Button();
            _flowLayoutPanel = new FlowLayoutPanel();
            _flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _buttonDisplayMenuOptions
            // 
            _buttonDisplayMenuOptions.Location = new Point(3, 240);
            _buttonDisplayMenuOptions.Name = "_buttonDisplayMenuOptions";
            _buttonDisplayMenuOptions.Size = new Size(395, 40);
            _buttonDisplayMenuOptions.TabIndex = 5;
            _buttonDisplayMenuOptions.Text = "Вернуться в меню";
            _buttonDisplayMenuOptions.Click += DisplayMenuOptions;
            // 
            // _buttonForPickingAnswers
            // 
            _buttonForPickingAnswers.Enabled = false;
            _buttonForPickingAnswers.Location = new Point(3, 148);
            _buttonForPickingAnswers.Name = "_buttonForPickingAnswers";
            _buttonForPickingAnswers.Size = new Size(395, 40);
            _buttonForPickingAnswers.TabIndex = 3;
            _buttonForPickingAnswers.Text = "Проверить ответ";
            _buttonForPickingAnswers.Visible = false;
            _buttonForPickingAnswers.Click += OnButtonForPickingAnswersClick;
            // 
            // _buttonChangeCategory
            // 
            _buttonChangeCategory.Location = new Point(3, 194);
            _buttonChangeCategory.Name = "_buttonChangeCategory";
            _buttonChangeCategory.Size = new Size(395, 40);
            _buttonChangeCategory.TabIndex = 6;
            _buttonChangeCategory.Text = "Переместить вопрос в другую категорию";
            _buttonChangeCategory.Visible = false;
            _buttonChangeCategory.Click += OnButtonChangeCategoryClick;
            // 
            // _label
            // 
            _label.AutoSize = true;
            _label.Location = new Point(3, 0);
            _label.Name = "_label";
            _label.Size = new Size(163, 20);
            _label.TabIndex = 0;
            _label.Text = "Доступные категории:";
            // 
            // _listBox
            // 
            _listBox.ItemHeight = 20;
            _listBox.Location = new Point(3, 23);
            _listBox.Name = "_listBox";
            _listBox.Size = new Size(29, 24);
            _listBox.TabIndex = 1;
            _listBox.Visible = false;
            _listBox.SelectedIndexChanged += OnListBoxForCategoriesSelectedIndexChanged;
            // 
            // _buttonAnswerQuestionsFromAvailableCategory
            // 
            _buttonAnswerQuestionsFromAvailableCategory.Location = new Point(3, 56);
            _buttonAnswerQuestionsFromAvailableCategory.Name = "_buttonAnswerQuestionsFromAvailableCategory";
            _buttonAnswerQuestionsFromAvailableCategory.Size = new Size(395, 40);
            _buttonAnswerQuestionsFromAvailableCategory.TabIndex = 2;
            _buttonAnswerQuestionsFromAvailableCategory.Text = "Вопросы этой категории";
            _buttonAnswerQuestionsFromAvailableCategory.Visible = false;
            _buttonAnswerQuestionsFromAvailableCategory.Click += OnButtonAnswerQuestionsFromAvailableCategoryClick;
            // 
            // _buttonAddNewQuestion
            // 
            _buttonAddNewQuestion.Location = new Point(3, 102);
            _buttonAddNewQuestion.Name = "_buttonAddNewQuestion";
            _buttonAddNewQuestion.Size = new Size(395, 40);
            _buttonAddNewQuestion.TabIndex = 4;
            _buttonAddNewQuestion.Text = "Создать новый вопрос";
            _buttonAddNewQuestion.Click += OnButtonAddNewQuestionClick;
            // 
            // _flowLayoutPanel
            // 
            _flowLayoutPanel.Controls.Add(_label);
            _flowLayoutPanel.Controls.Add(_listBox);
            _flowLayoutPanel.Controls.Add(_buttonAnswerQuestionsFromAvailableCategory);
            _flowLayoutPanel.Controls.Add(_buttonAddNewQuestion);
            _flowLayoutPanel.Controls.Add(_buttonForPickingAnswers);
            _flowLayoutPanel.Controls.Add(_buttonChangeCategory);
            _flowLayoutPanel.Controls.Add(_buttonDisplayMenuOptions);
            _flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            _flowLayoutPanel.Location = new Point(11, 11);
            _flowLayoutPanel.Name = "_flowLayoutPanel";
            _flowLayoutPanel.Size = new Size(409, 451);
            _flowLayoutPanel.TabIndex = 0;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 451);
            Controls.Add(_flowLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MenuForm";
            Text = "Доступные вопросы";
            _flowLayoutPanel.ResumeLayout(false);
            _flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel _flowLayoutPanel;
        private Label _label;
        private ListBox _listBox;
        private Button _buttonAnswerQuestionsFromAvailableCategory;
        private Button _buttonAddNewQuestion;
        private Button _buttonForPickingAnswers;
        private Button _buttonChangeCategory;
        private Button _buttonDisplayMenuOptions;
        private List<System.Windows.Forms.RadioButton> _radioButtons;
    }
}