﻿using Codex.Utilities;

namespace Codex.ObjectModel.Implementation
{
    public class ClassificationListModel : SpanListModel<ClassificationSpan, ClassificationSpanListSegmentModel, ClassificationStyle, string>, IClassificationList
    {
        public ClassificationListModel()
        {
        }

        public ClassificationListModel(IReadOnlyList<ClassificationSpan> spans)
            : base(spans)
        {
        }

        public static ClassificationListModel CreateFrom(IReadOnlyList<ClassificationSpan> spans)
        {
            //if (spans is IndexableListAdapter<ClassificationSpan> list && list.Indexable is ClassificationListModel model)
            //{
            //    return model;
            //}

            return new ClassificationListModel(spans);
        }

        public override ClassificationSpanListSegmentModel CreateSegment(ListSegment<ClassificationSpan> segmentSpans)
        {
            return new ClassificationSpanListSegmentModel()
            {
                LocalSymbolGroupIds = IntegerListModel.Create(segmentSpans, span => span.LocalGroupId)
            };
        }

        public override ClassificationSpan CreateSpan(int start, int length, ClassificationStyle shared, ClassificationSpanListSegmentModel segment, int segmentOffset)
        {
            return new ClassificationSpan()
            {
                Start = start,
                Length = length,
                Classification = shared.Name,
                DefaultClassificationColor = shared.Color,
                LocalGroupId = segment.LocalSymbolGroupIds?[segmentOffset] ?? 0
            };
        }

        public override ClassificationStyle GetShared(ClassificationSpan span)
        {
            return new ClassificationStyle()
            {
                Name = span.Classification,
                Color = span.DefaultClassificationColor
            };
        }

        public override string GetSharedKey(ClassificationSpan span)
        {
            return span.Classification;
        }
    }

    public class ClassificationSpanListSegmentModel : SpanListSegmentModel
    {
        public IntegerListModel LocalSymbolGroupIds { get; set; }

        internal override void OptimizeLists(OptimizationContext context)
        {
            LocalSymbolGroupIds?.Optimize(context);

            base.OptimizeLists(context);
        }

        internal override void ExpandLists(OptimizationContext context)
        {
            LocalSymbolGroupIds?.ExpandData(context);

            base.ExpandLists(context);
        }
    }
}
