using System;
using System.Collections.Generic;

namespace Funda.Makelaar
{
    public interface IPrintToConsole : IDisposable
    {
        void PrintTopMakelaars(List<string> topMakelaars);
        void PrintTopMakelaarsWithTuin(List<string> topMakelaars);
    }
}
