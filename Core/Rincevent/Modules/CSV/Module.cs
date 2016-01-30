using System;
using System.Collections.Generic;
using System.Text;
using Meow.FR.Rincevent.Core.Extensibility;
using System.Globalization;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.IO.CSV;
using System.IO;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.IO.CSV
{
    public class Module : IOModule
    {
        public override string Description
        {
            get { return Resources.CSV_Description; }
        }

        public override string Name
        {
            get { return Resources.CSV_Name; }
        }

        public override IOModuleType Type
        {
            get { return IOModuleType.Both; }
        }

        public override void Export(ContentManager contentManager)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.CheckPathExists = true;
            fileDialog.OverwritePrompt = true;
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV (*.csv)|*.csv";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter stream = new StreamWriter(fileDialog.FileName, false, Encoding.UTF8))
                {
                    List<Content> items = contentManager.GetAllWantedItems();
                    bool isFirst = true;
                    foreach (Content item in items)
                    {
                        StringBuilder line = new StringBuilder();
                        if (isFirst)
                        {
                            foreach (ContentElement element in item.Elements)
                            {
                                if (((string)element.Name).IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                                    line.Append("\"" + ((string)element.Name).Replace("\"", "\"\"") + "\"");
                                else
                                    line.Append(element.Name.ToString());
                                line.Append(',');
                            }
                            if (line[line.Length - 1] == ',')
                                line.Remove(line.Length - 1, 1);
                            stream.WriteLine(line);
                            isFirst = false;
                            line = new StringBuilder();
                        }
                        foreach (ContentElement element in item.Elements)
                        {
                            if (element.Type == ContentType.Text)
                            {
                                if (((string)element.Data).IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                                    line.Append("\"" + ((string)element.Data).Replace("\"", "\"\"") + "\"");
                                else
                                    line.Append(element.Data.ToString());
                            }
                            line.Append(',');
                        }
                        if (line[line.Length - 1] == ',')
                            line.Remove(line.Length - 1, 1);
                        stream.WriteLine(line);
                    }
                }
            }
        }

        public override ContentManager Import()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV (*.csv)|*.csv";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FrmCsv frmCsv = new FrmCsv(fileDialog.FileName);
                if (frmCsv.ShowDialog() == DialogResult.OK)
                    return frmCsv.ContentManager;
                else
                    return null;
            }
            return null;
        }

        public override object AsyncState
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Threading.WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CompletedSynchronously
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsCompleted
        {
            get { throw new NotImplementedException(); }
        }

        public override IAsyncResult BeginImport()
        {
            throw new NotImplementedException();
        }

        public override ContentManager EndImport(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public override IAsyncResult BeginExport(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public override void EndExport(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }
    }
}
