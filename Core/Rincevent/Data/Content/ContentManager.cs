using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace Meow.FR.Rincevent.Core.Data
{
    [Serializable]
    public partial class ContentManager : ISerializable
    {
        #region Private Members
        private List<BaseSettings> _settingsList;              // Contains a list of each column settings
        private DataTable _tableContent;                       // Contains the values (e.g. all the data inserted by users)
        private DataView _viewContent;                         // DataView of the values
        private DataRow[] _checkedRows = new DataRow[] { };             // List of the checked rows
        private DataRow[] _unCheckedRows = new DataRow[] { };           // List of the checked rows
        private Random _rand = new Random();                            // Random system
        private readonly Queue<int> _randomQueue = new Queue<int>();    // Already used values, to avoid the random getters to return the same result
        private bool _hasBeenModified = false;                          // Should be set to true when content is modified
        private string _filePath;                                       // The name of the file if this content was already saved
        private RandomMode _randomMode = RandomMode.PlayAll;            // Mode used in order to get random content
        private event EventHandler UpdateEvent;                         // Fired when an element is added, removed, checked, or unchecked
        private readonly Dictionary<string, List<int>> _playlists;      // Playlists
        private Nullable<Guid> _fileId;                                 // Unique identifier
        private string _author;                                         // Original author
        private Nullable<DateTime> _creationDate;                       // Date of creation
        private Nullable<DateTime> _modificationDate;                   // Last modification
        #endregion

        #region Properties useful for XML serialization
        public DataTable TableContent
        {
            get { return _tableContent; }
            set
            {
                _tableContent = value;
                _viewContent = new DataView(_tableContent);
                _viewContent.Sort = Sort;
            }
        }
        #endregion

        #region Constructor & De/Serialization
        public ContentManager()
        {
            _settingsList = new List<BaseSettings>(2);
            _tableContent = new DataTable("values");
            // ID column
            DataColumn newCol = new DataColumn("Id", typeof(uint));
            newCol.AutoIncrement = true;
            newCol.AutoIncrementSeed = 0;
            newCol.Unique = true;
            _tableContent.Columns.Add(newCol);
            _tableContent.PrimaryKey = new DataColumn[] { newCol };
            // Checkable column
            newCol = new DataColumn("IsChecked", typeof(bool));
            _tableContent.Columns.Add(newCol);
            _viewContent = new DataView(_tableContent);
            _playlists = new Dictionary<string, List<int>>();
            InitContentManager();
        }

        protected ContentManager(SerializationInfo info, StreamingContext ctxt)
        {
            SerializationInfoEnumerator infoEnum = info.GetEnumerator();
            while (infoEnum.MoveNext())
            {
                switch (infoEnum.Name)
                {
                    case "settingsList":
                        _settingsList = (List<BaseSettings>)info.GetValue("settingsList", typeof(List<BaseSettings>));
                        break;

                    case "tableContent":
                        _tableContent = (DataTable)info.GetValue("tableContent", typeof(DataTable));
                        _viewContent = new DataView(_tableContent);
                        _viewContent.Sort = info.GetString("sort");
                        break;

                    case "playlists":
                        _playlists = (Dictionary<string, List<int>>)info.GetValue("playlists", typeof(Dictionary<string, List<int>>));
                        break;

                    case "fileId":
                        _fileId = (Guid)info.GetValue("fileId", typeof(Guid));
                        break;

                    case "author":
                        _author = info.GetString("author");
                        break;

                    case "creationDate":
                        _creationDate = info.GetDateTime("creationDate");
                        break;

                    case "modificationDate":
                        _modificationDate = info.GetDateTime("creationDate");
                        break;
                }
            }
            if (_playlists == null)
                _playlists = new Dictionary<string, List<int>>();
            InitContentManager();
        }

        private void InitContentManager()
        {
            UpdateEvent += ContentManager_UpdateEvent;
            UpdateEvent.Invoke(null, null);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("settingsList", _settingsList);
            info.AddValue("tableContent", _tableContent);
            info.AddValue("sort", _viewContent.Sort);
            info.AddValue("playlists", _playlists);
            if (!_fileId.HasValue)
                _fileId = Guid.NewGuid();
            info.AddValue("fileId", _fileId);
            if (_author == null)
                _author = Environment.UserName;
            info.AddValue("author", _author);
            if (!_creationDate.HasValue)
                _creationDate = DateTime.Now;
            info.AddValue("creationDate", _creationDate);
            _modificationDate = DateTime.Now;
            info.AddValue("modificationDate", _modificationDate);
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets or sets the RandomMode.
        /// </summary>
        public RandomMode RandomMode
        {
            get { return _randomMode; }
            set
            {
                if (_randomMode != value)
                {
                    _randomQueue.Clear();
                    _randomMode = value;
                }
            }
        }

        public Nullable<Guid> FileID { get { return _fileId; } }

        /// <summary>
        /// Gets all the items ID's.
        /// </summary>
        /// <returns>The list.</returns>
        public List<int> GetAllItemsIndexes()
        {
            List<int> idList = new List<int>(_checkedRows.Length + _unCheckedRows.Length);
            foreach (DataRow current in _checkedRows)
                idList.Add((int)current[0]);
            foreach (DataRow current in _unCheckedRows)
                idList.Add((int)current[0]);
            return idList;
        }

        /// <summary>
        /// Gets the checked items ID's.
        /// </summary>
        /// <returns>The list.</returns>
        public List<int> GetCheckedItemsIndexes()
        {
            List<int> idList = new List<int>(_checkedRows.Length);
            foreach (DataRow current in _checkedRows)
                idList.Add((int)current[0]);
            return idList;
        }

        /// <summary>
        /// Gets the unchecked items ID's.
        /// </summary>
        /// <returns>The list.</returns>
        public List<int> GetUncheckedItemsIndexes()
        {
            List<int> idList = new List<int>(_unCheckedRows.Length);
            foreach (DataRow current in _unCheckedRows)
                idList.Add((int)current[0]);
            return idList;
        }

        public bool IsItemChecked(int index)
        {
            return (bool)GetRow(index)[1];
        }

        public List<BaseSettings> Settings
        {
            get { return _settingsList; }
        }

        public String[] ColumnNames
        {
            get
            {
                String[] cn = new String[_settingsList.Count];
                for (int c = 0; c < _settingsList.Count; c++)
                    cn[c] = _settingsList[c].Name;
                return cn;
            }
        }

        public ContentType[] ColumnTypes
        {
            get
            {
                ContentType[] cn = new ContentType[_settingsList.Count];
                for (int c = 0; c < _settingsList.Count; c++)
                    cn[c] = _settingsList[c].ContentType;
                return cn;
            }
        }

        public object DataSource
        {
            get { return _viewContent; }
        }

        public bool HasBeenModified
        {
            get { return _hasBeenModified; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string FileName
        {
            get
            {
                if (_filePath == null)
                    return null;
                FileInfo info = new FileInfo(_filePath);
                return info.Name.Replace(info.Extension, "");
            }
        }

        /// <summary>
        /// Sort by the specified column name.
        /// </summary>
        public string Sort
        {
            get { return _viewContent.Sort; }
            set { _viewContent.Sort = value; }
        }

        #endregion

        #region Columns
        /// <summary>
        /// Creates a new column.
        /// </summary>
        /// <param name="title">Name of the column.</param>
        /// <param name="type">Type of data that will contain the column.</param>     
        public void ColumnAdd(string title, ContentType type)
        {
            DataColumn newCol;
            BaseSettings newSettings;

            switch (type)
            {
                case ContentType.Text:
                    newCol = new DataColumn(title, typeof(string));
                    newSettings = new TextSettings(title, type);
                    break;

                case ContentType.Image:
                    newCol = new DataColumn(title, typeof(byte[]));
                    newSettings = new ImageSettings(title, type); ;
                    break;

                default:
                    throw new NotImplementedException();
            }
            _tableContent.Columns.Add(newCol);
            newSettings.Order = (ushort)_settingsList.Count;
            _settingsList.Add(newSettings);
        }

        /// <summary>
        /// Rename a column. Don't forget to update the GUI.
        /// </summary>
        /// <param name="oldTitle">Precedent title.</param>
        /// <param name="newTitle">New title.</param>
        /// <returns>True is the operation succeeded.</returns>
        public bool ColumnRename(string oldTitle, string newTitle)
        {
            if (oldTitle == newTitle)
                return true;
            for (int c = 0; c < ColumnNames.Length; c++)
                if (ColumnNames[c] == newTitle)
                    return false;
            for (int c = 0; c < ColumnNames.Length; c++)
                if (ColumnNames[c] == oldTitle)
                {
                    ColumnNames[c] = newTitle;
                    _tableContent.Columns[c + 2].ColumnName = newTitle;
                    break;
                }
            if (Sort == oldTitle)
                Sort = newTitle;
            for (int c = 0; c < Settings.Count; c++)
                if (Settings[c].Name == oldTitle)
                {
                    Settings[c].Name = newTitle;
                    break;
                }
            _hasBeenModified = true;
            return true;
        }
        #endregion

        #region Check / Uncheck
        public void SetAllItemsChecked(bool value)
        {
            _tableContent.BeginLoadData();
            if (value)
                foreach (DataRow current in _unCheckedRows)
                {
                    current.BeginEdit();
                    current[1] = true;
                }
            else
                foreach (DataRow current in _checkedRows)
                {
                    current.BeginEdit();
                    current[1] = false;
                }
            _tableContent.AcceptChanges();
            _tableContent.EndLoadData();
            UpdateEvent.Invoke(null, null);
            _hasBeenModified = true;
        }

        /// <summary>
        /// Set an item as checked - or not.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <param name="value">True if checked.</param>
        public void SetItemChecked(int index, bool value)
        {
            SetItemChecked(index, value, true);
        }

        /// <summary>
        /// Set an item as checked - or not.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <param name="value">True if checked.</param>
        /// <param name="updateEvent">Must be true is it's the last call to this method in the stack.</param>
        private void SetItemChecked(int index, bool value, bool updateEvent)
        {
            DataRow row = GetRow(index);
            if (row != null)
            {
                row[1] = value;
                if (updateEvent)
                {
                    UpdateEvent.Invoke(null, null);
                    _hasBeenModified = true;
                }
            }
        }

        #endregion

        #region Elements
        private DataRow GetRow(int dataIndex)
        {
            DataRow row = _tableContent.Rows.Find(new object[] { dataIndex });
            if (row == null)
                throw new ArgumentOutOfRangeException();
            return row;
        }

        public List<Content> GetAllWantedItems(int max)
        {
            DataRow[] rows;
            if (_randomMode == RandomMode.PlayAll)
                rows = _tableContent.Select();
            else if (_randomMode == RandomMode.PlayChecked)
                rows = _checkedRows;
            else if (_randomMode == RandomMode.PlayNotChecked)
                rows = _unCheckedRows;
            else
                throw new NotImplementedException();

            if (max == -1)
                max = rows.Length;
            else if (max > rows.Length)
                max = rows.Length;

            List<int> alreadyUsed = new List<int>(max);
            List<Content> content = new List<Content>(max);
            for (int c = 0; c < max; c++)
            {
                int index;
                do
                {
                    index = _rand.Next(0, rows.Length);
                } while (alreadyUsed.Contains(index));
                content.Add(new Content(rows[index].ItemArray, ColumnTypes, ColumnNames, Settings));
                alreadyUsed.Add(index);
            }
            return content;
        }

        public List<Content> GetAllWantedItems()
        {
            return GetAllWantedItems(-1);
        }

        /// <summary>
        /// Gets a random content.
        /// </summary>
        /// <returns>A random content.</returns>
        public Content GetRandomWantedItem()
        {
            if (_randomMode == RandomMode.PlayAll)
                return (CleverRandomizeFromRows(_tableContent.Select()));
            else if (_randomMode == RandomMode.PlayChecked)
                return (CleverRandomizeFromRows(_checkedRows));
            else if (_randomMode == RandomMode.PlayNotChecked)
                return (CleverRandomizeFromRows(_unCheckedRows));
            else
                throw new NotImplementedException();
        }

        private Content CleverRandomizeFromRows(DataRow[] rows)
        {
            if (rows.Length == 0)
                return null;
            object[] obj;

            int maxSize = Convert.ToInt32(Math.Round(rows.Length * 0.75, 0));
            while (_randomQueue.Count >= maxSize)
                _randomQueue.Dequeue();
            _rand = new Random();
            do
            {
                obj = rows[_rand.Next(0, rows.Length)].ItemArray;
            } while (_randomQueue.Contains((int)obj[0]));
            _randomQueue.Enqueue((int)obj[0]);
            return (new Content(obj, ColumnTypes, ColumnNames, Settings));
        }

        public int ItemCountAll()
        {
            return _tableContent.Rows.Count;
        }

        public int ItemCountChecked()
        {
            return _checkedRows.Length;
        }

        public int ItemCountNotChecked()
        {
            return _unCheckedRows.Length;
        }

        public int ItemAdd(object[] data)
        {
            if (_settingsList.Count != data.Length)
                throw new ArgumentOutOfRangeException();
            object[] newData = new object[data.Length + 2];
            newData[0] = null;
            newData[1] = false;
            data.CopyTo(newData, 2);
            _tableContent.Rows.Add(newData);
            UpdateEvent.Invoke(null, null);
            int lastId = (int)_tableContent.Compute("Max(Id)", String.Empty);
            _tableContent.Columns["Id"].AutoIncrementSeed = lastId + 1;
            _hasBeenModified = true;
            return lastId;
        }

        public void ItemUpdate(int index, string columnName, object value)
        {
            DataRow row = GetRow(index);
            row[columnName] = value;
            row.EndEdit();
            _hasBeenModified = true;
        }

        public void ItemRemove(int index)
        {
            if (index >= 0)
            {
                string[] keys = PlaylistGetAll();
                foreach (string key in keys)
                    PlaylistRemoveIndex(key, index);
                GetRow(index).Delete();
                UpdateEvent.Invoke(null, null);
                _hasBeenModified = true;
            }
        }

        public Content ItemGet(int index)
        {
            return (new Content(GetRow(index).ItemArray, ColumnTypes, ColumnNames, Settings));
        }
        #endregion

        #region Playlists

        /// <summary>
        /// Gets the names of all the playlists.
        /// </summary>
        /// <returns>The list of playlists.</returns>
        public string[] PlaylistGetAll()
        {
            Dictionary<string, List<int>>.KeyCollection keyCollection = _playlists.Keys;
            string[] array = new string[keyCollection.Count];
            keyCollection.CopyTo(array, 0);
            return array;
        }

        public bool PlaylistExists(string playlistName)
        {
            return _playlists.ContainsKey(playlistName);
        }

        /// <summary>
        /// Gets the items of a specific playlist.
        /// </summary>
        /// <param name="playlistName">The playlist name.</param>
        /// <returns>List of items.</returns>
        public List<int> PlaylistGet(string playlistName)
        {
            return _playlists[playlistName];
        }

        /// <summary>
        /// Creates a new playlist from the given name.
        /// </summary>
        /// <param name="playlistName">Name of the new playlist.</param>
        public void PlaylistCreate(string playlistName)
        {
            if (!_playlists.ContainsKey(playlistName))
                _playlists.Add(playlistName, new List<int>());
        }

        /// <summary>
        /// Removes the specific playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        public void PlaylistRemove(string playlistName)
        {
            _playlists.Remove(playlistName);
        }

        public void PlaylistAddIndex(string playlistName, int indexToAdd)
        {
            _playlists[playlistName].Add(indexToAdd);
        }

        public void PlaylistRemoveIndex(string playlistName, int indexToRemove)
        {
            _playlists[playlistName].Remove(indexToRemove);
        }

        public void PlaylistAddCheckedItems(string playlistName)
        {
            foreach (DataRow row in _checkedRows)
                _playlists[playlistName].Add((int)row.ItemArray[0]);
        }

        public void PlaylistRemoveCheckedItems(string playlistName)
        {
            foreach (DataRow row in _checkedRows)
                _playlists[playlistName].Remove((int)row.ItemArray[0]);
        }

        public void PlaylistCheck(string playlistName)
        {
            List<int> lst = PlaylistGet(playlistName);
            for (int c = 0; c < lst.Count; c++)
            {
                if (c == lst.Count - 1)
                    SetItemChecked(lst[c], true, true);
                else
                    SetItemChecked(lst[c], true, false);
            }
        }

        public void PlaylistRename(string playlistName, string newPlaylistName)
        {
            PlaylistCreate(newPlaylistName);
            _playlists[newPlaylistName].AddRange(PlaylistGet(playlistName));
            PlaylistRemove(playlistName);
        }

        public void PlaylistUncheck(string playlistName)
        {
            List<int> lst = PlaylistGet(playlistName);
            for (int c = 0; c < lst.Count; c++)
            {
                if (c == lst.Count - 1)
                    SetItemChecked(lst[c], false, true);
                else
                    SetItemChecked(lst[c], false, false);
            }
        }

        #endregion

        #region Events Handlers
        void ContentManager_UpdateEvent(object sender, EventArgs e)
        {
            _checkedRows = _tableContent.Select("IsChecked='True'");
            _unCheckedRows = _tableContent.Select("IsChecked='False'");
            _randomQueue.Clear();
        }
        #endregion

        #region Load & Save
        static public ContentManager Load(string fileName)
        {
            ContentManager contentManager = FileManager.LoadDataFromFile<ContentManager>(fileName);
            contentManager.FilePath = fileName;
            return contentManager;
        }

        public void Save()
        {
            FileManager.SaveDataToFile(_filePath, this);
            _hasBeenModified = false;
        }

        public void SaveAs(string fileName)
        {
            _filePath = fileName;
            FileManager.SaveDataToFile(fileName, this);
            _hasBeenModified = false;
        }
        #endregion
    }
}
