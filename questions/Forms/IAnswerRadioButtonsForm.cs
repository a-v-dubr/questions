namespace Presentation.Forms
{
    /// <summary>
    /// Defines the contract for the forms which use radiobuttons
    /// </summary>
    public interface IAnswerRadioButtonsForm
    {
        List<RadioButton> RadioButtonsForAnswers { get; }
        void OnRadioButtonForAnswerCheckedChanged(object sender, EventArgs e);

        /// <summary>
        /// Creates disabled radiobutton with defined text and adds it to the flow layout panel
        /// </summary>
        /// <param name="text"></param>
        void CreateAnswerRadiobutton(string text)
        {
            var r = new RadioButton() { Enabled = false, Text = text };
            RadioButtonsForAnswers.Add(r);

            var form = this as FormQuestionsBase;
            form?.AddControlToFlowLayoutPanel(r);
        }

        /// <summary>
        /// Enables radiobutton and subscribes it to method for defining a correct answer or picking an answer 
        /// </summary>
        /// <param name="r"></param>
        void EnableAnswerRadioButton(RadioButton r)
        {
            r.CheckedChanged += OnRadioButtonForAnswerCheckedChanged!;
            r.Enabled = true;
        }

        /// <summary>
        /// Displays answers' options of the defined question 
        /// </summary>
        void DisplayAnswersOfQuestionOrDTO();
    }
}
