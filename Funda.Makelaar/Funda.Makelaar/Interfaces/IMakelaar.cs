using Funda.Makelaar.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public interface IMakelaar : IDisposable
    {
        void GetTopMakelaar(int number);

        void GetTopMakelaarWithTuin(int number);
    }
}
