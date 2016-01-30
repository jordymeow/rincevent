using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Meow.FR.Rincevent.Display.Quizz
{
    public partial class FrmAnswer : Form
    {
        public FrmAnswer(QuestionResult question)
        {
            InitializeComponent();
            lblAnswer.Text = question.GoodAnswer as String;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
