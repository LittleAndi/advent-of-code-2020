using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
                .ToList();

            var passports = new List<Passport>();
            var passport = new Passport("");
            passports.Add(passport);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passport = new Passport(line);
                    passports.Add(passport);
                }
                else
                {
                    passport.AddInfo(line);
                }
            }

            System.Console.WriteLine($"Part one: {passports.Where(p => p.IsValid).Count()} passports passed the first validation");
            System.Console.WriteLine($"Part two: {passports.Where(p => p.IsValid && p.IsInformationValid).Count()} passports passed the second validation");
        }
    }

    public class Passport
    {
        Dictionary<string, string> passportValues = new Dictionary<string, string>();
        Dictionary<string, Regex> validationRegex = new Dictionary<string, Regex>()
        {
            { "hgt", new Regex(@"(\d+)in|(\d+)cm") },
            { "hcl", new Regex(@"^#[0-9a-f]{6}$") },
            { "ecl", new Regex(@"amb|blu|brn|gry|grn|hzl|oth") },
            { "pid", new Regex(@"^\d{9}$") }
        };

        public Passport(string passportInfo)
        {
            AddInfo(passportInfo);
        }

        public void AddInfo(string passportInfo)
        {
            if (string.IsNullOrWhiteSpace(passportInfo)) return;

            var keyValues = passportInfo.Split(' ');
            foreach (var kv in keyValues)
            {
                var key = kv.Split(':')[0];
                var value = kv.Split(':')[1];
                passportValues.Add(key, value);
            }
        }

        // byr
        public int? BirthYear => passportValues.ContainsKey("byr") ? int.Parse(passportValues["byr"]) : (int?)null;
        public bool IsBirthYearValid => (BirthYear != null) && (BirthYear >= 1920) && (BirthYear <= 2002);
        // iyr
        public int? IssueYear => passportValues.ContainsKey("iyr") ? int.Parse(passportValues["iyr"]) : (int?)null;
        public bool IsIssueYearValid => (IssueYear != null) && (IssueYear >= 2010) && (IssueYear <= 2020);
        // eyr
        public int? ExpirationYear => passportValues.ContainsKey("eyr") ? int.Parse(passportValues["eyr"]) : (int?)null;
        public bool IsExpirationYearValid => (ExpirationYear != null) && (ExpirationYear >= 2020) && (ExpirationYear <= 2030);
        // hgt
        public string Height => passportValues.GetValueOrDefault("hgt", (string)null);
        public bool IsHeightValid
        {
            get
            {
                var match = validationRegex["hgt"].Match(Height);
                if (!match.Success) return false;
                if (match.Groups[1].Success)
                {
                    var height = int.Parse(match.Groups[1].Value);
                    return (height >= 59) && (height <= 76);
                }
                if (match.Groups[2].Success)
                {
                    var height = int.Parse(match.Groups[2].Value);
                    return (height >= 150) && (height <= 193);
                }
                return false;
            }
        }
        // hcl
        public string HairColor => passportValues.GetValueOrDefault("hcl", (string)null);
        public bool IsHairColorValid => validationRegex["hcl"].Match(HairColor).Success;
        // ecl
        public string EyeColor => passportValues.GetValueOrDefault("ecl", (string)null);
        public bool IsEyeColorValid => validationRegex["ecl"].Match(EyeColor).Success;
        // pid
        public string PassportId => passportValues.GetValueOrDefault("pid", (string)null);
        public bool IsPassportIdValid => validationRegex["pid"].Match(PassportId).Success;
        // cid
        public int? CountryId => passportValues.ContainsKey("cid") ? int.Parse(passportValues["cid"]) : (int?)null;
        public bool IsValid => BirthYear != null &&
                                IssueYear != null &&
                                ExpirationYear != null &&
                                Height != null &&
                                HairColor != null &&
                                EyeColor != null &&
                                PassportId != null;
        public bool IsInformationValid => IsBirthYearValid &&
                                          IsIssueYearValid &&
                                          IsExpirationYearValid &&
                                          IsHeightValid &&
                                          IsHairColorValid &&
                                          IsEyeColorValid &&
                                          IsPassportIdValid;
    }
}
