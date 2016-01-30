using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;

using System.Drawing;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.Quizz
{
    public partial class FrmQuizz : Form
    {
        int _questionNumber = 0;
        readonly QuizzMode _mode;
        readonly List<QuestionResult> _questionResult;
        readonly Queue<QuestionResult> _unresolved;
        readonly WritingRecognition _hanziRecognizer;
        readonly bool _mouseRecognizer;
        QuestionResult currentQuestion;

        public FrmQuizz(QuizzMode mode, List<QuestionResult> questionResult, bool mouseRecognizer, CultureInfo culture)
        {
            InitializeComponent();
            _mouseRecognizer = mouseRecognizer;
            _questionResult = questionResult;
            _unresolved = new Queue<QuestionResult>();
            _mode = mode;
            if (mode == QuizzMode.Exam)
                btnSkip.Visible = false;
            else
                btnSkip.Visible = true;
            if (_mouseRecognizer)
            {
                _hanziRecognizer = new WritingRecognition(picDraw, culture);
                _hanziRecognizer.HanziRecognized += _hanziRecognizer_HanziRecognized;

            }
            else
                tabExam.TabPages.RemoveAt(1);
            ShowNext();
        }

        void _hanziRecognizer_HanziRecognized(object sender, WritingRecognition.HanziRecognizedEventArgs e)
        {
            txtAnswer.Text = e.BestResult;
            TextChanged(sender, null);
            lstResults.Items.Clear();
            foreach (string result in e.AlternativeResults)
                lstResults.Items.Add(result);
        }

        private void ShowNext()
        {
            txtAnswer.Text = "";
            if (_hanziRecognizer != null)
            {
                _hanziRecognizer.Clear();
                lstResults.Items.Clear();
            }
            if (_questionNumber + 1 <= _questionResult.Count || (_mode == QuizzMode.Intensive && _unresolved.Count > 0))
            {
                if (_questionNumber + 1 <= _questionResult.Count)
                    currentQuestion = _questionResult[++_questionNumber - 1];
                else
                    currentQuestion = _unresolved.Dequeue();
                currentQuestion.AttemptCount++;
                if (_mode == QuizzMode.Intensive)
                    lblQuestionNumber.Text = currentQuestion.QuestionNumber + " / " + _questionResult.Count + " - " + Resources.Attempt + " " + currentQuestion.AttemptCount;
                else
                    lblQuestionNumber.Text = _questionNumber + " / " + _questionResult.Count;
                if (currentQuestion.Settings is TextSettings)
                {
                    TextSettings settings = currentQuestion.Settings as TextSettings;
                    lblContent.Font = settings.Font;
                    lblContent.BackColor = settings.BackgroundColor;
                    lblContent.ForeColor = settings.FontColor;
                    lblContent.Text = (string)currentQuestion.Question;
                    lblContent.Show();
                    picContent.Hide();
                }
                if (currentQuestion.Settings is ImageSettings)
                {
                    ImageSettings settings = currentQuestion.Settings as ImageSettings;
                    picContent.Image = FileManager.ByteArrayToImage((byte[])currentQuestion.Question);
                    picContent.SizeMode = settings.SizeMode;
                    picContent.BackColor = settings.BackgroundColor;
                    lblContent.Hide();
                    picContent.Show();

                }
                
                tabExam.SelectedIndex = 0;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            currentQuestion.UserAnswer = txtAnswer.Text;
            if ((string)currentQuestion.GoodAnswer == txtAnswer.Text)
                currentQuestion.Result = QuizzResult.Correct;
            else
            {
                currentQuestion.Result = QuizzResult.Incorrect;
                if (_mode == QuizzMode.Intensive)
                {
                    FrmAnswer frmAnswer = new FrmAnswer(currentQuestion);
                    frmAnswer.ShowDialog();
                    _unresolved.Enqueue(currentQuestion);
                }
            }
            ShowNext();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            txtAnswer.Text = "";
            Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            currentQuestion.Result = QuizzResult.Skipped;
            txtAnswer.Text = "";
            ShowNext();
        }

        private void lstResults_Click(object sender, EventArgs e)
        {
            txtAnswer.Text = (string)lstResults.Items[lstResults.SelectedIndex];
            TextChanged(sender, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _hanziRecognizer.Clear();
        }

        private void lblContent_Click(object sender, EventArgs e)
        {
            if (_hanziRecognizer != null)
                tabExam.SelectedIndex = 1;
        }

        private void txtAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            string answer = currentQuestion.GoodAnswer as String;
            if (answer == null)
                return;
            if (txtAnswer.TextLength >= answer.Length)
                e.Handled = true;
            else
            {
                if (answer[txtAnswer.TextLength] != e.KeyChar)
                    txtAnswer.SelectionBackColor = Color.FromArgb(255, 204, 204);
                else
                    txtAnswer.SelectionBackColor = Color.FromArgb(195, 239, 210);
            }
        }

        private new void TextChanged(object sender, EventArgs e)
        {
            string answer = currentQuestion.GoodAnswer as String;
            if (answer == null)
                return;

            for (int c = 0; c < answer.Length; c++)
            {
                txtAnswer.SelectionStart = c;
                txtAnswer.SelectionLength = 1;
                if (c >= txtAnswer.TextLength)
                {
                    txtAnswer.SelectionLength = 0;
                    break;
                }
                else if (answer[c] != txtAnswer.Text[c])
                    txtAnswer.SelectionBackColor = Color.FromArgb(255, 204, 204);
                else
                    txtAnswer.SelectionBackColor = Color.FromArgb(195, 239, 210);
            }
        }
    }
}