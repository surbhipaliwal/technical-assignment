using Ninject;
using System;
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
            var makelaar = kernel.Get<IMakelaar>();
            var printToConsole = kernel.Get<IPrintToConsole>();
            var logger = kernel.Get<ILogger>();

            logger.Info("Fetching the results....");

            //Fetching and printing the top 10 makelaars with maximum number of listings of house to buy in amsterdam
            var top10Makelaars = makelaar.GetTopMakelaar(10);
            printToConsole.PrintTopMakelaars(top10Makelaars.Result);


            //Fetching and printing the top 10 makelaars with maximum number of listings of house with tuin to buy in amsterdam 
            var top10MakelaarsWithTuin = makelaar.GetTopMakelaarWithTuin(10);
            printToConsole.PrintTopMakelaarsWithTuin(top10MakelaarsWithTuin.Result);

            Console.ReadKey();

        }
    }
}
