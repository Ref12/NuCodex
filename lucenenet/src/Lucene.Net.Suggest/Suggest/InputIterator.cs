﻿using Lucene.Net.Util;
using System.Collections.Generic;

namespace Lucene.Net.Search.Suggest
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
    /// Interface for enumerating term,weight,payload triples for suggester consumption;
    /// currently only <see cref="Analyzing.AnalyzingSuggester"/>, <see cref="Analyzing.FuzzySuggester"/>
    /// and <see cref="Analyzing.AnalyzingInfixSuggester"/> support payloads.
    /// </summary>
    public interface IInputIterator : IBytesRefIterator
    {

        /// <summary>
        /// A term's weight, higher numbers mean better suggestions. </summary>
        long Weight { get; }

        /// <summary>
        /// An arbitrary byte[] to record per suggestion.  See
        /// <see cref="Lookup.LookupResult.Payload"/> to retrieve the payload
        /// for each suggestion. 
        /// </summary>
        BytesRef Payload { get; }

        /// <summary>
        /// Returns true if the iterator has payloads </summary>
        bool HasPayloads { get; }

        /// <summary>
        /// A term's contexts context can be used to filter suggestions.
        /// May return null, if suggest entries do not have any context
        /// </summary>
        ICollection<BytesRef> Contexts { get; }

        /// <summary>
        /// Returns true if the iterator has contexts </summary>
        bool HasContexts { get; }
    }

    /// <summary>
    /// Singleton <see cref="IInputIterator"/> that iterates over 0 BytesRefs.
    /// </summary>
    public static class EmptyInputIterator
    {
        public static readonly IInputIterator Instance = new InputIteratorWrapper(BytesRefIterator.EMPTY);
    }

    /// <summary>
    /// Wraps a <see cref="IBytesRefIterator"/> as a suggester <see cref="IInputIterator"/>, with all weights
    /// set to <c>1</c> and carries no payload
    /// </summary>
    public class InputIteratorWrapper : IInputIterator
    {
        internal readonly IBytesRefIterator wrapped;

        /// <summary>
        /// Creates a new wrapper, wrapping the specified iterator and 
        /// specifying a weight value of <c>1</c> for all terms 
        /// and nullifies associated payloads.
        /// </summary>
        public InputIteratorWrapper(IBytesRefIterator wrapped)
        {
            this.wrapped = wrapped;
        }

        public virtual long Weight => 1;

        public virtual BytesRef Next()
            => wrapped.Next();

        public virtual BytesRef Payload => null;

        public virtual bool HasPayloads => false;

        public virtual IComparer<BytesRef> Comparer => wrapped.Comparer;

        public virtual ICollection<BytesRef> Contexts => null;

        public virtual bool HasContexts => false;
    }
}