using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class OrderDAO
    {
        FurnitureDataContext db = new FurnitureDataContext(ConfigurationManager.ConnectionStrings["strCon"].ConnectionString);

    }
}