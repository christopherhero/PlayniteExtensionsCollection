﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBMetadata.VndbDomain.Common.Attributes;

namespace VNDBMetadata.VndbDomain.Aggregates.ReleaseAggregate
{
    public enum ReleaseTypeEnum
    {
        /// <summary>
        /// Trial.
        /// </summary>
        [StringRepresentation(ReleaseConstants.ReleaseType.Trial)]
        Trial,

        /// <summary>
        /// Partial.
        /// </summary>
        [StringRepresentation(ReleaseConstants.ReleaseType.Partial)]
        Partial,

        /// <summary>
        /// Complete.
        /// </summary>
        [StringRepresentation(ReleaseConstants.ReleaseType.Complete)]
        Complete
    }
}