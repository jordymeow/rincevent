using System.Globalization;
using Meow.FR.Rincevent.Core.Extensibility;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.Quizz
{
    [GlobalizedObject("Display.Quizz")]
    public class ModuleSettings : DisplayModuleSettings
    {
        [GlobalizedCategory("1. Basics")]
        public CultureInfo WritingRecognitionLanguage
        {
            get { return QuizzSettings.Default.WritingRecognitionLanguage; }
            set { QuizzSettings.Default.WritingRecognitionLanguage = value; Settings.Default.Save(); }
        }

        [GlobalizedCategory("1. Basics")]
        public bool WritingRecognition
        {
            get { return QuizzSettings.Default.WritingRecognition; }
            set { QuizzSettings.Default.WritingRecognition = value; Settings.Default.Save(); }
        }
    }
}
