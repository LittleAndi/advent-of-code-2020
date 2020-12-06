using System;
using Xunit;
using day06;
using Shouldly;
namespace day06tests
{
    public class Day06Tests
    {
        [Theory]
        [InlineData("abc", "", "", "", 1, 3)]
        [InlineData("a", "b", "c", "", 3, 3)]
        [InlineData("ab", "ac", "", "", 2, 3)]
        [InlineData("a", "a", "a", "a", 4, 1)]
        [InlineData("b", "", "", "", 1, 1)]
        public void TestGroupAnyoneYesAnswer(string input1, string input2, string input3, string input4, int respondents, int anyoneYesAnswerCount)
        {
            var group = new Group();
            group.AddAnswers(input1);
            group.AddAnswers(input2);
            group.AddAnswers(input3);
            group.AddAnswers(input4);
            group.Respondents.ShouldBe(respondents);
            group.AnyoneYesAnswerCount.ShouldBe(anyoneYesAnswerCount);
        }

        [Theory]
        [InlineData("abc", "", "", "", 1, 3)]
        [InlineData("a", "b", "c", "", 3, 0)]
        [InlineData("ab", "ac", "", "", 2, 1)]
        [InlineData("a", "a", "a", "a", 4, 1)]
        [InlineData("b", "", "", "", 1, 1)]
        public void TestGroupEveryoneYesAnswer(string input1, string input2, string input3, string input4, int respondents, int everyoneYesAnswerCount)
        {
            var group = new Group();
            group.AddAnswers(input1);
            group.AddAnswers(input2);
            group.AddAnswers(input3);
            group.AddAnswers(input4);
            group.Respondents.ShouldBe(respondents);
            group.EveryoneYesAnswerCount.ShouldBe(everyoneYesAnswerCount);
        }
    }
}
