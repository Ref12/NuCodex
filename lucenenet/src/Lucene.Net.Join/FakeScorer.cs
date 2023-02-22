﻿using Lucene.Net.Search;
using System;
using System.Collections.Generic;

namespace Lucene.Net.Join
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
    /// Passed to <see cref="ICollector.SetScorer(Scorer)"/> during join collection.
    /// </summary>
    internal sealed class FakeScorer : Scorer
    {
        internal float _score;
        internal int doc = -1;

        public FakeScorer() 
            : base(null)
        {
        }

        public override int Advance(int target)
        {
            throw new NotSupportedException("FakeScorer doesn't support Advance(int)");
        }

        public override int DocID => doc;

        public override int Freq => throw new NotSupportedException("FakeScorer doesn't support Freq");

        public override int NextDoc()
        {
            throw new NotSupportedException("FakeScorer doesn't support NextDoc()");
        }

        public override float GetScore()
        {
            return _score;
        }

        public override long GetCost()
        {
            return 1;
        }

        public override Weight Weight => throw new NotSupportedException();

        public override ICollection<ChildScorer> GetChildren()
        {
            throw new NotSupportedException();
        }
    }
}