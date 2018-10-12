using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zur.Common
{
     public class Sourcedestinationpoints
    {
        public string source { get; set; }
        public string Destnation { get; set; }
        public IDictionary<string, string> SOurcePoints { get; set; }
        public IDictionary<string,string> DestinationPoints { get; set; }

    }
}
