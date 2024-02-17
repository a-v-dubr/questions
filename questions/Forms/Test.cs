using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation.Forms
{
    public partial class Test : Form
    {
        private const int MaxHeightWithoutScroll = 300;
        private const int RowHeight = 30; // Adjust as needed
        private TableLayoutPanel tableLayoutPanel;

        public Test()
        {
            InitializeComponent();
            tableLayoutPanel1.Visible = false;
            InitializeTableLayoutPanel();
        }

        private void InitializeTableLayoutPanel()
        {
            tableLayoutPanel = new();
            // Add initial rows
            AddRows(2);
        }

        private void AddRows(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                // Add your controls or content here to each row
                var label = new Label();
                label.Text = "Row " + tableLayoutPanel.RowCount;
                tableLayoutPanel.Controls.Add(label, 0, tableLayoutPanel.RowCount);

                tableLayoutPanel.RowCount++;
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, RowHeight));
            }

            UpdateTableLayoutPanelHeight();
        }

        private void UpdateTableLayoutPanelHeight()
        {
            int totalHeight = tableLayoutPanel.RowStyles.Count * RowHeight;
            if (totalHeight <= MaxHeightWithoutScroll)
            {
                tableLayoutPanel.Height = totalHeight;
                tableLayoutPanel.AutoScroll = false;
            }
            else
            {
                tableLayoutPanel.Height = MaxHeightWithoutScroll;
                tableLayoutPanel.AutoScroll = true;
            }
        }

        // This method can be called when you need to add more rows dynamically
        private void AddMoreRowsButton_Click(object sender, EventArgs e)
        {
            AddRows(5); // Add 5 more rows, for example
        }

    }
}
