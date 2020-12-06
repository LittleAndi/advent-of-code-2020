using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
                .ToList();

            List<Group> groups = new List<Group>();
            Group group = new Group();
            groups.Add(group);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    group = new Group();
                    groups.Add(group);
                }
                else
                {
                    group.AddAnswers(line);
                }
            }

            System.Console.WriteLine($"Part one: {groups.Sum(g => g.AnyoneYesAnswerCount)}");
            System.Console.WriteLine($"Part two: {groups.Sum(g => g.EveryoneYesAnswerCount)}");
        }
    }

    public class Group
    {
        public Group()
        {
            Answers = new Dictionary<char, int>();
            Respondents = 0;
        }
        public void AddAnswers(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;
            for (int i = 0; i < input.Length; i++)
            {
                if (Answers.ContainsKey(input[i])) Answers[input[i]]++;
                else Answers.Add(input[i], 1);
            }
            Respondents++;
        }
        public Dictionary<char, int> Answers { get; set; }
        public int Respondents { get; set; }
        public int EveryoneYesAnswerCount => Answers.Where(a => a.Value.Equals(Respondents)).Count();
        public int AnyoneYesAnswerCount => Answers.Count;
    }
}
