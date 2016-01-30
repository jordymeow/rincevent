using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Extensibility;
using Meow.FR.Rincevent.Core.Data;
using System.IO;
using System.Diagnostics;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class FrmPrint : Form
    {
        ContentManager _contentManager;

        public FrmPrint(ContentManager contentManager)
        {
            InitializeComponent();
            _contentManager = contentManager;
            cmbColsNumber.Items.AddRange(new object [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            cmbColsNumber.SelectedItem = 1;
            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Rincevent.html";
        }

        private void btnExport_Click(object sender, EventArgs ev)
        {
            try
            {
                int colsNumber = (int)cmbColsNumber.SelectedItem;
                TextWriter writer = File.CreateText(txtPath.Text);
                FileInfo info = new FileInfo(txtPath.Text);
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                writer.WriteLine("<HTML>");
                writer.WriteLine("<HEAD>");
                writer.WriteLine("<STYLE>");
                writer.WriteLine("body {");
                writer.WriteLine("	font-family: Verdana;");
                writer.WriteLine("}");
                writer.WriteLine("table {");
                writer.WriteLine("	width: 100%;");
                writer.WriteLine("	text-align: left;");
                writer.WriteLine("	border: black solid 1px;");
                writer.WriteLine("	border-collapse: collapse;");
                writer.WriteLine("}");
                writer.WriteLine("table th {");
                writer.WriteLine("	color: White;");
                writer.WriteLine("	background-color: Black;");
                writer.WriteLine("	border: Black Solid 1px;");
                writer.WriteLine("}");
                writer.WriteLine("table td {");
                writer.WriteLine("	border: Black Solid 1px;");
                writer.WriteLine("}");
                writer.WriteLine("table .separator {");
                writer.WriteLine("	background: Black;");
                writer.WriteLine("}");
                writer.WriteLine("img {");
                writer.WriteLine("	width: 100px;");
                writer.WriteLine("}");
                writer.WriteLine("</STYLE>");
                writer.WriteLine("</HEAD>");
                writer.WriteLine("<BODY>");
                writer.WriteLine("<TABLE>");
                writer.Write("<TR>");
                for (int e = 0; e < colsNumber; e++)
                {
                    for (int c = 0; c < _contentManager.ColumnNames.Length; c++)
                        if (_contentManager.ColumnTypes[c] == ContentType.Text || _contentManager.ColumnTypes[c] == ContentType.Image)
                            writer.Write("<TH>" + _contentManager.ColumnNames[c] + "</TH>");
                    if (colsNumber > 1 && e < colsNumber - 1)
                        writer.Write("<TD CLASS='separator'> </TD>");
                }
                writer.WriteLine("</TR>");
                List<Content> lstContent = _contentManager.GetAllWantedItems();
                for (int d = 0; d < lstContent.Count; d++)
                {
                    writer.Write("<TR>");
                    int e = 0;
                    for (; e < colsNumber && e + d < lstContent.Count; e++)
                    {
                        Content content = lstContent[e + d];
                        for (int c = 0; c < content.Count; c++)
                            if (content.Elements[c].Type == ContentType.Text)
                                writer.Write("<TD>" + content.Elements[c].Data.ToString() + "</TD>");
                            else if (content.Elements[c].Type == ContentType.Image)
                            {
                                if (!Directory.Exists(txtPath.Text + ".content/"))
                                    Directory.CreateDirectory(txtPath.Text + ".content/");
                                FileStream newFile = File.Create(txtPath.Text + ".content/" + (e + d).ToString() + ".data");
                                newFile.Write((byte[])content.Elements[c].Data, 0, ((byte[])content.Elements[c].Data).Length);
                                newFile.Close();
                                writer.Write("<TD><IMG SRC='" + info.Name + ".content\\" + (e + d).ToString() + ".data" + "' /></TD>");
                            }
                        if (colsNumber > 1 && e < colsNumber - 1)
                            writer.Write("<TD CLASS='separator'> </TD>");
                    }
                    d += e;
                    writer.WriteLine("</TR>");
                }
                writer.WriteLine("</TABLE>");
                writer.WriteLine("</BODY>");
                writer.WriteLine("</HTML>");
                writer.Close();
                Close();
                Process.Start(txtPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckPathExists = true;
            if (saveDialog.ShowDialog() == DialogResult.OK)
                txtPath.Text = saveDialog.FileName;
        }
    }
}
