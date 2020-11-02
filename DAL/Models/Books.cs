using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public DateTime Year { get; set; }
        public int Pages { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }

    }
}
