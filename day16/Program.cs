using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            Regex regexField = new Regex(@"(?<field>[a-z ]+): (?<start1>\d+)-(?<end1>\d+) or (?<start2>\d+)-(?<end2>\d+)");

            var ticketValidator = new TicketValidator();
            var line = 0;

            Match fieldMatch;
            while ((fieldMatch = regexField.Match(lines[line])).Success)
            {
                ticketValidator.AddRule(
                    fieldMatch.Groups["field"].Value,
                    int.Parse(fieldMatch.Groups["start1"].Value),
                    int.Parse(fieldMatch.Groups["end1"].Value),
                    int.Parse(fieldMatch.Groups["start2"].Value),
                    int.Parse(fieldMatch.Groups["end2"].Value));
                line++;
            }
        }
    }

    public class TicketValidator
    {
        public bool IsTicketValid(int[] ticketValues)
        {
            return false;
        }
        public void AddRule(string field, int start1, int end1, int start2, int end2)
        {

        }
    }
}
