using Lucene.Net.Store;
using System;
using System.IO;

namespace Lucene.Net.Support.IO
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
    internal sealed class BinaryReaderDataInput : DataInput, IDisposable
    {
        private readonly BinaryReader br;
        public BinaryReaderDataInput(BinaryReader br)
        {
            this.br = br;
        }
       
        public override byte ReadByte()
        {
            return br.ReadByte();
        }

        public override void ReadBytes(byte[] b, int offset, int len)
        {
            byte[] temp = br.ReadBytes(len);
            for (int i = offset; i < (offset + len) && i < temp.Length; i++)
            {
                b[i] = temp[i];
            }
        }

        public void Dispose()
        {
            br.Dispose();
        }
    }
}
