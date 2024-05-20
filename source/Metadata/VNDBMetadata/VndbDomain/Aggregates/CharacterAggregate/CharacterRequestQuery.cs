﻿using Newtonsoft.Json;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBMetadata.VndbDomain.Aggregates.TraitAggregate;
using VNDBMetadata.VndbDomain.Common.Filters;
using VNDBMetadata.VndbDomain.Common.Flags;
using VNDBMetadata.VndbDomain.Common.Queries;
using VNDBMetadata.VndbDomain.Common.Utilities;

namespace VNDBMetadata.VndbDomain.Aggregates.CharacterAggregate
{
    public class CharacterRequestQuery : RequestQueryBase
    {
        [JsonIgnore]
        public CharacterRequestFieldsFlags FieldsFlags;
        [JsonIgnore]
        public TraitRequestFieldsFlags TraitRequestFieldsFlags;
        [JsonIgnore]
        public CharacterRequestSortEnum Sort = CharacterRequestSortEnum.Id;

        // vns.* All visual novel fields are available here.
        // vns.release.* Object, usually null, specific release that this character appears in. All release fields are available here.

        public CharacterRequestQuery(SimpleFilterBase<Character> filter) : base(filter)
        {
            Initialize();
        }

        public CharacterRequestQuery(ComplexFilterBase<Character> filter) : base(filter)
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (CharacterRequestFieldsFlags field in Enum.GetValues(typeof(CharacterRequestFieldsFlags)))
            {
                FieldsFlags |= field;
            }

            foreach (TraitRequestFieldsFlags field in Enum.GetValues(typeof(TraitRequestFieldsFlags)))
            {
                TraitRequestFieldsFlags |= field;
            }
        }

        public override List<string> GetEnabledFields()
        {
            return EnumUtilities.GetStringRepresentations(FieldsFlags)
                .Concat(EnumUtilities.GetStringRepresentations(TraitRequestFieldsFlags, "traits."))
                .ToList();
        }

        public override string GetSortString()
        {
            if (Filters is SimpleFilterBase<Character> simpleFilter)
            {
                if (Sort == CharacterRequestSortEnum.SearchRank)
                {
                    if (simpleFilter.Name != CharacterFilterFactory.Search.FilterName)
                    {
                        return null;
                    }
                }
            }
            else if (Filters is ComplexFilterBase<Character> complexFilter)
            {
                var simplePredicates = complexFilter.Filters.OfType<SimpleFilterBase<Character>>();
                if (Sort == CharacterRequestSortEnum.SearchRank)
                {
                    var searchPredicatesCount = simplePredicates.Count(x => x.Name == CharacterFilterFactory.Search.FilterName);
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