using Domain;
using Presentation.Helper;
using System.Windows.Forms;
using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    partial class FormQuestionsBase
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
        protected virtual void InitializeComponent()
        {
            _flowLayoutPanel = new FlowLayoutPanel();
            _label = new Label();
            _flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            _flowLayoutPanel.ControlAdded += SetWidthWhenControlIsAdded;
            this._flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            _flowLayoutPanel.Controls.Add(_label);
            _flowLayoutPanel.Location = new Point(12, 10);
            _flowLayoutPanel.Name = "flowLayoutPanel";
            _flowLayoutPanel.Size = new Size(650, 450);
            _flowLayoutPanel.TabIndex = 0;            
            // 
            // _label
            // 
            _label.AutoSize = true;
            _label.Location = new Point(3, 0);
            _label.Name = "label1";
            _label.Size = new Size(50, 20);
            _label.TabIndex = 0;
            _label.Text = "label1";
            // 
            // FormQuestionsBase
            // 
            ClientSize = new Size(590, 493);
            Controls.Add(_flowLayoutPanel);
            Name = "FormQuestionsBase";
            _flowLayoutPanel.ResumeLayout(false);
            _flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        protected FlowLayoutPanel _flowLayoutPanel;
        protected Label _label;
    }
}