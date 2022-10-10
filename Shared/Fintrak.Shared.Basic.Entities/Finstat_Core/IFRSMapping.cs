//using Fintrak.Shared.Common.Contracts;
//using Fintrak.Shared.Common.Core;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace Fintrak.Shared.Basic.Entities
//{
//    public partial class IFRSMapping : EntityBase, IIdentifiableEntity
//    {
//        [DataMember]
//        [Browsable (false)]
//        public int MappingId { get; set; }

//        [DataMember]
//        [Required]
//        public string GLCode { get; set; }

//        [DataMember]
//        [Required]
//        public string GLDescription { get; set; }

//         [DataMember]
//        public string GLParentCode { get; set; }

//         [DataMember]   
//         public string MainCaption { get; set; }

//         [DataMember]
//         public string SubCaption { get; set; }

//         [DataMember]
//         public string SubCaption1 { get; set; }

//         [DataMember]
//         public string SubCaption2 { get; set; }

//         [DataMember]
//         public string SubCaption3 { get; set; }

//         [DataMember]
//         public string SubCaption4 { get; set; }

//         [DataMember]
//        [Required]
//         public string CompanyCode { get; set; }

//         [DataMember]
//         public int SubPosition { get; set; }
//        public int EntityId
//        {
//            get
//            {
//                return MappingId;
//            }
//        }
//    }
//}
