using System.Collections.Generic;

namespace Funda.Makelaar.Entities
{
    public class Listing
    {
        public List<House> Objects { get; set; }
        public Paging Paging { get; set; }
    }
}
