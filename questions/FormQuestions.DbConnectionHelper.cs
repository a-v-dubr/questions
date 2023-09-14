using Infrastructure;
using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    public partial class FormQuestions
    {
        /// <summary>
        /// Displays textbox for connection string input if the default string is invalid for connection
        /// </summary>
        private void TryEstablishDbConnection()
        {
            if (!DbHelper.TryEstablishDbConnection())
            {
                HideControls(_buttonDisplayAvailableQuestions, _buttonAddNewQuestion);
                _labelUserActionsHelper.Text = LabelTexts.AskForConnectionStringInput;
                DisplayControls(_textBoxForConnectionStringInput);
            }
        }

        /// <summary>
        /// Displays button for validating connection string typed in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxForConnectionStringInput_TextChanged(object sender, EventArgs e)
        {
            DisplayControls(_buttonCheckConnectionString);
        }

        /// <summary>
        /// Gets user to the main menu if the Db connection has been established correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCheckConnectionString_Click(object sender, EventArgs e)
        {
            if (DbHelper.TrySetValidConnectionString(_textBoxForConnectionStringInput.Text))
            {
                HideControls(_textBoxForConnectionStringInput, _buttonCheckConnectionString);
                _labelUserActionsHelper.Text = LabelTexts.DbConnectionIsSuccessful;
                DisplayControls(_buttonAddNewQuestion, _buttonDisplayAvailableQuestions);
            }
            else
            {
                _labelUserActionsHelper.Text = LabelTexts.DbConnectionFailed;
            }
        }
    }
}
