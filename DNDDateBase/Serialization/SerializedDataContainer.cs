using DNDDateBase.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDDateBase.Serialization
{
    [Serializable()]
    public class SerializedDataContainer
    {
        public List<Character> Characters { get; set; }
        public List<City> Cities { get; set; }
        public List<Location> Locations { get; set; }
        public List<Item> Items { get; set; }
    }
}
