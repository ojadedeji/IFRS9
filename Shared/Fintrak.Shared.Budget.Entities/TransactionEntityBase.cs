using Fintrak.Shared.Common.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Fintrak.Shared.Budget.Entities
{
    public partial class TransactionEntityBase<T> : EntityBase
    {
        [DataMember]
        [Required]
        public T Month1 { get; set; }

        [DataMember]
        [Required]
        public T Month2 { get; set; }

        [DataMember]
        [Required]
        public T Month3 { get; set; }

        [DataMember]
        [Required]
        public T Month4 { get; set; }

        [DataMember]
        [Required]
        public T Month5 { get; set; }

        [DataMember]
        [Required]
        public T Month6 { get; set; }

        [DataMember]
        [Required]
        public T Month7 { get; set; }

        [DataMember]
        [Required]
        public T Month8 { get; set; }

        [DataMember]
        [Required]
        public T Month9 { get; set; }

        [DataMember]
        [Required]
        public T Month10 { get; set; }

        [DataMember]
        [Required]
        public T Month11 { get; set; }

        [DataMember]
        [Required]
        public T Month12 { get; set; }
    }
}
