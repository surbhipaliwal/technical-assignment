using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public interface IMakelaar : IDisposable
    {
        Task<List<string>> GetTopMakelaar(int number);

        Task<List<string>> GetTopMakelaarWithTuin(int number);
    }
}
