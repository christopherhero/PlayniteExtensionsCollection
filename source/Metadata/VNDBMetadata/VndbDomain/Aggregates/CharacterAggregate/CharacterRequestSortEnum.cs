﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBMetadata.VndbDomain.Common.Attributes;

namespace VNDBMetadata.VndbDomain.Aggregates.CharacterAggregate
{
    public enum CharacterRequestSortEnum
    {
        [StringRepresentation(CharacterConstants.RequestSort.Id)]
        Id,
        [StringRepresentation(CharacterConstants.RequestSort.Name)]
        Name,
        [StringRepresentation(CharacterConstants.RequestSort.SearchRank)]
        SearchRank
    }
}