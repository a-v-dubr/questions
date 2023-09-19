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
            this._flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.ControlAdded += FlowLayoutPanel_SetWidthWhenControlIsAdded;
            this._flowLayoutPanel.Controls.Add(this._labelUserActionsHelper);
            this._flowLayoutPanel.Controls.Add(this._buttonAddNewQuestion);
            this._flowLayoutPanel.Controls.Add(this._buttonDisplayAvailableQuestions);
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
            this._buttonAddNewQuestion.Click += new System.EventHandler(this.ButtonAddNewQuestion_Click);
            // 
            // _buttonDisplayAvailableQuestions
            // 
            this._buttonDisplayAvailableQuestions.Location = new System.Drawing.Point(3, 67);
            this._buttonDisplayAvailableQuestions.Name = "_buttonDisplayAvailableQuestions";
            this._buttonDisplayAvailableQuestions.Size = new System.Drawing.Size(400, 37);
            this._buttonDisplayAvailableQuestions.TabIndex = 1;
            this._buttonDisplayAvailableQuestions.Text = ButtonTexts.DisplayAvailableQuestions;
            this._buttonDisplayAvailableQuestions.UseVisualStyleBackColor = true;
            this._buttonDisplayAvailableQuestions.Click += new System.EventHandler(this.ButtonDisplayAvailableQuestions_Click);
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
            _textBoxForQuestionInput = new() { Visible = false, ScrollBars = ScrollBars.Vertical, PlaceholderText = PlaceholderTexts.TypeQuestionText };
            _textBoxForQuestionInput.TextChanged += TextBoxForQuestionInput_TextChanged;
            _textBoxForAnswerInput = new() { Visible = false, ScrollBars = ScrollBars.Vertical, PlaceholderText = PlaceholderTexts.TypeAnswerText };
            _textBoxForCreatingCategory = new() { Visible = false, ScrollBars = ScrollBars.Vertical, PlaceholderText = PlaceholderTexts.TypeCategoryTitle };
            _textBoxForCreatingCategory.TextChanged += TextBoxForCreatingCategory_TextChanged;

            _buttonCreateNewCategory = new() { Visible = false, Text = ButtonTexts.CreateNewCategory };
            _buttonCreateNewCategory.Click += ButtonCreateNewCategory_Click;
            _buttonChooseExistingCategory = new() { Visible = false, Text = ButtonTexts.ChooseAvailableCategory };
            _buttonChooseExistingCategory.Click += ButtonChooseExistingCategory_Click;
            _buttonSaveCategoryTitle = new() { Visible = false, Text = ButtonTexts.SaveCategoryTitle };
            _buttonSaveCategoryTitle.Click += ButtonSaveCategoryTitle_Click;
            _buttonAcceptCategoryChoice = new() { Visible = false, Text = ButtonTexts.AcceptCategoryChoice };
            _buttonAcceptCategoryChoice.Click += ButtonAcceptCategoryChoice_Click;
            _buttonAcceptQuestionText = new() { Visible = false, Text = ButtonTexts.AcceptQuestionText };
            _buttonAcceptQuestionText.Click += ButtonAcceptQuestionText_Click;
            _buttonAcceptAnswerText = new() { Visible = false, Text = ButtonTexts.AcceptAnswerText };
            _buttonAcceptAnswerText.Click += ButtonAcceptAnswerText_Click;
            _buttonSaveCorrectAnswerIndex = new() { Visible = false, Text = ButtonTexts.AcceptCorrectAnswerInput };
            _buttonSaveCorrectAnswerIndex.Click += ButtonSaveCorrectAnswerIndex_Click;
            _buttonChooseAvailableQuestion = new() { Visible = false, Text = ButtonTexts.AnswerTheQuestion };
            _buttonChooseAvailableQuestion.Click += ButtonChooseAvailableQuestion_Click;
            _buttonAcceptAnswerInput = new() { Visible = false, Text = ButtonTexts.PickAnswer };
            _buttonAcceptAnswerInput.Click += ButtonAcceptAnswerInput_Click;

            _comboBoxChooseAvailableQuestion = new() { Visible = false };
            _comboBoxChooseAvailableQuestion.SelectedIndexChanged += ComboBoxChooseAvailableQuestion_SelectedIndexChanged;
            _comboBoxChooseAvailableCategory = new() { Visible = false };
            _comboBoxChooseAvailableCategory.SelectedIndexChanged += ComboBoxChooseAvailableCategory_SelectedIndexChanged;

            _radioButtonsForPickingAnswer = new RadioButton[Question.AnswersCount];

            _labelErrorMessages = new() { Visible = false, ForeColor = Color.Red };

            _flowLayoutPanel.Controls.Add(_labelErrorMessages);
            _flowLayoutPanel.Controls.Add(_textBoxForAnswerInput);
            _flowLayoutPanel.Controls.Add(_textBoxForQuestionInput);
            _flowLayoutPanel.Controls.Add(_buttonAcceptQuestionText);
            _flowLayoutPanel.Controls.Add(_buttonAcceptAnswerText);
            _flowLayoutPanel.Controls.Add(_buttonSaveCorrectAnswerIndex);
            _flowLayoutPanel.Controls.Add(_comboBoxChooseAvailableQuestion);
            _flowLayoutPanel.Controls.Add(_buttonChooseAvailableQuestion);
            _flowLayoutPanel.Controls.Add(_buttonAcceptAnswerInput);
            _flowLayoutPanel.Controls.AddRange(_radioButtonsForPickingAnswer);
            _flowLayoutPanel.Controls.Add(_buttonChooseExistingCategory);
            _flowLayoutPanel.Controls.Add(_buttonCreateNewCategory);
            _flowLayoutPanel.Controls.Add(_comboBoxChooseAvailableCategory);
            _flowLayoutPanel.Controls.Add(_textBoxForCreatingCategory);
            _flowLayoutPanel.Controls.Add(_buttonSaveCategoryTitle);
            _flowLayoutPanel.Controls.Add(_buttonAcceptCategoryChoice);
        }

        private const int _controlWidth = 400;

        private FlowLayoutPanel _flowLayoutPanel;

        private Label _labelUserActionsHelper; 
        private Label _labelErrorMessages;

        private Button _buttonAddNewQuestion;
        private Button _buttonDisplayAvailableQuestions;
        private Button _buttonCreateNewCategory;
        private Button _buttonChooseExistingCategory;
        private Button _buttonSaveCategoryTitle;
        private Button _buttonAcceptCategoryChoice;
        private Button _buttonAcceptQuestionText;
        private Button _buttonAcceptAnswerText;
        private Button _buttonSaveCorrectAnswerIndex;
        private Button _buttonChooseAvailableQuestion;
        private Button _buttonAcceptAnswerInput;

        private TextBox _textBoxForQuestionInput;
        private TextBox _textBoxForAnswerInput;
        private TextBox _textBoxForCreatingCategory;

        private ComboBox _comboBoxChooseAvailableQuestion;
        private ComboBox _comboBoxChooseAvailableCategory;

        private RadioButton[] _radioButtonsForPickingAnswer;        
    }
}