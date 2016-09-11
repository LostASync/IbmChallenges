using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbmChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = PonderThis._1609.GetSequence();

            sequence?
                .Select(x => x.Aggregate((a, b) => a + b))
                .ToList()
                .ForEach(s => Console.WriteLine(s));
            Console.WriteLine($"Number of violations: {PonderThis._1609.GetNumberOfFaults(sequence)}");

            Console.ReadKey();
        }
    }
}
