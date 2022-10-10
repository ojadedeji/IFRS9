using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IfrsStocksMapping : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int IfrsStocksMappingId { get; set; }

        [DataMember]
        public string Unqouted_stock_code { get; set; }

        [DataMember]
        public string StockDescription { get; set; }

        [DataMember]
        public string Qouted_stock_code { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return IfrsStocksMappingId;
            }
        }
    }
}
