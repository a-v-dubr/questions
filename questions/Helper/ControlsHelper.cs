namespace Presentation.Helper
{
    internal static class ControlsHelper
    {
        /// <summary>
        /// Removes control from the flow layout panel controls' list
        /// </summary>
        /// <param name="control"></param>
        public static void RemoveControlFromFlowLayoutPanel<T>(this FlowLayoutPanel flowLayoutPanel, List<T> controls) where T : Control
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
        public static void DisplayControlBelowOthersInFlowPanel(this FlowLayoutPanel flowLayoutPanel, Control control)
        {
            flowLayoutPanel.Controls.SetChildIndex(control, flowLayoutPanel.Controls.Count);
            if (!control.Visible)
            {
                DisplayControls(control);
            }
        }
    }
}
