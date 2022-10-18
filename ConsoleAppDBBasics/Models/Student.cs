using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDBBasics.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Course { get; set; }
        public double Rating { get; set; }
    }
}
