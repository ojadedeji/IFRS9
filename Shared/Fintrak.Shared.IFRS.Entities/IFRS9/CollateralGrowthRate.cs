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
    public partial class CollateralGrowthRate : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public int seq { get; set; }

        [DataMember]
        public string TypeCode { get; set; }

        [DataMember]
        public double Inflation { get; set; }

        [DataMember]
        public double Depreciation { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public double GrowthRate { get; set; }

        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
