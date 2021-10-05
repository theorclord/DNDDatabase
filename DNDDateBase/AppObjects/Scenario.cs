using System.Collections.Generic;

namespace DNDDateBase.AppObjects
{
    /// <summary>
    /// Main container for all objects related to a scenario
    /// </summary>
    public class Scenario : DNDAppObj
    {
        public List<Character> Characters { get; set; }
        public List<City> Cities { get; set; }
        public List<Location> Locations { get; set; }
        public List<Item> Items { get; set; }
    }
}
