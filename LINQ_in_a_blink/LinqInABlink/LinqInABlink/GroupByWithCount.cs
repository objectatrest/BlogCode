using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInABlink
{
    public class GroupByWithCount
    {
        public class Person
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }

        public void BasicCount_PossiblyRelated()
        {
            var people = new List<Person>
            {
                new Person{ Firstname="Jim", Lastname="Bob" },
                new Person{ Firstname="Joe", Lastname="Bob" },
                new Person{ Firstname="Joe", Lastname="John" },
            };

            Console.WriteLine("Group and count");

            var groupedAndCounted = people
                                    .GroupBy(person => person.Lastname)
                                    .Select(grouping => new { Lastname = grouping.Key, Count = grouping.Count() })
                                    .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, 
                                groupedAndCounted.Select(personStats => string.Format("Lastname={0}, Count={1}", 
                                                                                        personStats.Lastname, 
                                                                                        personStats.Count)).ToArray()));
            
            Console.WriteLine("Group and count. Only include items that occur more than once.");

            var possibleRelatives = people
                                    .GroupBy(person => person.Lastname)
                                    .Select(grouping => new { Lastname = grouping.Key, Count = grouping.Count() })
                                    .Where(personStats => personStats.Count > 1)
                                    .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, 
                                possibleRelatives.Select(personStats => string.Format("Lastname={0}, Count={1}", 
                                                                                        personStats.Lastname, 
                                                                                        personStats.Count)).ToArray()));

        }

    }
}
