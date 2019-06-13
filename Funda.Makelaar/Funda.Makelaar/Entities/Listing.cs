using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funda.Makelaar.Entities
{
    public class Listing
    {
        public List<House> Objects { get; set; }
        public Paging Paging { get; set; }
    }
}
