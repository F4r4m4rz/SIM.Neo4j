using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SIM.Neo4j
{
    public class SimPropertyDictionary : IDictionary<string, object>
    {
        public object this[string key]
        {
            get
            {
                return Values.ElementAt(Keys.ToList().IndexOf(key));
            }

            set
            {
                Values.ToArray()[Keys.ToList().IndexOf(key)] = value;
            }
        }

        public ICollection<string> Keys { get; }

        public ICollection<object> Values { get; }

        public int Count => Keys.Count;

        public bool IsReadOnly => false;

        public void Add(string key, object value)
        {
            Keys.Add(key);
            Values.Add(value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            Keys.Add(item.Key);
            Values.Add(item.Value);
        }

        public void Clear()
        {
            Keys.Clear();
            Values.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return Keys.Contains(item.Key) && Values.Contains(item.Value);
        }

        public bool ContainsKey(string key)
        {
            return Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            Values.Remove(this[key]);
            Keys.Remove(key);
            return true;
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            Values.Remove(this[item.Key]);
            Keys.Remove(item.Key);
            return true;
        }

        public bool TryGetValue(string key, out object value)
        {
            value = null;
            try
            {
                value = this[key];
                return true;
            }
            catch
            {
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
