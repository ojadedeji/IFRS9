using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class PostingDetailInfo
    {
        public PostingDetail PostingDetail { get; set; }
        public PostingDetail Parent { get; set; }
      
    }
}