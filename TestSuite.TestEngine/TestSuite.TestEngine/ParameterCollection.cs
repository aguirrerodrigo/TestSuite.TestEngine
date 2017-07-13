using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestSuite.TestEngine
{
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
                return dictionary[parameter];
            }
            set
            {
                if (dictionary.ContainsKey(parameter))
                    dictionary[parameter] = value;
                else
                    dictionary.Add(parameter, value);
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