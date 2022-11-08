
namespace Assets.Scripts.Internationalization
{
    public static class StaticLanguageController
    {
        public static TextMessages textMessages = new EnglishTextMessages();

        public static void ChangeToSpanish()
        {
            textMessages = new SpanishTextMessages();
        }

        public static void ChangeToEnglish()
        {
            textMessages = new EnglishTextMessages();
        }
    }
}
