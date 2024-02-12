using Presentation.Helper;
using System.Windows.Forms;
using static Presentation.Helper.ControlMessages;

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
            _label = new Label();
            _textBoxForQuestionTitle = new TextBox();
            _buttonAcceptQuestionText = new Button();
            _buttonFinishAddingAnswers = new Button();
            _buttonChangeQuestionCategory = new Button();
            _buttonDisplayMenu = new Button();
            _flowLayoutPanel = new FlowLayoutPanel();
            _flowLayoutPanel.SuspendLayout();
            _radioButtons = new List<System.Windows.Forms.RadioButton>();
            SuspendLayout();
            // 
            // _label
            // 
            _label.AutoSize = true;
            _label.Location = new Point(3, 0);
            _label.Name = "_label";
            _label.Size = new Size(169, 20);
            _label.TabIndex = 0;
            _label.Text = "Введите текст вопроса:";
            // 
            // _textBoxForQuestionTitle
            // 
            _textBoxForQuestionTitle.Location = new Point(3, 23);
            _textBoxForQuestionTitle.Name = "_textBoxForQuestionTitle";
            _textBoxForQuestionTitle.Size = new Size(348, 27);
            _textBoxForQuestionTitle.TabIndex = 1;
            _textBoxForQuestionTitle.TextChanged += OnTextBoxTextChanged;
            // 
            // _buttonAcceptQuestionText
            // 
            _buttonAcceptQuestionText.Enabled = false;
            _buttonAcceptQuestionText.Location = new Point(3, 56);
            _buttonAcceptQuestionText.Name = "_buttonAcceptQuestionText";
            _buttonAcceptQuestionText.Size = new Size(348, 28);
            _buttonAcceptQuestionText.TabIndex = 2;
            _buttonAcceptQuestionText.Visible = false;
            _buttonAcceptQuestionText.Text = ButtonTexts.AcceptQuestionText;
            _buttonAcceptQuestionText.Click += OnButtonAcceptQuestionTextClick;
            // 
            // _buttonFinishAddingAnswers
            // 
            _buttonFinishAddingAnswers.Enabled = false;
            _buttonFinishAddingAnswers.Location = new Point(3, 90);
            _buttonFinishAddingAnswers.Name = "_buttonFinishAddingAnswers";
            _buttonFinishAddingAnswers.Size = new Size(348, 28);
            _buttonFinishAddingAnswers.TabIndex = 3;
            _buttonFinishAddingAnswers.Visible = false;
            _buttonFinishAddingAnswers.Text = ButtonTexts.FinishAddingAnswers;
            _buttonFinishAddingAnswers.Click += OnButtonFinishAddingAnswersClick;
            // 
            // _buttonChangeQuestionCategory
            // 
            _buttonChangeQuestionCategory.Location = new Point(3, 124);
            _buttonChangeQuestionCategory.Name = "_buttonChangeQuestionCategory";
            _buttonChangeQuestionCategory.Size = new Size(348, 28);
            _buttonChangeQuestionCategory.TabIndex = 4;
            _buttonChangeQuestionCategory.Text = "Выбрать категорию для вопроса";
            _buttonChangeQuestionCategory.Visible = false;
            _buttonChangeQuestionCategory.Click += OnButtonChangeQuestionCategoryClick;
            // 
            // _buttonDisplayMenu
            // 
            _buttonDisplayMenu.Location = new Point(3, 158);
            _buttonDisplayMenu.Name = "_buttonDisplayMenu";
            _buttonDisplayMenu.Size = new Size(348, 28);
            _buttonDisplayMenu.TabIndex = 5;
            _buttonDisplayMenu.Text = "Продолжить создание вопросов";
            _buttonDisplayMenu.Visible = false;
            _buttonDisplayMenu.Click += OnButtonDisplayMenu;
            // 
            // _flowLayoutPanel
            // 
            _flowLayoutPanel.Controls.Add(_label);
            _flowLayoutPanel.Controls.Add(_textBoxForQuestionTitle);
            _flowLayoutPanel.Controls.Add(_buttonAcceptQuestionText);
            _flowLayoutPanel.Controls.Add(_buttonFinishAddingAnswers);
            _flowLayoutPanel.Controls.Add(_buttonChangeQuestionCategory);
            _flowLayoutPanel.Controls.Add(_buttonDisplayMenu);
            _flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            _flowLayoutPanel.Location = new Point(4, 8);
            _flowLayoutPanel.Name = "_flowLayoutPanel";
            _flowLayoutPanel.Size = new Size(562, 381);
            _flowLayoutPanel.TabIndex = 0;
            // 
            // AddQuestionsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_flowLayoutPanel);
            Name = "AddQuestionsForm";
            Text = "Добавление новых вопросов";
            _flowLayoutPanel.ResumeLayout(false);
            _flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }       

        #endregion

        private FlowLayoutPanel _flowLayoutPanel;
        private Label _label;
        private TextBox _textBoxForQuestionTitle;
        private Button _buttonAcceptQuestionText;
        private Button _buttonFinishAddingAnswers;
        private Button _buttonChangeQuestionCategory;
        private Button _buttonDisplayMenu;
        private List<System.Windows.Forms.RadioButton> _radioButtons;
    }
}