using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PlantEng.Models.Category
{
    public class VideoCatInfo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Sort { get; set; }
        public bool IsDeleted { get; set; }
        public VideoCatInfo()
        {
            Sort = 999999;
            IsDeleted = false;
        }
    }
}
