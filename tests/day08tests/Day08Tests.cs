using System;
using Xunit;
using day08;
using Shouldly;

namespace day08tests
{
    public class Day08Tests
    {
        string[] program = new string[] {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6",
        };

        [Fact]
        public void TestAcc()
        {
            var gameConsole = new GameConsole(program);
            gameConsole.Acc(6);
            gameConsole.Accumulator.ShouldBe(6);
        }

        [Fact]
        public void TestJmp()
        {
            var gameConsole = new GameConsole(program);
            gameConsole.Jmp(3);
            gameConsole.CurrentInstruction.ShouldBe(program[3]);
        }

        [Fact]
        public void TestNop()
        {
            var gameConsole = new GameConsole(program);
            gameConsole.Nop(-99);
            gameConsole.CurrentInstruction.ShouldBe(program[1]);
        }

        [Fact]
        public void TestRunInstruction()
        {
            var gameConsole = new GameConsole(program);
            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(0);
            gameConsole.CurrentInstruction.ShouldBe("acc +1");

            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(1);
            gameConsole.CurrentInstruction.ShouldBe("jmp +4");

            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(1);
            gameConsole.CurrentInstruction.ShouldBe("acc +1");

            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(2);
            gameConsole.CurrentInstruction.ShouldBe("jmp -4");

            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(2);
            gameConsole.CurrentInstruction.ShouldBe("acc +3");

            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(5);
            gameConsole.CurrentInstruction.ShouldBe("jmp -3");

            gameConsole.RunCurrentInstruction();
            gameConsole.Accumulator.ShouldBe(5);
            gameConsole.CurrentInstruction.ShouldBe("acc +1");

            Should.Throw<Exception>(() => gameConsole.RunCurrentInstruction()).Message.ShouldBe("Program loop.");
        }
    }
}
