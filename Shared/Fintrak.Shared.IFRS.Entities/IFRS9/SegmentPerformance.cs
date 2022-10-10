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
    public partial class SegmentPerformance : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)] public int SegmentId { get; set; }
        [DataMember] public string SegmentCode { get; set; }

        [DataMember] public string SegmentName { get; set; }
        [DataMember] public int Period { get; set; }
        [DataMember] public int Non_Performing_Count { get; set; }
        [DataMember] public int Previously_Performing_Count { get; set; }
        [DataMember] public double Estimate { get; set; }
        [DataMember] public string Param1 { get; set; }
        [DataMember] public string Param2 { get; set; }
        [DataMember] public int? Param3 { get; set; }
        [DataMember] public int? Param4 { get; set; }
        [DataMember] public DateTime? Param5 { get; set; }
        [DataMember] public DateTime? Param6 { get; set; }

        public int EntityId{
            get{
                return SegmentId;
            }
        }
    }
}
