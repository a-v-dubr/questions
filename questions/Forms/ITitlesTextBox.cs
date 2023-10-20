namespace Presentation
{
    /// <summary>
    /// Defines the contract for the forms which use textbox 
    /// </summary>
    public interface ITitlesTextBox
    {
        TextBox TextBox { get; }
        void OnTextBoxTextChanged(object sender, EventArgs e);
    }
}
