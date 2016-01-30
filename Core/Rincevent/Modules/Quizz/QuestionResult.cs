using Meow.FR.Rincevent.Core.Data;

namespace Meow.FR.Rincevent.Display.Quizz
{
    public class QuestionResult
    {
        public int QuestionNumber;
        public QuizzResult Result;
        public int ContentIndex;
        public object Question;
        public ContentType QuestionContentType;
        public object UserAnswer;
        public object GoodAnswer;
        public ContentType AnswerContentType;
        public BaseSettings Settings;
        public int AttemptCount;
    }
}
