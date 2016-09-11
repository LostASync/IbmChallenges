using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbmChallenges.PonderThis
{
    /// <summary>
    /// https://www.research.ibm.com/haifa/ponderthis/challenges/September2016.html
    /// </summary>
    public class _1609
    {

        /// <summary>
        /// Get the answer
        /// </summary>
        /// <returns></returns>
        public static List<List<string>> GetSequence()
        {
            var seed = new List<string> { "A", "B", "C", "D" };
            var allSecs = (from f in seed
                           from s in seed
                           from t in seed
                           from o in seed
                           select new List<string> { f, s, t, o })
                      .Distinct()
                      .ToList();

            var sequence = new List<List<string>> { seed };
            foreach (var s in allSecs)
            {
                sequence.Add(s);
                if (!isValidSequence(sequence))
                    sequence.Remove(s);
            }

            return Sort(sequence);
        }

        /// <summary>
        /// Shuffle the source
        /// </summary>
        /// <param name="source">Input sequence</param>
        /// <returns>Shutffled list</returns>
        public static List<List<string>> Sort(List<List<string>> source)
        {
            var returned = source.FirstOrDefault();
            var newList = new List<List<string>>();

            while (newList.Count != source.Count)
            {
                var sourceMax = source
                    .Except(newList)
                    .Select(x => new { x, diff = x.Zip(returned, (a, b) => a.CompareTo(b)).Sum(y => Math.Abs(y)) })
                    .OrderByDescending(x => x.diff);

                returned = sourceMax.FirstOrDefault()?.x;
                newList.Add(returned);
            }

            return newList;
        }

        /// <summary>
        /// Is the given sequence valid?
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool isValidSequence(List<List<string>> sequence)
        {
            bool isTwentyFour = sequence.Count == 24;

            bool allHasFour = sequence.All(s => s.Count == 4);

            bool allHaveUniqueChars = sequence
                .All(s => s.GroupBy(y => y).Count() == 4);

            bool isSequenceUnique = !sequence
                .Select(s => s.Aggregate((x, y) => x + y))
                .GroupBy(s => s)
                .Any(x => x.ToList().Count > 1);

            bool hasNewPositions = !sequence
                .Zip(sequence.Skip(1), (x, y) =>
                    x.Zip(y, (a, b) => a == b).Any(s => s)
                )
                .Any(s => s);

            return //isTwentyFour &&
                allHasFour &&
                allHaveUniqueChars &&
                isSequenceUnique
                //hasNewPositions
                ;
        }

        /// <summary>
        /// Number of violations
        /// </summary>
        /// <param name="sequence">Answer</param>
        /// <returns>Number of violations</returns>
        public static int GetNumberOfFaults(List<List<string>> sequence)
        {
            return sequence
                .Zip(sequence.Skip(1), (x, y) =>
                    x.Zip(y, (a, b) => a == b).Count(i => i)
                )
                .Sum(i => i);
        }
    }
}