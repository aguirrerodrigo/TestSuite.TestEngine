using System;
using System.Collections;
using System.Collections.Generic;

namespace TestSuite.TestEngine
{
    [Serializable]
    public class ParameterCollection : IEnumerable<KeyValuePair<string, object>>
    {
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public int Count
        {
            get { return this.dictionary.Count; }
        }

        public object this[string parameter]
        {
            get
            {
                return this.dictionary[parameter];
            }
            set
            {
                if (this.dictionary.ContainsKey(parameter))
                    this.dictionary[parameter] = value;
                else
                    this.dictionary.Add(parameter, value);
            }
        }

        internal ParameterCollection() { }

        public bool ContainsKey(string key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }
    }
}