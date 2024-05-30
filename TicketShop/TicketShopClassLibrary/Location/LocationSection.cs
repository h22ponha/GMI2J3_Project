using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketShopClassLibrary;
public class LocationSection
{
    public string SectionName { get; set; }
    public int[,] Layout { get; set; }
    public double Price { get; set; }

    public LocationSection(string sectionName, int[,] layout, double price)
    {
        SectionName = sectionName;
        Layout = layout;
        Price = price;
    }
}
