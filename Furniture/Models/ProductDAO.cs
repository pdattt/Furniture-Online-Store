using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class ProductDAO
    {
        FurnitureDataContext db = new FurnitureDataContext(ConfigurationManager.ConnectionStrings["strCon"].ConnectionString);
        
        public List<Product> SelectALL()
        {
            //db.ObjectTrackingEnabled = false;

            List<Product> products = db.Products.ToList();

            return products;
        }

        public Product SelectByID(int ID)
        {
            Product product = db.Products.Where(x => x.ID == ID).SingleOrDefault();

            return product;
        }
    }
}