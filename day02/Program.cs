using System;
using System.Text.RegularExpressions;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class PasswordDatabaseEntry
    {
        Regex rx = new Regex(@"(\d)-(\d) ([a-z]): (\w+)");
        GroupCollection matchGroups;
        public PasswordDatabaseEntry(string entry)
        {
            this.PasswordPolicyAndPassword = entry;
            var matches = rx.Matches(this.PasswordPolicyAndPassword);
            matchGroups = matches[0].Groups;
        }
        public string PasswordPolicyAndPassword { get; set; }
        public string Password => matchGroups[4].Value;
        public char PasswordPolicyLetter => matchGroups[3].Value.ToCharArray()[0];
        public int PasswordPolicyLetterMinOccur => int.Parse(matchGroups[1].Value);
        public int PasswordPolicyLetterMaxOccur => int.Parse(matchGroups[2].Value);
    }

    public class PasswordPolicy
    {
        public string Letter { get; set; }
    }
}
