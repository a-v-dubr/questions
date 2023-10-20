using Domain;
using static Presentation.Helper.ControlMessages;

namespace Presentation
{
    /// <summary>
    /// Defines the contract for the forms which use listbox 
    /// </summary>
    public interface ICategoriesListBoxForm
    {
        ListBox ListBoxForCategories { get; }
        void OnListBoxForCategoriesSelectedIndexChanged(object sender, EventArgs e);

        /// <summary>
        /// Adds unique categories to the listbox
        /// </summary>
        /// <param name="categories"></param>
        void AddCategoriesToListBox(List<Category> categories)
        {
            if (ListBoxForCategories.Items.Count > 0)
            {
                ListBoxForCategories.Items.Clear();
            }

            int questionsCount;
            foreach (var c in categories)
            {
                questionsCount = c.Questions.Where(q => q.AvailableAt <= DateTime.Now).Count();
                if (questionsCount > 0)
                {
                    ListBoxForCategories.Items.Add(string.Format(ListBoxTexts.AvailableCategories, c.Title, questionsCount));
                }
            }
        }
    }
}
