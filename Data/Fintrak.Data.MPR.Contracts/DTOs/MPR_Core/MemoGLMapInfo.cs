using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MemoGLMapInfo
    {
        public MemoGLMap MemoGLMap { get; set; }
        public GLDefinition GLDefinition { get; set; }
        public MemoUnits MemoUnits { get; set; }
       
    }
}