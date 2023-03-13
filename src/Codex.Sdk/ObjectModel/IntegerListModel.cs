using Codex.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codex.ObjectModel.Implementation
{
    public class IntegerListModel : IReadOnlyList<int>
    {
        public int ValueByteWidth { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int MinValue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int DecompressedLength { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int CompressedLength { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public byte[] Data { get; set; }

        [JsonPropertyNameAttribute("cdata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string CompressedData { get; set; }

        [JsonIgnore]
        public int Count
        {
            get
            {
                var length = Data == null ? DecompressedLength : Data.Length;
                return length / ValueByteWidth;
            }
        }

        [JsonIgnore]
        public int DataLength
        {
            get
            {
                var length = Data == null ? DecompressedLength : Data.Length;
                return length;
            }
        }

        public IntegerListModel()
        {
        }

        public static IntegerListModel Create<T>(IReadOnlyList<T> values, Func<T, int> selector)
        {
            var minValue = values.Min(selector);
            var maxValue = values.Max(v => selector(v) - minValue);
            var byteWidth = NumberUtils.GetByteWidth(maxValue);
            var list = new IntegerListModel(byteWidth, values.Count, minValue);

            for (int i = 0; i < values.Count; i++)
            {
                list[i] = selector(values[i]);
            }

            return list;
        }

        public IntegerListModel(BitArray bitArray)
        {
            var byteArrayLength = (bitArray.Count + 7) / 8;
            Data = new byte[byteArrayLength];
            bitArray.CopyTo(Data, 0);
            ValueByteWidth = 1;
        }

        public IntegerListModel(int byteWidth, int numberOfValues, int minValue)
        {
            Data = new byte[byteWidth * numberOfValues];
            ValueByteWidth = byteWidth;
            MinValue = minValue;
        }

        [JsonIgnore]
        public int this[int index]
        {
            get
            {
                var value = GetIndexDirect(index);
                return value + MinValue;
            }

            set
            {
                value -= MinValue;
                SetIndexDirect(index, value);
            }
        }

        internal int GetIndexDirect(int index)
        {
            int value = 0;
            int dataIndex = index * ValueByteWidth;
            int byteOffset = 0;
            for (int i = 0; i < ValueByteWidth; i++, byteOffset += 8)
            {
                value |= (Data[dataIndex++] << byteOffset);
            }

            return value;
        }

        internal void SetIndexDirect(int index, int value)
        {
            int dataIndex = index * ValueByteWidth;
            for (int i = 0; i < ValueByteWidth; i++)
            {
                Data[dataIndex++] = unchecked((byte)value);
                value >>= 8;
            }
        }

        internal void Optimize(OptimizationContext context)
        {
            if (CompressedData == null && Data != null)
            {
                var compressedData = context.Compress(Data);
                if (compressedData.Length < Data.Length)
                {
                    DecompressedLength = Data.Length;
                    CompressedLength = compressedData.Length;
                    CompressedData = Convert.ToBase64String(compressedData);
                }
                else
                {
                    CompressedData = Convert.ToBase64String(Data);
                }

                Data = null;
            }
        }

        internal void ExpandData(OptimizationContext context)
        {
            if (CompressedData == null)
            {
                return;
            }

            Data = Convert.FromBase64String(CompressedData);
            if (DecompressedLength != 0)
            {
                var compressedData = Data;
                Data = new byte[DecompressedLength];
                using (var compressedStream = new DeflateStream(new MemoryStream(compressedData), CompressionMode.Decompress))
                {
                    compressedStream.Read(Data, 0, DecompressedLength);
                }
            }

            CompressedData = null;
            DecompressedLength = 0;
            CompressedLength = 0;
        }

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
