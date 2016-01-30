using System;
using System.Collections;
using System.Collections.Generic;

namespace Meow.FR.Rincevent.Core.Data
{
    public class Content : ICollection<ContentElement>
    {
        /// <summary>
        /// Creates a content object (the first object in 'row' is the index).
        /// </summary>
        /// <param name="row">Row that contains the data (the first element must be the Index, the second element must be the CheckBox - but this last is not used here).</param>
        /// <param name="types">Types for each element.</param>
        /// <param name="names">Names for each column.</param>
        /// <param name="settings">Settings for each column.</param>
        public Content(Object[] row, ContentType[] types, String[] names, IList<BaseSettings> settings)
        {
            _index = (int)row[0];
            _elements = new List<ContentElement>(row.Length - 2);
            for (int c = 0; c + 2 < row.Length; c++)
                if (settings[c].Activated)
                    _elements.Add(new ContentElement(row[c + 2], types[c], names[c], settings[c]));
            _elements.Sort();
        }

        private readonly int _index;
        public int Index
        {
            get { return _index; }
        }

        private List<ContentElement> _elements;
        public List<ContentElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public ContentElement GetElementByColumn(String name)
        {
            foreach (ContentElement current in _elements)
                if (current.Name == name)
                    return current;
            return null;
        }

        #region ICollection<ContentElement> Members

        public void Add(ContentElement item)
        {
            _elements.Add(item);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(ContentElement item)
        {
            return _elements.Contains(item);
        }

        public void CopyTo(ContentElement[] array, int arrayIndex)
        {
            _elements.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ContentElement item)
        {
            return _elements.Remove(item);
        }

        #endregion

        #region IEnumerable<ContentElement> Members

        public IEnumerator<ContentElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        #endregion
    }
}
