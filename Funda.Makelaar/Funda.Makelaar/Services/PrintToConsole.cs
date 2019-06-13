using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public class PrintToConsole : IPrintToConsole
    {
        private readonly ILogger _logger;

        public PrintToConsole(ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _logger.Dispose();
        }

        /// <summary>
        /// Method to print the top makelaars with maximum number of listings on funda
        /// </summary>
        /// <param name="topMakelaars"></param>
        public void PrintTopMakelaars(List<string> topMakelaars)
        {
            _logger.Info($"Top {topMakelaars.Count()} makelaars with maximum number of listings are :");
            foreach (var makelaar in topMakelaars)
            {
                _logger.Info(makelaar);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Method to print the top makelaars with maximum number of listings with a tuin on funda
        /// </summary>
        /// <param name="topMakelaars"></param>
        public void PrintTopMakelaarsWithTuin(List<string> topMakelaars)
        {
            _logger.Info($"Top {topMakelaars.Count()} makelaars with maximum number of listings with tuin are :");
            foreach (var makelaar in topMakelaars)
            {
                _logger.Info(makelaar);
            }
            Console.ReadKey();
        }
    }
}
