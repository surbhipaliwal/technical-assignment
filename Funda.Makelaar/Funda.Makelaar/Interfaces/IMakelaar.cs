using System;

namespace Funda.Makelaar
{
    public interface IMakelaar : IDisposable
    {
        void GetTopMakelaar(int number);

        void GetTopMakelaarWithTuin(int number);
    }
}
