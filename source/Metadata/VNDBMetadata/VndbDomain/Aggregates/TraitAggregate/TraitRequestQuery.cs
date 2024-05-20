﻿using Newtonsoft.Json;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBMetadata.VndbDomain.Common.Filters;
using VNDBMetadata.VndbDomain.Common.Flags;
using VNDBMetadata.VndbDomain.Common.Queries;
using VNDBMetadata.VndbDomain.Common.Utilities;

namespace VNDBMetadata.VndbDomain.Aggregates.TraitAggregate
{
    public class TraitRequestQuery : RequestQueryBase
    {
        [JsonIgnore]
        public TraitRequestFieldsFlags FieldsFlags;
        [JsonIgnore]
        public TraitRequestSortEnum Sort = TraitRequestSortEnum.Id;

        public TraitRequestQuery(SimpleFilterBase<Trait> filter) : base(filter)
        {
            Initialize();
        }

        public TraitRequestQuery(ComplexFilterBase<Trait> filter) : base(filter)
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (TraitRequestFieldsFlags field in Enum.GetValues(typeof(TraitRequestFieldsFlags)))
            {
                FieldsFlags |= field;
            }
        }

        public override List<string> GetEnabledFields()
        {
            return EnumUtilities.GetStringRepresentations(FieldsFlags);
        }

        public override string GetSortString()
        {
            if (Filters is SimpleFilterBase<Trait> simpleFilter)
            {
                if (Sort == TraitRequestSortEnum.SearchRank)
                {
                    if (simpleFilter.Name != TraitFilterFactory.Search.FilterName)
                    {
                        return null;
                    }
                }
            }
            else if (Filters is ComplexFilterBase<Trait> complexFilter)
            {
                var simplePredicates = complexFilter.Filters.OfType<SimpleFilterBase<Trait>>();
                if (Sort == TraitRequestSortEnum.SearchRank)
                {
                    var searchPredicatesCount = simplePredicates.Count(x => x.Name == TraitFilterFactory.Search.FilterName);
                    if (searchPredicatesCount != 1)
                    {
                        return null;
                    }
                }
            }

            return EnumUtilities.GetEnumStringRepresentation(Sort);
        }
    }
}