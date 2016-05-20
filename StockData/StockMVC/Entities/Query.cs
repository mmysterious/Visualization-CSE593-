using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Entities
{
    public class Query
    {
        [Required]
        public string Code { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
