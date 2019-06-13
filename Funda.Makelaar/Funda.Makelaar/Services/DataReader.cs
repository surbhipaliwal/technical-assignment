using Funda.Makelaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Funda.Makelaar
{
    public class DataReader : IDataReader
    {
        private readonly ILogger _logger;
        public DataReader(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get the list of houses registered on funda to buy
        /// </summary>
        /// <returns>Returns all the houses listedd on funda to buy</returns>
        private async Task<List<House>> FetchAllListings(string basicUrl)
        {
            var listings = new List<House>();
            Listing result = null;
            int currentPage = 1, totalPages = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    do
                    {
                        string url = $"{basicUrl}/&page={currentPage}&pagesize=25";
                        HttpResponseMessage response = await client.GetAsync(url);
                        var stream = await response.Content.ReadAsStreamAsync();

                        //Throwing an exception if the request did not go through
                        if (response.IsSuccessStatusCode == false)
                        {
                            var content = await JsonStreamDeserializer.StreamToStringAsync(stream);
                            throw new ApiException
                            {
                                StatusCode = (int)response.StatusCode,
                                Content = content
                            };
                        }

                        //Deserializing json as a stream
                        if (response.IsSuccessStatusCode)
                            result = JsonStreamDeserializer.DeserializeJsonFromStream<Listing>(stream);

                        //Adding all the objects from the current page to the list
                        listings.AddRange(result?.Objects);

                        //Fetching the total number of pages
                        totalPages = result?.Paging?.AantalPaginas ?? 0;

                        //Incrementing the page count to fetch the next page
                        currentPage++;
                    } while (totalPages >= currentPage);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                Console.ReadKey();
            }
            return listings;
        }

        /// <summary>
        /// Get the names of top makelaars
        /// </summary>
        /// <param name="number">Number of makelaars to be fetched</param>
        /// <returns></returns>
        public async Task<List<string>> GetTopMakelaarsFromList(int number, string basicUrl)
        {
            List<string> topMakelaars = new List<string>();

            //Fetching all the houses listed too buy in Amsterdam
            var houseListings = await FetchAllListings(basicUrl);

            //Grouping the list by makelaarId
            var topList = houseListings.GroupBy(x => x.MakelaarId).OrderByDescending(g => g.Count()).Take(number).ToList();

            //Getting the names of top makelaars with maximum listing
            foreach (var makelaar in topList)
                topMakelaars.Add(makelaar.First().MakelaarNaam);

            return topMakelaars;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
