﻿using Newtonsoft.Json;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBMetadata.VndbDomain.Common.Filters;
using VNDBMetadata.VndbDomain.Common.Queries;
using VNDBMetadata.VndbDomain.Common.Utilities;

namespace VNDBMetadata.VndbDomain.Aggregates.ProducerAggregate
{
    public class ProducerRequestQuery : RequestQueryBase
    {
        [JsonIgnore]
        public ProducerRequestFieldsFlags Fields;
        [JsonIgnore]
        public ProducerRequestSortEnum Sort = ProducerRequestSortEnum.Id;

        public ProducerRequestQuery(SimpleFilterBase<Producer> filter) : base(filter)
        {
            Initialize();
        }

        public ProducerRequestQuery(ComplexFilterBase<Producer> filter) : base(filter)
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (ProducerRequestFieldsFlags field in Enum.GetValues(typeof(ProducerRequestFieldsFlags)))
            {
                Fields |= field;
            }
        }

        public override List<string> GetEnabledFields()
        {
            return EnumUtilities.GetStringRepresentations(Fields);
        }

        public override string GetSortString()
        {
            if (Filters is SimpleFilterBase<Producer> simpleFilter)
            {
                if (Sort == ProducerRequestSortEnum.SearchRank)
                {
                    if (simpleFilter.Name != ProducerFilterFactory.Search.FilterName)
                    {
                        return null;
                    }
                }
            }
            else if (Filters is ComplexFilterBase<Producer> complexFilter)
            {
                var simplePredicates = complexFilter.Filters.OfType<SimpleFilterBase<Producer>>();
                if (Sort == ProducerRequestSortEnum.SearchRank)
                {
                    var searchPredicatesCount = simplePredicates.Count(x => x.Name == ProducerFilterFactory.Search.FilterName);
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