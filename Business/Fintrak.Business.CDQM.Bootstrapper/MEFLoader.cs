using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Fintrak.Data.CDQM;

namespace Fintrak.Business.CDQM.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CDQMAddressRepository).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(DataRepositoryFactory).Assembly));
           
            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }

    }
}
