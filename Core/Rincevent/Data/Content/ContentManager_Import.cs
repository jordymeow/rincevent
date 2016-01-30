using System.Data;

namespace Meow.FR.Rincevent.Core.Data
{
    public partial class ContentManager
    {
        static public ContentManager ImportBetaRinceventFile(string fileName)
        {
            ContentManager contentManager = new ContentManager();
            DataSet ds = new DataSet("MeowRincevent");
            ds.ReadXml(fileName);
            DataTable tableWord = ds.Tables["word"];
            DataTable tableSet = ds.Tables["set"];
            if (tableWord.Columns.Contains("checked"))
                tableWord.Columns.Remove("checked");
            foreach (DataRow current in tableSet.Rows)
                contentManager.ColumnAdd((string)current.ItemArray[1], ContentType.Text);
            foreach (DataRow current in tableWord.Rows)
                contentManager.ItemAdd(current.ItemArray);
            return contentManager;
        }
    }
}
