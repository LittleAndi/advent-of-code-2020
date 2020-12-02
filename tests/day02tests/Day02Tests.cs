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
        [InlineData("10-16 c: ccccqccchcccccjlc", "ccccqccchcccccjlc", 'c', 10, 16)]
        public void TestPasswordDatabaseEntryFromOldJob(string entry, string password, char letter, int letterMinOccur, int letterMaxOccur)
        {
            var passwordDatabaseEntry = new day02.PasswordDatabaseEntryFromOldJob(entry);
            passwordDatabaseEntry.Password.ShouldBe(password);
            passwordDatabaseEntry.PasswordPolicyLetter.ShouldBe(letter);
            passwordDatabaseEntry.PasswordPolicyLetterMinOccur.ShouldBe(letterMinOccur);
            passwordDatabaseEntry.PasswordPolicyLetterMaxOccur.ShouldBe(letterMaxOccur);
        }

        [Theory]
        [InlineData("1-3 a: abcde", true)]
        [InlineData("1-3 b: cdefg", false)]
        [InlineData("2-9 c: ccccccccc", true)]
        [InlineData("10-16 c: ccccqccchcccccjlc", true)]
        public void TestValidPassword(string entry, bool validPassword)
        {
            var passwordDatabaseEntry = new day02.PasswordDatabaseEntryFromOldJob(entry);
            passwordDatabaseEntry.ValidPassword.ShouldBe(validPassword);
        }

        [Theory]
        [InlineData("1-3 a: abcde", "abcde", 'a', 1, 3, 'a', 'c')]
        [InlineData("1-3 b: cdefg", "cdefg", 'b', 1, 3, 'c', 'e')]
        [InlineData("2-9 c: ccccccccc", "ccccccccc", 'c', 2, 9, 'c', 'c')]
        [InlineData("10-16 c: ccccqccchcccccjlc", "ccccqccchcccccjlc", 'c', 10, 16, 'c', 'l')]
        public void TestPasswordDatabaseEntry(string entry, string password, char letter, int letterPositionOne, int letterPositionTwo, char letterOne, char letterTwo)
        {
            var passwordDatabaseEntry = new day02.PasswordDatabaseEntry(entry);
            passwordDatabaseEntry.Password.ShouldBe(password);
            passwordDatabaseEntry.PasswordPolicyLetter.ShouldBe(letter);
            passwordDatabaseEntry.PasswordPolicyLetterPositionOne.ShouldBe(letterPositionOne);
            passwordDatabaseEntry.PasswordPolicyLetterPositionTwo.ShouldBe(letterPositionTwo);
            passwordDatabaseEntry.PasswordPolicyLetterOne.ShouldBe(letterOne);
            passwordDatabaseEntry.PasswordPolicyLetterTwo.ShouldBe(letterTwo);
        }
    }
}
