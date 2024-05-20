﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNDBMetadata.VndbDomain.Common.Attributes;

namespace VNDBMetadata.VndbDomain.Aggregates.CharacterAggregate
{
    public enum CharacterSexEnum
    {
        /// <summary>
        /// Male.
        /// </summary>
        [StringRepresentation(CharacterConstants.Sex.Male)]
        Male,

        /// <summary>
        /// Female.
        /// </summary>
        [StringRepresentation(CharacterConstants.Sex.Female)]
        Female,

        /// <summary>
        /// Both.
        /// </summary>
        [StringRepresentation(CharacterConstants.Sex.Both)]
        Both,
    }

}