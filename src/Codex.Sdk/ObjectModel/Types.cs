using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codex.ObjectModel;
using Codex.ObjectModel.Attributes;

using static Codex.Utilities.SerializationUtilities;

namespace Codex.ObjectModel.Implementation
{
    /// <summary>
    ///  Allows defining extension data during analysis
    /// </summary>
    public class ExtensionData
    {
    }

    public partial class CodeSymbol
    {
        /// <summary>
        /// Extension data used during analysis/search
        /// TODO: Why is this needed?
        /// </summary>
        public ExtensionData ExtData { get; set; }

        protected bool Equals(CodeSymbol other)
        {
            return string.Equals(ProjectId, other.ProjectId, StringComparison.Ordinal) && string.Equals(Id.Value, other.Id.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CodeSymbol)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ProjectId?.GetHashCode() ?? 0) * 397) ^ (Id.Value?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return Id.Value;
        }
    }

    partial class ReferenceSymbol
    {
        public override string ToString()
        {
            return ReferenceKind + " " + base.ToString();
        }
    }

    [ExcludedSerializationProperty(nameof(ReferenceKind))]
    partial class DefinitionSymbol
    {
        protected override void Initialize()
        {
            ReferenceKind = nameof(ObjectModel.ReferenceKind.Definition);
            base.Initialize();
        }

        private string CoerceAbbreviatedName(string value)
        {
            if (value == null && ShortName != null)
            {
                int abbreviationLength = GetAbbreviationLength();
                if (abbreviationLength >= 3)
                {
                    AbbreviatedName = value = AccumulateAbbreviationCharacters(
                        new StringBuilder(abbreviationLength),
                        (sb, c) => sb.Append(c)).ToString();
                }
            }

            return value;
        }

        public void IncrementReferenceCount()
        {
            Placeholder.Todo("Increment reference count");
        }

        private int GetAbbreviationLength()
        {
            return AccumulateAbbreviationCharacters(0, (i, c) => i + 1);
        }

        /// <summary>
        /// Essentially extracts all upper-case characters not preceded by another uppercase character
        /// </summary>
        internal T AccumulateAbbreviationCharacters<T>(T initial, Func<T, char, T> accumulator)
        {
            if (ShortName.Length < 3)
            {
                return initial;
            }

            var value = initial;
            (bool isUpper, bool isAccumulated) state = default;
            for (int i = 0; i < ShortName.Length; i++)
            {
                var ch = ShortName[i];
                var lastState = state;
                state = (char.IsUpper(ch), false);

                if (i == 0 && char.IsLetter(ShortName[0]))
                {
                    state.isAccumulated = true;
                    value = accumulator(value, ch);
                    continue;
                }

                if (state.isUpper)
                {
                    //if (!lastCharacterWasUppercase || 
                    //    // Special case interface names which start with canonical 'I{UpperChar}{LowerChar}' prefix
                    //    // For instance IDefinitionSymbol should be abbreviated as IDS even though ID are two consecutive upper characters
                    //    (i == 1 && ShortName[0] == 'I' && !char.IsUpper(ShortName[2])))
                    //{
                    //    value = accumulator(value, ShortName[i]);
                    //}

                    if (!lastState.isUpper)
                    {
                        state.isAccumulated = true;
                        value = accumulator(value, ch);
                        continue;
                    }
                }
                else
                {
                    if (lastState.isUpper && !lastState.isAccumulated && char.IsLower(ch))
                    {
                        // Current char is a lowercase character after an uppercase character which was not accumulated.
                        // Assume that the prior character must have been the start of a work and accumulate it
                        value = accumulator(value, ShortName[i - 1]);
                    }
                }
            }

            return value;
        }

        private string CoerceShortName(string value)
        {
            return value ?? "";
        }

        public override string ToString()
        {
            return DisplayName;
        }

        protected override void OnDeserializedCore()
        {
            ReferenceKind = nameof(ObjectModel.ReferenceKind.Definition);
            base.OnDeserializedCore();
        }
    }

    public partial class Span
    {
        /// <summary>
        /// The absolute character offset of the end (exclusive) of the span within the document
        /// </summary>
        public int End => Start + Length;
    }

    partial class ClassificationSpan
    {
        protected override void Initialize()
        {
            DefaultClassificationColor = -1;
            base.Initialize();
        }
    }

    partial class LineSpan
    {
        private int CoerceLineIndex(int? value)
        {
            if (value == null || (value <= 0 && m_LineNumber != null))
            {
                if (m_LineNumber != null)
                {
                    // Line number is 1-based whereas this value is 0-based
                    return m_LineNumber.Value - 1;
                }
                else
                {
                    return 0;
                }
            }

            return value.Value;
        }

        private int CoerceLineNumber(int? value)
        {
            if (value == null || (value == 1 && m_LineIndex != null))
            {
                if (m_LineIndex != null)
                {
                    // Line index is 0-based whereas this value is 1-based
                    return m_LineIndex.Value + 1;
                }
                else
                {
                    return 1;
                }
            }

            return value.Value;
        }

        protected override void OnDeserializedCore()
        {
            base.OnDeserializedCore();
        }

        protected override void OnSerializingCore()
        {
            base.OnSerializingCore();
        }
    }

    partial class TextLineSpan
    {
        public int LineSpanEnd => LineSpanStart + Length;

        public string Segment => this.GetSegment();

        public void Trim()
        {
            if (string.IsNullOrWhiteSpace(LineSpanText))
            {
                LineSpanStart = 0;
                LineSpanText = string.Empty;
                Length = 0;
            }
            else
            {
                var initialLength = LineSpanText.Length;
                LineSpanText = LineSpanText.TrimStart();
                var newLength = LineSpanText.Length;
                LineSpanStart -= (initialLength - newLength);
                LineSpanText = LineSpanText.TrimEnd();
                LineSpanStart = Math.Max(LineSpanStart, 0);
                Length = Math.Min(LineSpanText.Length, Length);
            }
        }
    }

    partial class ReferenceSpan
    {
        public override string ToString()
        {
            return $"[{Start}, {End}] {Reference?.ProjectId}::{Reference?.Id} '{LineSpanText}'";
        }
    }

    partial class SymbolSpan
    {
        public ReferenceSpan CreateReference(ReferenceSymbol referenceSymbol, SymbolId relatedDefinition = default(SymbolId))
        {
            return new ReferenceSpan(this)
            {
                RelatedDefinition = relatedDefinition,
                Reference = referenceSymbol
            };
        }

        public DefinitionSpan CreateDefinition(DefinitionSymbol definition)
        {
            return new DefinitionSpan(this)
            {
                Definition = definition
            };
        }
    }

    public partial class PropertyMap : Dictionary<string, string>, IPropertyMap
    {
    }

    //partial class TextSourceSearchModel
    //{
    //    protected override void OnDeserializedCore()
    //    {
    //        base.OnDeserializedCore();
    //    }

    //    protected override void OnSerializingCore()
    //    {
    //        File.Content = 
    //        base.OnSerializingCore();
    //    }
    //}

    partial class ReferenceSearchModel
    {
        private IReadOnlyList<SymbolSpan> CoerceSpans(IReadOnlyList<SymbolSpan> value)
        {
            value = value ?? CompressedSpans?.ToList();
            this.Spans = value;
            return value;
        }

        protected override void OnSerializingCore()
        {
            if (Spans != null)
            {
                string lineSpanText = null;
                foreach (var span in Spans)
                {
                    span.LineSpanText = RemoveDuplicate(span.LineSpanText, ref lineSpanText);
                }
            }

            base.OnSerializingCore();
        }

        protected override void OnDeserializedCore()
        {
            if (Spans != null)
            {
                string lineSpanText = null;
                foreach (var span in Spans)
                {
                    span.LineSpanText = AssignDuplicate(span.LineSpanText, ref lineSpanText);
                }
            }

            base.OnDeserializedCore();
        }
    }
}