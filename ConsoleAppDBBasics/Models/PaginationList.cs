using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDBBasics.Models
{
    public class PaginationList<T>
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}
