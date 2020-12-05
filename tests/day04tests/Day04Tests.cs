using System;
using System.Collections.Generic;
using day04;
using Shouldly;
using Xunit;

namespace day04tests
{
    public class Day02Tests
    {
        [Theory]
        [InlineData("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd", "byr:1937 iyr:2017 cid:147 hgt:183cm", "", "",
                    1937, 2017, 2020,
                    "183cm", "#fffffd", "gry",
                    "860033327",
                    147)]
        [InlineData("iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884", "hcl:#cfa07d byr:1929", "", "",
                    1929, 2013, 2023,
                    null, "#cfa07d", "amb",
                    "028048884",
                    350)]
        [InlineData("hcl:#ae17e1 iyr:2013", "eyr:2024", "ecl:brn pid:760753108 byr:1931", "hgt:179cm",
                    1931, 2013, 2024,
                    "179cm", "#ae17e1", "brn",
                    "760753108",
                    null)]
        [InlineData("hcl:#cfa07d eyr:2025 pid:166559648", "iyr:2011 ecl:brn hgt:59in", "", "",
                    null, 2011, 2025,
                    "59in", "#cfa07d", "brn",
                    "166559648",
                    null)]
        public void TestPassportInfo(
            string info1, string info2, string info3, string info4,
            int? birthYear, int? issueYear, int? expirationYear,
            string height, string hairColor, string eyeColor,
            string passportId,
            int? countryId)
        {
            var passport = new Passport(info1);
            passport.AddInfo(info2);
            passport.AddInfo(info3);
            passport.AddInfo(info4);

            passport.BirthYear.ShouldBe(birthYear);
            passport.IssueYear.ShouldBe(issueYear);
            passport.ExpirationYear.ShouldBe(expirationYear);
            passport.Height.ShouldBe(height);
            passport.HairColor.ShouldBe(hairColor);
            passport.EyeColor.ShouldBe(eyeColor);
            passport.PassportId.ShouldBe(passportId);
            passport.CountryId.ShouldBe(countryId);
        }

        [Theory]
        [InlineData("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd", "byr:1937 iyr:2017 cid:147 hgt:183cm", "", "", true)]
        [InlineData("iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884", "hcl:#cfa07d byr:1929", "", "", false)]
        [InlineData("hcl:#ae17e1 iyr:2013", "eyr:2024", "ecl:brn pid:760753108 byr:1931", "hgt:179cm", true)]
        [InlineData("hcl:#cfa07d eyr:2025 pid:166559648", "iyr:2011 ecl:brn hgt:59in", "", "", false)]
        public void TestPassportValidation(
            string info1, string info2, string info3, string info4, bool isValid)
        {
            var passport = new Passport(info1);
            passport.AddInfo(info2);
            passport.AddInfo(info3);
            passport.AddInfo(info4);

            passport.IsValid.ShouldBe(isValid);
        }

        [Theory]
        [InlineData("eyr:1972 cid:100 hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926", false)]
        [InlineData("iyr:2019 hcl:#602927 eyr:1967 hgt:170cm ecl:grn pid:012533040 byr:1946", false)]
        [InlineData("hcl:dab227 iyr:2012 ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277", false)]
        [InlineData("hgt:59cm ecl:zzz eyr:2038 hcl:74454a iyr:2023 pid:3556412378 byr:2007", false)]
        [InlineData("pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980 hcl:#623a2f", true)]
        [InlineData("eyr:2029 ecl:blu cid:129 byr:1989 iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm", true)]
        [InlineData("hcl:#888785 hgt:164cm byr:2001 iyr:2015 cid:88 pid:545766238 ecl:hzl eyr:2022", true)]
        [InlineData("iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719", true)]
        public void TestExtendedPassportValidation(string info, bool valid)
        {
            var passport = new Passport(info);
            passport.IsInformationValid.ShouldBe(valid);
        }

        [Theory]
        [InlineData("byr:2002", true)]
        [InlineData("byr:2003", false)]
        public void TestBirthYear(string input, bool valid)
        {
            var passport = new Passport(input);
            passport.IsBirthYearValid.ShouldBe(valid);
        }

        [Theory]
        [InlineData("hgt:60in", true)]
        [InlineData("hgt:190cm", true)]
        [InlineData("hgt:190in", false)]
        [InlineData("hgt:190", false)]
        public void TestHeight(string input, bool valid)
        {
            var passport = new Passport(input);
            passport.IsHeightValid.ShouldBe(valid);
        }

        [Theory]
        [InlineData("hcl:#123abc", true)]
        [InlineData("hcl:#123abz", false)]
        [InlineData("hcl:123abc", false)]
        public void TestHairColor(string input, bool valid)
        {
            var passport = new Passport(input);
            passport.IsHairColorValid.ShouldBe(valid);
        }

        [Theory]
        [InlineData("ecl:brn", true)]
        [InlineData("ecl:wat", false)]
        public void TestEyeColor(string input, bool valid)
        {
            var passport = new Passport(input);
            passport.IsEyeColorValid.ShouldBe(valid);
        }

        [Theory]
        [InlineData("pid:000000001", true)]
        [InlineData("pid:0123456789", false)]
        public void TestPasspordId(string input, bool valid)
        {
            var passport = new Passport(input);
            passport.IsPassportIdValid.ShouldBe(valid);
        }
    }
}
