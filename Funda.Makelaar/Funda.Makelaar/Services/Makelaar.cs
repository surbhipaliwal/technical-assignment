using Funda.Makelaar.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public class Makelaar : IMakelaar
    {
        private readonly IDataReader _dataReader;
        private readonly IPrintToConsole _printToConsole;
        private const string urlAmsterdamHouses = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam";
        private const string urlAmsterdamHousesWithTuin = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin";

        public Makelaar(IDataReader dataReader
                       ,IPrintToConsole printToConsole)
        {
            _dataReader = dataReader;
            _printToConsole = printToConsole;
        }

        public async void GetTopMakelaar(int number)
        {
            var topMakelaars = await _dataReader.GetTopMakelaarsFromList(number, urlAmsterdamHouses);
            _printToConsole.PrintTopMakelaars(topMakelaars);
        }

        public async void GetTopMakelaarWithTuin(int number)
        {
            var topMakelaarsWithTuin = await _dataReader.GetTopMakelaarsFromList(number, urlAmsterdamHousesWithTuin);
            _printToConsole.PrintTopMakelaarsWithTuin(topMakelaarsWithTuin);
        }

        public void Dispose()
        {
            _dataReader.Dispose();
            _printToConsole.Dispose();
        }
    }
}
