using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Extensibility;

using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.Quizz
{
    public class Module : DisplayModule
    {
        private readonly ModuleSettings _settings = new ModuleSettings();
        FrmQuizz _frmTwik;
        FrmStart _frmStart;
        FrmResult _frmResult;
        ICore _core;
        QuizzMode _mode;

        public QuizzMode PlayingMode;

        override public string Name
        {
            get { return Resources.Quizz_Name; }
        }

        override public string Description
        {
            get { return Resources.Quizz_Description; }
        }

        public override int Timer
        {
            get { return 0; }
        }

        public override DisplayModuleSettings Settings
        {
            get { return _settings; }
        }

        override public void Start(ICore core)
        {
            _core = core;
            InvokeWaitForAllDataEvent(null);
        }

        public ContentElement GetElementFromName(List<ContentElement> l, object name)
        {
            foreach (ContentElement e in l)
                if (e.Name == name as string)
                    return e;
            return null;
        }

        public override void ReceiveAllData(List<Content> content)
        {
            DialogResult finalResult = DialogResult.Abort;

            if (content.Count < 1)
                InvokeStoppedEvent(null);
            _frmStart = new FrmStart();
            _frmStart.lblQuestionNumber.Text = content.Count.ToString();
            _frmStart.numMaxQuestions.Maximum = content.Count;
            bool questionChoosen = false;
            foreach (ContentElement element in content[0].Elements)
            {
                if (element.Type == ContentType.Text || element.Type == ContentType.Image)
                {
                    _frmStart.cboQuestionConcern.Items.Add(element.Name);
                    if (element.Type == ContentType.Text)
                        _frmStart.cboAnswerConcern.Items.Add(element.Name);
                    if (!questionChoosen)
                    {
                        _frmStart.cboQuestionConcern.Text = element.Name;
                        questionChoosen = true;
                    }
                    if (element.Type == ContentType.Text)
                        _frmStart.cboAnswerConcern.Text = element.Name;
                }
            }
            if (_frmStart.ShowDialog() == DialogResult.OK)
            {
                List<QuestionResult> questionResult = new List<QuestionResult>();
                if (_frmStart.chkMaxQuestions.Checked)
                    content.RemoveRange((int)_frmStart.numMaxQuestions.Value, content.Count - (int)_frmStart.numMaxQuestions.Value);
                int num = 0;
                foreach (Content current in content)
                {
                    QuestionResult newQuestion = new QuestionResult();
                    newQuestion.QuestionNumber = ++num;
                    newQuestion.ContentIndex = current.Index;
                    newQuestion.AnswerContentType = GetElementFromName(current.Elements, _frmStart.cboAnswerConcern.SelectedItem).Type;
                    newQuestion.GoodAnswer = GetElementFromName(current.Elements, _frmStart.cboAnswerConcern.SelectedItem).Data;
                    newQuestion.QuestionContentType = GetElementFromName(current.Elements, _frmStart.cboQuestionConcern.SelectedItem).Type;
                    newQuestion.Question = GetElementFromName(current.Elements, _frmStart.cboQuestionConcern.SelectedItem).Data;
                    newQuestion.Settings = GetElementFromName(current.Elements, _frmStart.cboQuestionConcern.SelectedItem).Settings;
                    questionResult.Add(newQuestion);
                }
                if (_frmStart.chkWritingRecognition.Checked)
                {
                    QuizzSettings.Default.WritingRecognitionLanguage =
                        (CultureInfo)_frmStart.cboWritingLanguage.SelectedItem;
                    QuizzSettings.Default.WritingRecognition = true;
                }
                else
                    QuizzSettings.Default.WritingRecognition = false;
                if (_frmStart.rdoIntensive.Checked)
                    _mode = QuizzMode.Intensive;
                else
                    _mode = QuizzMode.Exam;
                
                _frmTwik = new FrmQuizz(_mode, questionResult, _frmStart.chkWritingRecognition.Checked, (CultureInfo)_frmStart.cboWritingLanguage.SelectedItem);
                _frmStart = null;
                if (_frmTwik.ShowDialog() == DialogResult.OK)
                {
                    _frmTwik = null;
                    _frmResult = new FrmResult(_mode, _core, questionResult);
                    finalResult = _frmResult.ShowDialog();
                    _frmResult = null;
                }
            }
            InvokeStoppedEvent(null);
            if (finalResult == DialogResult.Retry)
                ReceiveAllData(content);
            else if (finalResult == DialogResult.Yes)
                InvokeWaitForAllDataEvent(null);
            _core.WakeUp();
        }

        override public void Show(Content content)
        {
            return;
        }

        override public void Stop()
        {
            if (_frmStart != null)
                _frmStart.Close();
            if (_frmTwik != null)
                _frmTwik.Close();
            if (_frmResult != null)
                _frmResult.Close();
        }

        public override void BossShow()
        {
            if (_frmStart != null)
                _frmStart.Show();
            if (_frmTwik != null)
                _frmTwik.Show();
            if (_frmResult != null)
                _frmResult.Show();
        }

        public override void BossHide()
        {
            if (_frmStart != null)
                _frmStart.Hide();
            if (_frmTwik != null)
                _frmTwik.Hide();
            if (_frmResult != null)
                _frmResult.Hide();
        }
    }
}
