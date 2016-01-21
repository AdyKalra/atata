﻿using System;

namespace Atata
{
    public class FindByColumnAttribute : TermMatchFindAttribute
    {
        private const TermFormat DefaultFormat = TermFormat.Title;
        private const TermMatch DefaultMatch = TermMatch.Equals;

        private readonly Type defaultStrategy = typeof(FindByColumnHeaderStrategy);

        private bool useIndexStrategy;

        public FindByColumnAttribute()
        {
        }

        public FindByColumnAttribute(int columnIndex)
        {
            ColumnIndex = columnIndex;
            useIndexStrategy = true;
        }

        public FindByColumnAttribute(params string[] values)
            : base(values)
        {
        }

        public int ColumnIndex { get; private set; }
        public Type Strategy { get; set; }

        public override IElementFindStrategy CreateStrategy(UIComponentMetadata metadata)
        {
            if (useIndexStrategy)
            {
                return new FindByColumnIndexStrategy(ColumnIndex);
            }
            else
            {
                Type strategyType = GetStrategyType(metadata);
                return (IElementFindStrategy)Activator.CreateInstance(strategyType);
            }
        }

        // TODO: Rewiew copy/paste.
        private Type GetStrategyType(UIComponentMetadata metadata)
        {
            if (Strategy != null)
            {
                return Strategy;
            }
            else
            {
                var settingsAttribute = metadata.GetFirstOrDefaultGlobalAttribute<FindByColumnSettingsAttribute>(x => x.Strategy != null);
                return settingsAttribute != null ? settingsAttribute.Strategy : defaultStrategy;
            }
        }

        protected override TermFormat GetTermFormatFromMetadata(UIComponentMetadata metadata)
        {
            var settingsAttribute = metadata.GetFirstOrDefaultGlobalAttribute<FindByColumnSettingsAttribute>(x => x.Format != TermFormat.Inherit);
            return settingsAttribute != null ? settingsAttribute.Format : DefaultFormat;
        }

        protected override TermMatch GetTremMatchFromMetadata(UIComponentMetadata metadata)
        {
            var settingsAttribute = metadata.GetFirstOrDefaultGlobalAttribute<FindByColumnSettingsAttribute>(x => x.Match != TermMatch.Inherit);
            return settingsAttribute != null ? settingsAttribute.Match : DefaultMatch;
        }
    }
}
