using Presentation.Helper;
using static Presentation.Helper.ControlMessages;

namespace Presentation.Forms
{
    partial class AddCategoryForm
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
            _textBoxForCategoryTitle = new TextBox();
            _listBox = new ListBox();
            _buttonAcceptCategory = new Button();
            _buttonCreateNewCategory = new Button();
            _buttonReturnToAddingQuestions = new Button();
            _flowLayoutPanel = new FlowLayoutPanel();
            _flowLayoutPanel.SuspendLayout();
            SuspendLayout();
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
            // _textBoxForCategoryTitle
            // 
            _textBoxForCategoryTitle.Location = new Point(3, 56);
            _textBoxForCategoryTitle.Name = "_textBoxForCategoryTitle";
            _textBoxForCategoryTitle.Size = new Size(322, 27);
            _textBoxForCategoryTitle.TabIndex = 1;
            _textBoxForCategoryTitle.Visible = false;
            _textBoxForCategoryTitle.TextChanged += OnTextBoxTextChanged;
            // 
            // _listBox
            // 
            _listBox.ItemHeight = 20;
            _listBox.Location = new Point(3, 23);
            _listBox.Name = "_listBox";
            _listBox.Size = new Size(322, 24);
            _listBox.TabIndex = 2;
            _listBox.AutoSize = true;
            _listBox.SelectedIndexChanged += OnListBoxForCategoriesSelectedIndexChanged;
            // 
            // _buttonAcceptCategory
            // 
            _buttonAcceptCategory.Enabled = false;
            _buttonAcceptCategory.Location = new Point(3, 89);
            _buttonAcceptCategory.Name = "_buttonAcceptCategory";
            _buttonAcceptCategory.Size = new Size(322, 40);
            _buttonAcceptCategory.TabIndex = 3;
            _buttonAcceptCategory.Text = "Выбрать эту категорию";
            _buttonAcceptCategory.Visible = false;
            _buttonAcceptCategory.Click += OnButtonAcceptCategoryClick;
            // 
            // _buttonCreateNewCategory
            // 
            _buttonCreateNewCategory.Location = new Point(3, 135);
            _buttonCreateNewCategory.Name = "_buttonCreateNewCategory";
            _buttonCreateNewCategory.Size = new Size(322, 40);
            _buttonCreateNewCategory.TabIndex = 4;
            _buttonCreateNewCategory.Text = "Создать новую категорию";
            _buttonCreateNewCategory.Click += OnButtonCreateCategoryClick;
            // 
            // _buttonReturnToAddingQuestions
            // 
            _buttonReturnToAddingQuestions.Location = new Point(3, 181);
            _buttonReturnToAddingQuestions.Name = "_buttonReturnToAddingQuestions";
            _buttonReturnToAddingQuestions.Size = new Size(322, 40);
            _buttonReturnToAddingQuestions.TabIndex = 5;
            _buttonReturnToAddingQuestions.Text = "Вернуться к вопросам";
            _buttonReturnToAddingQuestions.Visible = false;
            _buttonReturnToAddingQuestions.Click += OnButtonReturnToAddingQuestionsClick;
            // 
            // _flowLayoutPanel
            // 
            _flowLayoutPanel.Controls.Add(_label);
            _flowLayoutPanel.Controls.Add(_listBox);
            _flowLayoutPanel.Controls.Add(_textBoxForCategoryTitle);
            _flowLayoutPanel.Controls.Add(_buttonAcceptCategory);
            _flowLayoutPanel.Controls.Add(_buttonCreateNewCategory);
            _flowLayoutPanel.Controls.Add(_buttonReturnToAddingQuestions);
            _flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            _flowLayoutPanel.Location = new Point(1, 1);
            _flowLayoutPanel.Name = "_flowLayoutPanel";
            _flowLayoutPanel.Size = new Size(376, 381);
            _flowLayoutPanel.TabIndex = 0;
            // 
            // AddCategoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(390, 397);
            Controls.Add(_flowLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "AddCategoryForm";
            Text = "Выбор категории";
            _flowLayoutPanel.ResumeLayout(false);
            _flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel _flowLayoutPanel;
        private Label _label;
        private ListBox _listBox;
        private TextBox _textBoxForCategoryTitle;
        private Button _buttonAcceptCategory;
        private Button _buttonCreateNewCategory;
        private Button _buttonReturnToAddingQuestions;
    }
}