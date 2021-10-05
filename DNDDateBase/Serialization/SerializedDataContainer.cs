﻿using DNDDateBase.AppObjects;
using System.Collections.Generic;

namespace DNDDateBase.Serialization
{
    public class SerializedDataContainer
    {
        public Scenario Scenario { get; set; }
        public List<Character> Characters { get; set; }
        public List<City> Cities { get; set; }
        public List<Location> Locations { get; set; }
        public List<Item> Items { get; set; }
    }
}
