using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public class Makelaar : IMakelaar
    {
        private readonly IDataReader _dataReader;
        private const string urlAmsterdamHouses = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam";
        private const string urlAmsterdamHousesWithTuin = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin";

        public Makelaar(IDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public async Task<List<string>> GetTopMakelaar(int number)
        {
            return await _dataReader.GetTopMakelaarsFromList(number, urlAmsterdamHouses);
        }

        public async Task<List<string>> GetTopMakelaarWithTuin(int number)
        {
            return await _dataReader.GetTopMakelaarsFromList(number, urlAmsterdamHousesWithTuin);
        }

        public void Dispose()
        {
            _dataReader.Dispose();
        }
    }
}
