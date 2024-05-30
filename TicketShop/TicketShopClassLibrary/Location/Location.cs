using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class Location : ILocation
{
    public string LocationName { get ; set; }
    public int Capacity { get; set; }
    public string Address { get; set; }
    public List<LocationSection> Sections { get; set; }

    // Skapar en 2-dimensionell array med antal rader och kolumner som argument
    public Location(string name, string address, List<LocationSection> sections)
    {
        LocationName = name;
        Address = address;
        Sections = sections;
    }
    // TODO: Byt ut int array mot en dictionary
}
