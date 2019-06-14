using System;

namespace Funda.Makelaar
{
    public interface ILogger : IDisposable
    {
        void Error(Exception ex);

        void Info(object msg);
        void Error(object msg);
    }
}
