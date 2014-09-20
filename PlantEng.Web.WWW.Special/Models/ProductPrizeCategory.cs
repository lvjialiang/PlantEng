using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantEng.Web.WWW.Areas.Special.Models
{
    public class ProductPrizeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductPrizeItem> Items { get; set; }
    }
}
