using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    namespace Resources
    {
        [Serializable]
        public class ResourceDictionary<TKey, TValue> where TValue : struct, IComparable, IConvertible, IComparable<int>, IEquatable<int>
        {
            [SerializeField] private TKey[] key;
            [SerializeField] private int[] amount;

            public Dictionary<TKey, int> ToDictionary()
            {
                if(key.Length != amount.Length) 
                    throw new ArgumentException("'Key' and 'Amount' count not equal");

                Dictionary<TKey, int> dictionary = new Dictionary<TKey, int>();

                for (int i = 0; i < key.Length; i++) 
                {
                    dictionary[key[i]] = amount[i];
                }
                return dictionary;
            }
        }
    }
}