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
            _radioButtons = new List<System.Windows.Forms.RadioButton>();
            _flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            _flowLayoutPanel.Controls.Add(_label);
            _flowLayoutPanel.Controls.Add(_listBox);
            _flowLayoutPanel.Controls.Add(_buttonAnswerQuestionsFromAvailableCategory);
            _flowLayoutPanel.Controls.Add(_buttonAddNewQuestion);
            _flowLayoutPanel.Controls.Add(_buttonForPickingAnswers);
            _flowLayoutPanel.Controls.Add(_buttonChangeCategory);
            _flowLayoutPanel.Controls.Add(_buttonDisplayMenuOptions);
            _flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            _flowLayoutPanel.Location = new Point(12, 10);
            _flowLayoutPanel.Name = "flowLayoutPanel1";
            _flowLayoutPanel.Size = new Size(650, 450);
            _flowLayoutPanel.TabIndex = 0;
            // 
            // _buttonDisplayMenuOptions
            // 
            _buttonDisplayMenuOptions.Location = new Point(3, 249);
            _buttonDisplayMenuOptions.Name = "_buttonDisplayMenuOptions";
            _buttonDisplayMenuOptions.Size = new Size(395, 28);
            _buttonDisplayMenuOptions.TabIndex = 5;
            _buttonDisplayMenuOptions.Text = "Вернуться в меню";
            _buttonDisplayMenuOptions.Click += DisplayMenuOptions;
            // 
            // _buttonForPickingAnswers
            // 
            _buttonForPickingAnswers.Location = new Point(3, 181);
            _buttonForPickingAnswers.Name = "_buttonForPickingAnswers";
            _buttonForPickingAnswers.Size = new Size(395, 28);
            _buttonForPickingAnswers.TabIndex = 3;
            _buttonForPickingAnswers.Text = "Проверить ответ";
            _buttonForPickingAnswers.Visible = false;
            _buttonForPickingAnswers.Click += OnButtonForPickingAnswersClick;
            // 
            // _buttonChangeCategory
            // 
            _buttonChangeCategory.Location = new Point(3, 215);
            _buttonChangeCategory.Name = "_buttonChangeCategory";
            _buttonChangeCategory.Size = new Size(395, 28);
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
            _label.Size = new Size(42, 20);
            _label.TabIndex = 0;
            _label.Text = LabelTexts.AvailableCategories;
            // 
            // _listBox
            // 
            _listBox.ItemHeight = 20;
            _listBox.Location = new Point(3, 23);
            _listBox.Name = "_listBox";
            _listBox.Size = new Size(395, 84);
            _listBox.TabIndex = 1;
            _listBox.Visible = false;
            _listBox.SelectedIndexChanged += OnListBoxForCategoriesSelectedIndexChanged;
            // 
            // _buttonAnswerQuestionsFromAvailableCategory
            // 
            _buttonAnswerQuestionsFromAvailableCategory.Location = new Point(3, 113);
            _buttonAnswerQuestionsFromAvailableCategory.Name = "_buttonAnswerQuestionsFromAvailableCategory";
            _buttonAnswerQuestionsFromAvailableCategory.Size = new Size(395, 28);
            _buttonAnswerQuestionsFromAvailableCategory.TabIndex = 2;
            _buttonAnswerQuestionsFromAvailableCategory.Text = "Вопросы этой категории";
            _buttonAnswerQuestionsFromAvailableCategory.Visible = false;
            _buttonAnswerQuestionsFromAvailableCategory.Click += OnButtonAnswerQuestionsFromAvailableCategoryClick;
            // 
            // _buttonAddNewQuestion
            // 
            _buttonAddNewQuestion.Location = new Point(3, 147);
            _buttonAddNewQuestion.Name = "_buttonAddNewQuestion";
            _buttonAddNewQuestion.Size = new Size(395, 28);
            _buttonAddNewQuestion.TabIndex = 4;
            _buttonAddNewQuestion.Text = "Добавить новый вопрос";
            _buttonAddNewQuestion.Click += OnButtonAddNewQuestionClick;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_flowLayoutPanel);
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