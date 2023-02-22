using Lucene.Net.Support;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Lucene.Net.Util
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    /// <summary>
    /// A native <see cref="int"/> hash-based set where one value is reserved to mean "EMPTY" internally. The space overhead is fairly low
    /// as there is only one power-of-two sized <see cref="T:int[]"/> to hold the values.  The set is re-hashed when adding a value that
    /// would make it >= 75% full.  Consider extending and over-riding <see cref="Hash(int)"/> if the values might be poor
    /// hash keys; Lucene docids should be fine.
    /// The internal fields are exposed publicly to enable more efficient use at the expense of better O-O principles.
    /// <para/>
    /// To iterate over the integers held in this set, simply use code like this:
    /// <code>
    /// SentinelIntSet set = ...
    /// foreach (int v in set.keys) 
    /// {
    ///     if (v == set.EmptyVal)
    ///         continue;
    ///     //use v...
    /// }
    /// </code>
    /// <para/>
    /// NOTE: This was SentinelIntSet in Lucene
    /// <para/>
    /// @lucene.internal
    /// </summary>
    public class SentinelInt32Set
    {
        /// <summary>
        /// A power-of-2 over-sized array holding the integers in the set along with empty values. </summary>
        [WritableArray]
        [SuppressMessage("Microsoft.Performance", "CA1819", Justification = "Lucene's design requires some writable array properties")]
        public int[] Keys
        {
            get => keys;
            set => keys = value;
        }
        private int[] keys;

        /// <summary>
        /// The number of integers in this set. </summary>
        public int Count { get; private set; } // LUCENENET NOTE: made setter internal to encapsulate, and using to replace size() in Lucene
        public int EmptyVal { get; private set; }

        /// <summary>
        /// The count at which a rehash should be done. </summary>
        public int RehashCount { get; set; }

        ///
        /// <param name="size">  The minimum number of elements this set should be able to hold without rehashing
        ///              (i.e. the slots are guaranteed not to change). </param>
        /// <param name="emptyVal"> The integer value to use for EMPTY. </param>
        public SentinelInt32Set(int size, int emptyVal)
        {
            this.EmptyVal = emptyVal;
            int tsize = Math.Max(Lucene.Net.Util.BitUtil.NextHighestPowerOfTwo(size), 1);
            RehashCount = tsize - (tsize >> 2);
            if (size >= RehashCount) // should be able to hold "size" w/o re-hashing
            {
                tsize <<= 1;
                RehashCount = tsize - (tsize >> 2);
            }
            keys = new int[tsize];
            if (emptyVal != 0)
            {
                Clear();
            }
        }

        public virtual void Clear()
        {
            Arrays.Fill(keys, EmptyVal);
            Count = 0;
        }

        /// <summary>
        /// (internal) Return the hash for the key. The default implementation just returns the key,
        /// which is not appropriate for general purpose use.
        /// </summary>
        public virtual int Hash(int key)
        {
            return key;
        }

        // LUCENENET specific - replacing with Count property (above)
        ///// <summary>
        ///// The number of integers in this set. </summary>
        //public virtual int Size
        //{
        //    get { return Count; }
        //}

        /// <summary>
        /// (internal) Returns the slot for this key. </summary>
        public virtual int GetSlot(int key)
        {
            Debug.Assert(key != EmptyVal);
            int h = Hash(key);
            int s = h & (keys.Length - 1);
            if (keys[s] == key || keys[s] == EmptyVal)
            {
                return s;
            }

            int increment = (h >> 7) | 1;
            do
            {
                s = (s + increment) & (keys.Length - 1);
            } while (keys[s] != key && keys[s] != EmptyVal);
            return s;
        }

        /// <summary>
        /// (internal) Returns the slot for this key, or -slot-1 if not found. </summary>
        public virtual int Find(int key)
        {
            Debug.Assert(key != EmptyVal);
            int h = Hash(key);
            int s = h & (keys.Length - 1);
            if (keys[s] == key)
            {
                return s;
            }
            if (keys[s] == EmptyVal)
            {
                return -s - 1;
            }

            int increment = (h >> 7) | 1;
            for (; ; )
            {
                s = (s + increment) & (keys.Length - 1);
                if (keys[s] == key)
                {
                    return s;
                }
                if (keys[s] == EmptyVal)
                {
                    return -s - 1;
                }
            }
        }

        /// <summary>
        /// Does this set contain the specified integer? </summary>
        public virtual bool Exists(int key)
        {
            return Find(key) >= 0;
        }

        /// <summary>
        /// Puts this integer (key) in the set, and returns the slot index it was added to.
        /// It rehashes if adding it would make the set more than 75% full.
        /// </summary>
        public virtual int Put(int key)
        {
            int s = Find(key);
            if (s < 0)
            {
                Count++;
                if (Count >= RehashCount)
                {
                    Rehash();
                    s = GetSlot(key);
                }
                else
                {
                    s = -s - 1;
                }
                keys[s] = key;
            }
            return s;
        }

        /// <summary>
        /// (internal) Rehashes by doubling key (<see cref="T:int[]"/>) and filling with the old values. </summary>
        public virtual void Rehash()
        {
            int newSize = keys.Length << 1;
            int[] oldKeys = keys;
            keys = new int[newSize];
            if (EmptyVal != 0)
            {
                Arrays.Fill(keys, EmptyVal);
            }

            foreach (int key in oldKeys)
            {
                if (key == EmptyVal)
                {
                    continue;
                }
                int newSlot = GetSlot(key);
                keys[newSlot] = key;
            }
            RehashCount = newSize - (newSize >> 2);
        }
    }
}