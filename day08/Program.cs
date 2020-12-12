using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();

            PartOne(lines);
            PartTwo(lines);
        }

        private static void PartTwo(string[] lines)
        {
            {
                // Try nop => jmp
                for (int i = 0; i < lines.Length; i++)
                {
                    var testlines = lines.Select(a => (string)a.Clone()).ToArray();
                    if (testlines[i].Substring(0, 3).Equals("nop"))
                    {
                        testlines[i] = testlines[i].Replace("nop", "jmp");
                        var gameConsole = new GameConsole(testlines);
                        bool result;
                        if (result = gameConsole.BootUp())
                        {
                            System.Console.WriteLine($"Part two: Boot (replaced nop with jmp) { (result ? "success" : "failed") }, current stage of accumulator: {gameConsole.Accumulator}");
                        }
                    }
                }
            }

            {
                // Try jmp => nop
                for (int i = 0; i < lines.Length; i++)
                {
                    var testlines = lines.Select(a => (string)a.Clone()).ToArray();
                    if (testlines[i].Substring(0, 3).Equals("jmp"))
                    {
                        testlines[i] = testlines[i].Replace("jmp", "nop");
                        var gameConsole = new GameConsole(testlines);
                        bool result;
                        if (result = gameConsole.BootUp())
                        {
                            System.Console.WriteLine($"Part two: Boot (replaced jmp with nop) { (result ? "success" : "failed") }, current stage of accumulator: {gameConsole.Accumulator}");
                        }
                    }
                }
            }

        }

        private static void PartOne(string[] lines)
        {
            var gameConsole = new GameConsole(lines);
            var result = gameConsole.BootUp();
            System.Console.WriteLine($"Part one: Boot { (result ? "success" : "failed") }, current stage of accumulator: {gameConsole.Accumulator}");
        }
    }

    public class GameConsole
    {
        Regex regex = new Regex(@"(?<instruction>nop|acc|jmp) (?<value>(?:\+|\-)\d+)");
        private readonly string[] program;
        private int instructionPointer = 0;
        private int accumulator = 0;
        private HashSet<int> instructionPointerMemory = new HashSet<int>();
        public string CurrentInstruction => program[instructionPointer];
        public int Accumulator => accumulator;
        private const string programLoopMessage = "Program loop.";
        public GameConsole(string[] program)
        {
            this.program = program;
        }

        public void Acc(int value)
        {
            if (!this.instructionPointerMemory.Add(this.instructionPointer)) throw new Exception(programLoopMessage);
            this.accumulator += value;
            this.instructionPointer++;
        }

        public void Jmp(int value)
        {
            if (!this.instructionPointerMemory.Add(this.instructionPointer)) throw new Exception(programLoopMessage);
            this.instructionPointer += value;
        }

        public void Nop(int value)
        {
            if (!this.instructionPointerMemory.Add(this.instructionPointer)) throw new Exception(programLoopMessage);
            this.instructionPointer++;
        }

        public void RunCurrentInstruction()
        {
            var instructionInfo = regex.Match(this.CurrentInstruction);
            var instruction = instructionInfo.Groups[1].Value;
            var value = int.Parse(instructionInfo.Groups[2].Value);
            switch (instruction)
            {
                case "acc":
                    Acc(value);
                    break;
                case "jmp":
                    Jmp(value);
                    break;
                case "nop":
                    Nop(value);
                    break;
            }
        }

        public bool BootUp()
        {
            do
            {
                try
                {
                    RunCurrentInstruction();
                }
                catch (System.Exception)
                {
                    return false;
                }
            } while (this.instructionPointer < program.Length);
            return true;
        }
    }
}
