﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day02
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwordDatabaseEntriesPartOne = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => new PasswordDatabaseEntryFromOldJob(l))
                .ToList();

            Console.WriteLine($"Part one valid passwords: {passwordDatabaseEntriesPartOne.Count(p => p.ValidPassword)}");

            var passwordDatabaseEntriesPartTwo = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => new PasswordDatabaseEntry(l))
                .ToList();

            Console.WriteLine($"Part two valid passwords: {passwordDatabaseEntriesPartTwo.Count(p => p.ValidPassword)}");
        }
    }

    public class PasswordDatabaseEntryFromOldJob
    {
        Regex rx = new Regex(@"(\d+)-(\d+) ([a-z]): (\w+)");
        GroupCollection matchGroups;
        public PasswordDatabaseEntryFromOldJob(string entry)
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
        public bool ValidPassword => (Password.Count(p => p == PasswordPolicyLetter) >= PasswordPolicyLetterMinOccur) && (Password.Count(p => p == PasswordPolicyLetter) <= PasswordPolicyLetterMaxOccur);
    }
    public class PasswordDatabaseEntry
    {
        Regex rx = new Regex(@"(\d+)-(\d+) ([a-z]): (\w+)");
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
        public int PasswordPolicyLetterPositionOne => int.Parse(matchGroups[1].Value);
        public int PasswordPolicyLetterPositionTwo => int.Parse(matchGroups[2].Value);
        public char PasswordPolicyLetterOne => Password[PasswordPolicyLetterPositionOne - 1];
        public char PasswordPolicyLetterTwo => Password[PasswordPolicyLetterPositionTwo - 1];
        public bool ValidPassword => (PasswordPolicyLetterOne == PasswordPolicyLetter) ^ (PasswordPolicyLetterTwo == PasswordPolicyLetter);
    }

}
