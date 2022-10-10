using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fintrak.Business.Core.Contracts
{
    public class UploadItemData
    {
        public int UploadId { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
    }
}
