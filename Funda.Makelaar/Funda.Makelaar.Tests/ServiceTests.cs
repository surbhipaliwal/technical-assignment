using Ninject;
using System;
using Xunit;


namespace Funda.Makelaar.Tests
{
    public class ServiceTests : IDisposable
    {
        public StandardKernel Kernel { get; }
        public readonly IDataReader _dataReader;
        public ServiceTests()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new Bindings());
            Kernel.Rebind<ILogger>().To<Logger>();
            _dataReader = Kernel.Get<IDataReader>();
        }

        [Fact]
        public async void GetObjects_WithWrongUri_ShouldThrowInvalidOperationException()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(() => _dataReader.GetTopMakelaarsFromList(10, ""));
        }

        public void Dispose()
        {
            _dataReader.Dispose();
        }
    }
}
