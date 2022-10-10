using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class RatingMappingInfo
    {
        public RatingMapping RatingMapping { get; set; }
        public ExternalRating ExternalRating { get; set; }
        public InternalRatingBased InternalRatingBased { get; set; }
    }
}