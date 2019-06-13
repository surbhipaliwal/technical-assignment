using Ninject;
using System.Reflection;

namespace Funda.Makelaar
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Resolving the dependency
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var _makelaar = kernel.Get<IMakelaar>();

            //Fetching and printing the top 10 makelaars with maximum number of listings of house to buy in amsterdam
            _makelaar.GetTopMakelaar(10);

            //Fetching and printing the top 10 makelaars with maximum number of listings of house with tuin to buy in amsterdam 
            _makelaar.GetTopMakelaarWithTuin(10);
        }
    }
}
