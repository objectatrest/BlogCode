using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInABlink
{
    class Program
    {
        static void Main(string[] args)
        {
            //Group and count items in a list
            var groupingDemo = new GroupByWithCount();
            groupingDemo.BasicCount_PossiblyRelated();

            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
