using System;
using Xunit;
using Shouldly;

namespace day02tests
{
    public class Day02Tests
    {
        [Theory]
        [InlineData("1-3 a: abcde", "abcde", 'a', 1, 3)]
        [InlineData("1-3 b: cdefg", "cdefg", 'b', 1, 3)]
        [InlineData("2-9 c: ccccccccc", "ccccccccc", 'c', 2, 9)]
        public void TestPasswordDatabaseEntry(string entry, string password, char letter, int letterMinOccur, int letterMaxOccur)
        {
            var passwordDatabaseEntry = new day02.PasswordDatabaseEntry(entry);
            passwordDatabaseEntry.Password.ShouldBe(password);
            passwordDatabaseEntry.PasswordPolicyLetter.ShouldBe(letter);
            passwordDatabaseEntry.PasswordPolicyLetterMinOccur.ShouldBe(letterMinOccur);
            passwordDatabaseEntry.PasswordPolicyLetterMaxOccur.ShouldBe(letterMaxOccur);
        }
    }
}
