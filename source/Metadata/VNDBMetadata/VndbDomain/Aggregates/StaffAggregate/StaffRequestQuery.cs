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

namespace VNDBMetadata.VndbDomain.Aggregates.StaffAggregate
{
    public class StaffRequestQuery : RequestQueryBase
    {
        [JsonIgnore]
        public StaffRequestFieldsFlags FieldsFlags;
        [JsonIgnore]
        public ExtLinksFieldsFlags ExtLinksFieldsFlags;
        [JsonIgnore]
        public AliasesFieldsFlags AliasesFieldsFlags;
        [JsonIgnore]
        public StaffRequestSortEnum Sort = StaffRequestSortEnum.Id;

        public StaffRequestQuery(SimpleFilterBase<Staff> filter) : base(filter)
        {
            Initialize();
        }

        public StaffRequestQuery(ComplexFilterBase<Staff> filter) : base(filter)
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (StaffRequestFieldsFlags field in Enum.GetValues(typeof(StaffRequestFieldsFlags)))
            {
                FieldsFlags |= field;
            }

            foreach (ExtLinksFieldsFlags field in Enum.GetValues(typeof(ExtLinksFieldsFlags)))
            {
                ExtLinksFieldsFlags |= field;
            }

            foreach (AliasesFieldsFlags field in Enum.GetValues(typeof(AliasesFieldsFlags)))
            {
                AliasesFieldsFlags |= field;
            }
        }

        public override List<string> GetEnabledFields()
        {
            return EnumUtilities.GetStringRepresentations(FieldsFlags)
                .Concat(EnumUtilities.GetStringRepresentations(ExtLinksFieldsFlags))
                .Concat(EnumUtilities.GetStringRepresentations(AliasesFieldsFlags))
                .ToList();
        }

        public override string GetSortString()
        {
            if (Filters is SimpleFilterBase<Staff> simpleFilter)
            {
                if (Sort == StaffRequestSortEnum.SearchRank)
                {
                    if (simpleFilter.Name != StaffFilterFactory.Search.FilterName)
                    {
                        return null;
                    }
                }
            }
            else if (Filters is ComplexFilterBase<Staff> complexFilter)
            {
                var simplePredicates = complexFilter.Filters.OfType<SimpleFilterBase<Staff>>();
                if (Sort == StaffRequestSortEnum.SearchRank)
                {
                    var searchPredicatesCount = simplePredicates.Count(x => x.Name == StaffFilterFactory.Search.FilterName);
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