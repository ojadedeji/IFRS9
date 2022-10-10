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
    public partial class IfrsPdTermStructure : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Rating { get; set; }

        [DataMember]
        public double _1 { get; set; }


        [DataMember]
        public double _2 { get; set; }


    [DataMember]
    public double _3 { get; set; }


    [DataMember]
    public double _4 { get; set; }


    [DataMember]
    public double _5 { get; set; }

    [DataMember]
    public double _6 { get; set; }


    [DataMember]
    public double _7 { get; set; }


    [DataMember]
    public double _8 { get; set; }


    [DataMember]
    public double _9 { get; set; }


    [DataMember]
    public double _10 { get; set; }


    [DataMember]
    public double _11 { get; set; }


    [DataMember]
    public double _12 { get; set; }


    [DataMember]
    public double _13 { get; set; }


    [DataMember]
    public double _14 { get; set; }


    [DataMember]
    public double _15 { get; set; }


    //[DataMember]
    //public string CompanyCode { get; set; }


    [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
