using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Shared.Common
{
    public interface IDirtyCapable
    {
        bool IsDirty { get; }

        bool IsAnythingDirty();

        List<IDirtyCapable> GetDirtyObjects();

        void CleanAll();
    }
}
