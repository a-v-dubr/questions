using Domain;
using Presentation.Helper;
using System.Windows.Forms;
using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    partial class FormQuestions
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._labelUserActionsHelper = new System.Windows.Forms.Label();
            this._buttonAddNewQuestion = new System.Windows.Forms.Button();
            this._buttonDisplayAvailableQuestions = new System.Windows.Forms.Button();
            this._buttonExitProgram = new System.Windows.Forms.Button();
            this._flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.ControlAdded += FlowLayoutPanel_SetWidthWhenControlIsAdded;
            this._flowLayoutPanel.Controls.Add(this._labelUserActionsHelper);
            this._flowLayoutPanel.Controls.Add(this._buttonAddNewQuestion);
            this._flowLayoutPanel.Controls.Add(this._buttonDisplayAvailableQuestions);
            this._flowLayoutPanel.Controls.Add(this._buttonExitProgram);
            this._flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flowLayoutPanel.Location = new System.Drawing.Point(4, 3);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Size = new System.Drawing.Size(600, 450);
            this._flowLayoutPanel.TabIndex = 0;
            // 
            // _labelUserActionsHelper
            // 
            this._labelUserActionsHelper.Location = new System.Drawing.Point(3, 0);
            this._labelUserActionsHelper.Name = "_labelUserActionsHelper";
            this._labelUserActionsHelper.Size = new System.Drawing.Size(400, 23);
            this._labelUserActionsHelper.TabIndex = 2;
            this._labelUserActionsHelper.Text = LabelTexts.ChooseMainMenuAction;
            // 
            // _buttonAddNewQuestion
            // 
            this._buttonAddNewQuestion.Location = new System.Drawing.Point(3, 26);
            this._buttonAddNewQuestion.Name = "_buttonAddNewQuestion";
            this._buttonAddNewQuestion.Size = new System.Drawing.Size(400, 37);
            this._buttonAddNewQuestion.TabIndex = 0;
            this._buttonAddNewQuestion.Text = ButtonTexts.AddNewQuestion;
            this._buttonAddNewQuestion.UseVisualStyleBackColor = true;
            this._buttonAddNewQuestion.Click += new System.EventHandler(this.OnButtonAddNewQuestionClick);
            // 
            // _buttonDisplayAvailableQuestions
            // 
            this._buttonDisplayAvailableQuestions.Location = new System.Drawing.Point(3, 67);
            this._buttonDisplayAvailableQuestions.Name = "_buttonDisplayAvailableQuestions";
            this._buttonDisplayAvailableQuestions.Size = new System.Drawing.Size(400, 37);
            this._buttonDisplayAvailableQuestions.TabIndex = 1;
            this._buttonDisplayAvailableQuestions.Text = ButtonTexts.DisplayAvailableQuestions;
            this._buttonDisplayAvailableQuestions.UseVisualStyleBackColor = true;
            this._buttonDisplayAvailableQuestions.Click += new System.EventHandler(this.OnButtonDisplayAvailableQuestionsClick);
            // 
            // _buttonExitProgram
            // 
            this._buttonExitProgram.Name = "_buttonExitProgram";
            this._buttonExitProgram.Size = new System.Drawing.Size(400, 37);
            this._buttonExitProgram.TabIndex = 2;
            this._buttonExitProgram.Text = ButtonTexts.ExitProgram;
            this._buttonExitProgram.UseVisualStyleBackColor = true;
            this._buttonExitProgram.Click += new System.EventHandler(this.OnButtonExitProgramClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._flowLayoutPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this._flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private void InitializeControls()
        {
            _labelErrorMessages = new() { Visible = false, ForeColor = Color.Red };
            _flowLayoutPanel.Controls.Add(_labelErrorMessages);

            _textBox = new() { Visible = false, ScrollBars = ScrollBars.Vertical };
            _textBox.TextChanged += OnTextBoxTextChanged;
           _textBox.Click += OnTextBoxClick;
            _flowLayoutPanel.Controls.Add(_textBox);

            _comboBox = new() { Visible = false, DropDownStyle = ComboBoxStyle.DropDownList };
            _comboBox.SelectedIndexChanged += OnComboBoxSelectedIndexChanged;
            _flowLayoutPanel.Controls.Add(_comboBox);

            _buttonChooseAvailableQuestion = new() { Visible = false };
            _buttonChooseAvailableQuestion.Click += OnButtonChooseAvailableQuestionClick;
            _flowLayoutPanel.Controls.Add(_buttonChooseAvailableQuestion);            

            _buttonForPickingAnswers = new() { Visible = false };
            _buttonForPickingAnswers.Click += OnButtonForPickingAnswersClick;
            _flowLayoutPanel.Controls.Add(_buttonForPickingAnswers);

            _buttonReturnToMainMenu = new() { Visible = false, Text = ButtonTexts.ReturnToMainMenu };
            _buttonReturnToMainMenu.Click += OnButtonReturnToMainMenuClick;
            _flowLayoutPanel.Controls.Add(_buttonReturnToMainMenu);

            _buttonEditOrCreateQuestion = new() { Visible = false, Text = ButtonTexts.EditQuestion };
            _buttonEditOrCreateQuestion.Click += OnButtonEditOrCreateQuestionClick;
            _flowLayoutPanel.Controls.Add(_buttonEditOrCreateQuestion);
        }
        

        private FlowLayoutPanel _flowLayoutPanel;

        private Label _labelUserActionsHelper;
        private Label _labelErrorMessages;
        private Button _buttonAddNewQuestion;
        private Button _buttonDisplayAvailableQuestions;
        private Button _buttonExitProgram;
        private Button _buttonReturnToMainMenu;

        private Button _buttonChooseAvailableQuestion;
        private ComboBox _comboBox;
        private List<RadioButton> _radioButtonsForAnswers = new();
        private Button _buttonForPickingAnswers;

        private Button _buttonEditOrCreateQuestion;
        private TextBox _textBox;
    }
}