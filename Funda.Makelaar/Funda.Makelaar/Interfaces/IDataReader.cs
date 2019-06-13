using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public interface IDataReader : IDisposable
    {
        Task<List<string>> GetTopMakelaarsFromList(int number, string basicUrl);
    }
}
