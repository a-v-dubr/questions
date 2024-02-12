namespace Presentation.Helper
{
    internal static class ControlsHelper
    {
        private const int _controlWidth = 350;

        // <summary>
        /// Sets autoheight and fixed width for the control added to the flow layout panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SetWidthWhenControlIsAdded(object obj, EventArgs e)
        {
            if (obj is FlowLayoutPanel flowLayoutPanel)
            {
                int index = flowLayoutPanel.Controls.Count - 1;
                var control = flowLayoutPanel.Controls[index];
                control.AutoSize = true;
                control.Width = _controlWidth;
                if (control is Label)
                {
                    control.MaximumSize = new(_controlWidth, 0);
                }
            }
        }

        /// <summary>
        /// Removes control from the flow layout panel controls' list
        /// </summary>
        /// <param name="control"></param>
        public static void RemoveControlFromFlowLayoutPanel<T>(List<T> controls, FlowLayoutPanel flowLayoutPanel) where T : Control
        {
            foreach (var control in from control in controls
                                    where flowLayoutPanel.Controls.Contains(control)
                                    select control)
            {
                flowLayoutPanel.Controls.Remove(control);
            }
        }

        /// <summary>
        /// Disables visibility for form control elements
        /// </summary>
        /// <param name="controls"></param>
        public static void HideControls(params Control[] controls)
        {
            foreach (var c in controls)
            {
                c.Visible = false;
            }
        }

        /// <summary>
        /// Enables visibility for form control elements
        /// </summary>
        /// <param name="controls"></param>
        public static void DisplayControls(params Control[] controls)
        {
            foreach (var c in controls)
            {
                c.Visible = true;
            }
        }

        /// <summary>
        /// Adds unique control to the flow layout panel
        /// </summary>
        /// <param name="control"></param>
        public static void AddControlToFlowLayoutPanel(this FlowLayoutPanel flowLayoutPanel, params Control[] controls)
        {
            foreach (var control in controls)
            {
                if (control is not null && !flowLayoutPanel.Contains(control))
                {
                    flowLayoutPanel.Controls.Add(control);
                }
            }
        }

        /// <summary>
        /// Displays a control below other panel elements
        /// </summary>
        /// <param name="control"></param>
        public static void DisplayControlBelowOthersInFlowPanel(FlowLayoutPanel flowLayoutPanel, Control control)
        {
            flowLayoutPanel.Controls.SetChildIndex(control, flowLayoutPanel.Controls.Count);
            if (control.Visible == false)
            {
                DisplayControls(control);
            }
        }
    }
}
