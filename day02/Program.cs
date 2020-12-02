using System;

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
        public PasswordDatabaseEntry(string entry)
        {
            this.PasswordPolicyAndPassword = entry;
        }
        public string PasswordPolicyAndPassword { get; set; }
        public string Password => "";
        public char PasswordPolicyLetter => char.MinValue;
        public int PasswordPolicyLetterMinOccur = int.MinValue;
        public int PasswordPolicyLetterMaxOccur = int.MaxValue;
    }

    public class PasswordPolicy
    {
        public string Letter { get; set; }
    }
}
