using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace Funda.Makelaar
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x => x.FromThisAssembly()
               .IncludingNonPublicTypes()
               .SelectAllClasses()
               .InNamespaces("Funda.Makelaar")
               .BindDefaultInterface());
        }
    }
}
