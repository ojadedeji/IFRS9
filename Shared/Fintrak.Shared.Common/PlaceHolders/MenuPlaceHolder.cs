using System;
using System.Linq;

namespace Fintrak.Shared.Common.PlaceHolders
{
    public class MenuPlaceHolder
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Action { get; set; }
        public string ActionUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Parent { get; set; }
        public string ParentModule { get; set; }
        public int Position { get; set; }
    }
}
