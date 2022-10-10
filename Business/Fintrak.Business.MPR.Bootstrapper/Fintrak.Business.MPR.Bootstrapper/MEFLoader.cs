using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Fintrak.Data.MPR;
using Fintrak.Shared.Common.Data;

namespace Fintrak.Business.MPR.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(BSCaptionRepository).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(DataRepositoryFactory).Assembly));
           
            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }

    }
}
