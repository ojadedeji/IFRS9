using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class IfrsStocksMappingData : DataContractBase
    {

        [DataMember]
        public int IfrsStocksMappingId { get; set; }

        [DataMember]
        public string Unqouted_stock_code { get; set; }

        [DataMember]
        public string Unqouted_stock_Name { get; set; }

        [DataMember]
        public string StockDescription { get; set; }

        [DataMember]
        public string Qouted_stock_code { get; set; }

        [DataMember]
        public string Qouted_stock_Name { get; set; }
       

        [DataMember]
        public bool Active { get; set; }
    }
}
