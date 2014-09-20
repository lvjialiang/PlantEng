using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantEng.Web.WWW.Areas.Special.Models
{
    /// <summary>
    /// 产品奖ITEM
    /// </summary>
    public class ProductPrizeItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int VoteCount { get; set; }
    }
}
