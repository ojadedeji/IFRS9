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
    public partial class IfrsStocksPrimaryData : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int IfrsStocksPrimaryDataId { get; set; }

        [DataMember]
        public string Sector_code { get; set; }

        [DataMember]
        public string Company_name { get; set; }

        [DataMember]
        public double Current_stock_price { get; set; }

        [DataMember]
        public int Share_volume { get; set; }

        [DataMember]
        public double EPS { get; set; }
             

        [DataMember]
        public double Book_value { get; set; }

        [DataMember]
        public double Cash_flow { get; set; }


        [DataMember]
        public double Sales { get; set; }

        [DataMember]
        public string Stock_code { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return IfrsStocksPrimaryDataId;
            }
        }
    }
}
