using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using Meow.FR.Rincevent.Core.Data;
using System.IO;
using System.Drawing.Imaging;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.IO.SmartFM
{
    public class SmartParser : DispatcherObject, INotifyPropertyChanged
    {
        BackgroundWorker bWorker = new BackgroundWorker();

        public event PropertyChangedEventHandler PropertyChanged = (a, b) => { };

        public bool IsIdle
        {
            get { return _IsIdle; }
            set { _IsIdle = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsIdle")); }
        }
        private bool _IsIdle;

        public SmartElement Current
        {
            get { return _Current; }
            set { _Current = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Current")); }
        }
        private SmartElement _Current;

        public int Count
        {
            get { return _Count; }
            set { _Count = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Count")); }
        }
        private int _Count;

        public int Loaded
        {
            get { return _Loaded; }
            set { _Loaded = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Loaded")); }
        }
        private int _Loaded;

        public string TextColumn
        {
            get { return _TextColumn; }
            set { _TextColumn = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TextColumn")); }
        }
        private string _TextColumn;

        public string CharacterColumn
        {
            get { return _CharacterColumn; }
            set { _CharacterColumn = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CharacterColumn")); }
        }
        private string _CharacterColumn;

        public string ResponseColumn
        {
            get { return _ResponseColumn; }
            set { _ResponseColumn = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TranslationColumn")); }
        }
        private string _ResponseColumn;

        public string ListId
        {
            get { return _ListId; }
            set { _ListId = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ListId")); }
        }
        private string _ListId;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Status")); }
        }
        private string _Status;

        private bool HasPictures
        {
            get { return _HasPictures; }
            set { _HasPictures = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("HasPictures")); }
        }
        private bool _HasPictures;

        private bool HasCharacters
        {
            get { return _HasCharacters; }
            set { _HasCharacters = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("HasCharacters")); }
        }
        private bool _HasCharacters;

        private bool HasText
        {
            get { return _HasText; }
            set { _HasText = value; PropertyChanged.Invoke(this, new PropertyChangedEventArgs("HasText")); }
        }
        private bool _HasText;

        public List<SmartElement> Elements { get; set; }

        public event Action<ContentManager> JobDone;

        public SmartParser()
        {
            Loaded = -1;
            ListId = "24532";
            TextColumn = "Text";
            ResponseColumn = "Response";
            CharacterColumn = "Character";
            bWorker.WorkerSupportsCancellation = true;
            bWorker.DoWork += new DoWorkEventHandler(bWorker_DoWork);
            bWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bWorker_RunWorkerCompleted);
            IsIdle = true;
            Elements = new List<SmartElement>();
            Status = "Idle.";
        }

        public void Parse()
        {
            if (!IsIdle)
                bWorker.CancelAsync();
            Loaded = -1;
            Elements.Clear();
            bWorker.RunWorkerAsync();
            IsIdle = false;
        }

        void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsIdle = true;
            if (!e.Cancelled && e.Error == null)
            {
                Write(ListId);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("An error happened: " + e.Error.Message + " Are you sure the list number is correct?", "Smart.fm Importer", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static XPathDocument CreateXPathDocumentFromUri(string uri)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                webRequest.Proxy = WebProxy.GetDefaultProxy();
                ((WebProxy)webRequest.Proxy).UseDefaultCredentials = true;
                webRequest.UserAgent = "Rincevent";
                XmlTextReader reader = null;
                reader = new XmlTextReader(webRequest.GetResponse().GetResponseStream());
                reader.MoveToContent();
                XPathDocument xPathDoc = new XPathDocument(reader);
                reader.Close();
                return xPathDoc;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                    return CreateXPathDocumentFromUri(uri);
                else
                    throw;
            }
        }

        static byte[] SearchImageFor(string toSearch)
        {
            XPathDocument xDocPics = CreateXPathDocumentFromUri("http://api.iknow.co.jp/sentences/matching/" + toSearch + ".xml");
            XPathNavigator navPics = xDocPics.CreateNavigator();
            XPathNodeIterator xPicItems = navPics.SelectChildren("sentences", "http://api.smart.fm/specifications/sentences/1.0");
            while (xPicItems.MoveNext())
            {
                XPathNodeIterator xPicItem = xPicItems.Current.SelectChildren("sentence", "http://api.smart.fm/specifications/sentences/1.0");
                if (xPicItem.MoveNext())
                {
                    XPathNodeIterator xPic = xPicItem.Current.SelectChildren("square_image", "http://api.smart.fm/specifications/sentences/1.0");
                    if (xPic.MoveNext())
                    {
                        if (!string.IsNullOrEmpty(xPic.Current.InnerXml))
                        {
                            byte[] imgBytes = FileManager.FileToByteArray(xPic.Current.InnerXml);
                            if (imgBytes != null)
                                return imgBytes;
                        }
                    }
                }
            }
            return null;
        }

        void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int currentCount = 0, page = 1;
            HasPictures = true;
            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() =>
            {
                HasCharacters = true;
                HasText = true;
                HasPictures = true;
                Elements.Clear();
                Status = "Counting items for #" + ListId + "...";
            }));
            XPathDocument xPathDoc = CreateXPathDocumentFromUri("http://api.iknow.co.jp/lists/" + ListId + ".xml");
            XPathNavigator nav = xPathDoc.CreateNavigator();
            XPathNodeIterator xItem = nav.SelectChildren("lists", "");
            xItem.MoveNext();
            xItem = xItem.Current.SelectChildren("list", "");
            xItem.MoveNext();
            xItem.MoveNext();
            xItem = xItem.Current.SelectChildren("items_count", "");
            xItem.MoveNext();
            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Loaded = 0; Count = int.Parse(xItem.Current.InnerXml); }));
            do
            {
                Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Status = "Retrieving items for #" + ListId + "..."; }));
                xPathDoc = CreateXPathDocumentFromUri("http://api.iknow.co.jp/lists/" + ListId + "/items.xml?page=" + page++ + "&per_page=50");
                nav = xPathDoc.CreateNavigator();
                xItem = nav.SelectChildren("vocabulary", "http://api.smart.fm/specifications/vocabulary/1.0");
                xItem.MoveNext();
                xItem = xItem.Current.SelectChildren("items", "http://api.smart.fm/specifications/vocabulary/1.0");
                xItem.MoveNext();
                xItem = xItem.Current.SelectChildren("item", "http://api.smart.fm/specifications/vocabulary/1.0");
                xItem.MoveNext();
                currentCount = xItem.Count;
                Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Status = "Parsing items for #" + ListId + "..."; }));
                do
                {
                    Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Current = new SmartElement(); }));

                    // Cue and Name
                    XPathNodeIterator xCue = xItem.Current.SelectChildren("cue", "http://api.smart.fm/specifications/vocabulary/1.0");
                    xCue.MoveNext();
                    XPathNodeIterator xName = xCue.Current.SelectChildren("text", "http://api.smart.fm/specifications/vocabulary/1.0");
                    if (xName.MoveNext())
                    {
                        Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() =>
                        {
                            Current = new SmartElement();
                            if (!string.IsNullOrEmpty(xName.Current.InnerXml))
                                Current.Text = xName.Current.InnerXml;
                        }));
                    }
                    XPathNodeIterator xCharacter = xCue.Current.SelectChildren("character", "http://api.smart.fm/specifications/vocabulary/1.0");
                    if (xCharacter.MoveNext())
                    {
                        Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() =>
                        {
                            if (!string.IsNullOrEmpty(xCharacter.Current.InnerXml))
                                Current.Character = xCharacter.Current.InnerXml;
                        }));
                    }
                    if (string.IsNullOrEmpty(Current.Character) && string.IsNullOrEmpty(Current.Text))
                        continue;
                    else if (string.IsNullOrEmpty(Current.Character))
                        Current.Character = Current.Text;
                    else if (string.IsNullOrEmpty(Current.Text))
                        Current.Text = Current.Character;

                    // Response
                    XPathNodeIterator xResponses = xItem.Current.SelectChildren("responses", "http://api.smart.fm/specifications/vocabulary/1.0");
                    if (xResponses.MoveNext())
                    {
                        XPathNodeIterator xResponse = xResponses.Current.SelectChildren("", "http://api.smart.fm/specifications/vocabulary/1.0");
                        if (xResponse.MoveNext())
                        {
                            XPathNodeIterator xText = xResponse.Current.SelectChildren("text", "http://api.smart.fm/specifications/vocabulary/1.0");
                            if (xText.MoveNext())
                                Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Current.Response = xText.Current.InnerXml; }));
                        }
                    }

                    // Sentences
                    XPathNodeIterator xSentences = xItem.Current.SelectChildren("sentences", "http://api.smart.fm/specifications/vocabulary/1.0");

                    while (xSentences.MoveNext())
                    {
                        XPathNodeIterator xSentence = xSentences.Current.SelectChildren("sentence", "http://api.smart.fm/specifications/vocabulary/1.0");
                        xSentence.MoveNext();
                        XPathNodeIterator xImage = xSentence.Current.SelectChildren("square_image", "http://api.smart.fm/specifications/vocabulary/1.0");
                        xImage.MoveNext();
                        if (!string.IsNullOrEmpty(xImage.Current.InnerXml))
                        {
                            byte[] imgBytes = FileManager.FileToByteArray(xImage.Current.InnerXml);
                            if (imgBytes != null)
                                Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Current.AddPicture(imgBytes); }));
                            break;
                        }
                        else
                        {
                            // Look for another sources for pictures (via the sentences search)
                            List<string> splittedWords = Current.Response.Replace(',', ' ').Split(' ').ToList<string>();
                            for (int c = splittedWords.Count - 1; c >= 0; c--)
                                if (string.IsNullOrEmpty(splittedWords[c]))
                                    splittedWords.RemoveAt(c);
                            string toSearch = "";
                            for (int c = 0; c < 3 && c < splittedWords.Count; c++)
                                toSearch += c > 0 ? "+" + splittedWords[c] : splittedWords[c];
                            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Status = "Searching pics for '" + Current.Response + "'..."; }));
                            byte[] imgBytes = SearchImageFor(toSearch);

                            // 2nd attempt
                            if (imgBytes == null)
                            {
                                toSearch = "";
                                for (int c = 0; c < 2 && c < splittedWords.Count; c++)
                                    toSearch += c > 0 ? "+" + splittedWords[c] : splittedWords[c];
                                imgBytes = SearchImageFor(toSearch);
                            }

                            // 3rd attempt
                            if (imgBytes == null)
                            {
                                toSearch = splittedWords[0];
                                imgBytes = SearchImageFor(toSearch);
                            }

                            // Use a dummy image
                            if (imgBytes == null)
                            {
                                MemoryStream ms = new MemoryStream();
                                Smart_fm.NoPhoto.Save(ms, ImageFormat.Jpeg);
                                imgBytes = ms.ToArray();
                            }

                            Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Current.AddPicture(imgBytes); Status = "Retrieving items for #" + ListId + "..."; }));
                        }
                    }
                    Dispatcher.Invoke(DispatcherPriority.DataBind, new Action(() => { Elements.Add(Current); }));
                    Loaded++;
                } while (xItem.MoveNext());
            }
            while (currentCount >= 50);
        }

        public void Write(string id)
        {
            Status = "Writing files for #" + ListId + "...";
            ContentManager Content = new ContentManager();
            Content.ColumnAdd(TextColumn, ContentType.Text);
            Content.ColumnAdd(ResponseColumn, ContentType.Text);
            if (HasPictures)
                Content.ColumnAdd("Photo", ContentType.Image);
            foreach (SmartElement element in Elements)
            {
                string text = (element.Character == element.Text) ? element.Text : element.Character + " (" + element.Text + ")";
                if (HasPictures)
                {
                    Byte[] imageByte = null;
                    if (element.Pictures.Count > 0)
                        imageByte = element.Pictures[0];
                    Content.ItemAdd(new object[] { text, element.Response, imageByte });
                }
                else
                    Content.ItemAdd(new object[] { text, element.Response });

            }
            JobDone.Invoke(Content);
        }
    }
}
