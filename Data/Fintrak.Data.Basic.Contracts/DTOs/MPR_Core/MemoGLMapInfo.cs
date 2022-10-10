using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class MemoGLMapInfo
    {
        public MemoGLMap MemoGLMap { get; set; }
        public GLDefinition GLDefinition { get; set; }
        public MemoUnits MemoUnits { get; set; }
       
    }
}