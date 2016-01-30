using System;
using System.Globalization;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.Quizz
{
    public partial class FrmStart : Form
    {
        public FrmStart()
        {
            InitializeComponent();
            if (WritingRecognition.RecognitionIsInstalled)
            {
                chkWritingRecognition.Enabled = true;
                chkWritingRecognition.Checked = QuizzSettings.Default.WritingRecognition;
                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
                object selectedItem = null;
                CultureInfo cultureInfo = QuizzSettings.Default.WritingRecognitionLanguage;
                for (int c = 0; c < cultures.Length; c++)
                {
                    if (cultures[c].LCID == cultureInfo.LCID)
                        selectedItem = cultures[c];
                    else if (cultures[c].LCID == 127)
                        continue;
                    cboWritingLanguage.Items.Add(cultures[c]);
                }
                if (selectedItem != null)
                    cboWritingLanguage.SelectedItem = selectedItem;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkMaxQuestions_CheckedChanged(object sender, EventArgs e)
        {
            numMaxQuestions.Visible = chkMaxQuestions.Checked;
        }

        private void chkWritingRecognition_CheckedChanged(object sender, EventArgs e)
        {
            cboWritingLanguage.Visible = chkWritingRecognition.Checked;
        }
    }
}