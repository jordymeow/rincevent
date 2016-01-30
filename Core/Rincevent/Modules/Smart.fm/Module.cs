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

namespace Meow.FR.Rincevent.IO.SmartFM
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
            get { return IOModuleType.Import; }
        }

        public override void Export(ContentManager contentManager)
        {
            throw new NotSupportedException();
        }

        public override ContentManager Import()
        {
            var window = new Meow.FR.Rincevent.IO.SmartFM.WndMain();
            window.ShowDialog();
            return window.Content;
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
