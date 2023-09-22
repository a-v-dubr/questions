namespace Presentation.Helper
{
    /// <summary>
    /// Contains string constants and patterns for naming and placeholder texts of the form controls
    /// </summary>
    internal static class ControlMessages
    {
        public static class ButtonTexts
        {
            public const string AddNewQuestion = "Добавить новый вопрос";
            public const string DisplayAvailableQuestions = "Показать доступные вопросы";
            public const string ChooseAvailableCategory = "Выбрать существующую категорию";
            public const string CreateNewCategory = "Создать новую категорию";
            public const string AcceptCategoryChoice = "Выбрать эту категорию";
            public const string SaveCategoryTitle = "Сохранить название категории";
            public const string AcceptQuestionText = "Сохранить вопрос";
            public const string FinishAddingAnswers = "Завершить ввод ответов";
            public const string AcceptAnswerText = "Сохранить вариант ответа";
            public const string AcceptCorrectAnswerInput = "Сохранить правильный ответ";
            public const string AnswerTheQuestion = "Ответить на вопрос";
            public const string PickAnswer = "Проверить ответ";
            public const string ExitProgram = "Выход из программы";
            public const string ReturnToMainMenu = "Вернуться в меню";
            public const string EditQuestion = "Редактировать вопрос";
            public const string SaveTextChanges = "Сохранить изменения";
        }

        public static class LabelTexts
        {
            public const string ChooseMainMenuAction = "Выберите действие:";

            public const string ChooseOrCreateCategory = "Выберите категорию вопросов или создайте новую:";
            public const string AvailableCategories = "Доступные категории:";
            public const string CreateCategory = "Создаём новую категорию:";

            public readonly static string ChooseQuestionInCategory = "Выберите вопрос в категории \"{0}\":";
            public const string NoQuestionsAvailable = "Доступных вопросов нет. Вы можете добавить новый вопрос.";
            public readonly static string CreateQuestion = "Создаём новый вопрос в категории \"{0}\":";
            public readonly static string DisplayQuestionWhileAddingAnswers = "Вопрос: \n{0}\n";
            public readonly static string DisplayAnswersWhileAddingAnswers = "\nВариант ответа #{0}:\n{1}\n";
            public const string DuplicateQuestionsError = "Вы ввели повторяющиеся ответы. Начните заново.";
            public readonly static string SetCorrectAnswer = "Укажите правильный ответ на вопрос:\n{0}";
            public const string QuestionIsSavedAndAvailable = "Вопрос сохранён и помещён в список доступных вопросов.";
            public readonly static string WhenQuestionIsAvailable = "\nВопрос будет доступен {0}.";
            public const string CorrectAnswer = "Вы дали правильный ответ!";
            public const string WrongAnswer = "Вы дали неправильный ответ!";
            public const string EmptyFormsError = "Чтобы сохранить изменения, заполните все формы";
            public const string TypeNewTextValues = "Введите новые значения вопроса и ответов:";
        }

        public static class PlaceholderTexts
        {
            public const string TypeCategoryTitle = "Введите название новой категории";
            public const string TypeQuestionText = "Введите вопрос";
            public const string TypeAnswerText = "Введите вариант ответа";
        }
    }
}
