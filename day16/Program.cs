﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            Regex regexField = new Regex(@"(?<field>[a-z ]+): (?<start1>\d+)-(?<end1>\d+) or (?<start2>\d+)-(?<end2>\d+)");

            var ticketValidator = new TicketValidator();
            var line = 0;

            Match fieldMatch;
            while ((fieldMatch = regexField.Match(lines[line])).Success)
            {
                ticketValidator.AddRule(
                    fieldMatch.Groups["field"].Value,
                    int.Parse(fieldMatch.Groups["start1"].Value),
                    int.Parse(fieldMatch.Groups["end1"].Value),
                    int.Parse(fieldMatch.Groups["start2"].Value),
                    int.Parse(fieldMatch.Groups["end2"].Value));
                line++;
            }

            line += 2;

            // Read my ticket
            var myTicket = lines.Skip(line).First().Split(',').Select(c => int.Parse(c)).ToArray();

            line += 3;

            // Read nearby tickets
            var k = lines
                        .Skip(line)
                        .Where(l => !string.IsNullOrWhiteSpace(l));

            foreach (var l in k)
            {
                var t = l.Split(',').Select(c => int.Parse(c)).ToArray();
                ticketValidator.IsTicketValid(t);
            }

            System.Console.WriteLine($"Part one: Ticket scanning error rate is {ticketValidator.TicketScanningErrorRate}");

            var resolvedFields = ticketValidator.GetResolvedFields();
            long productOfDepartureFields = 1;
            resolvedFields.Where(field => field.Key.StartsWith("departure")).ToList().ForEach(field => productOfDepartureFields *= myTicket[field.Value]);

            System.Console.WriteLine($"Part two: The product of departure fields from my ticket is {productOfDepartureFields}");
        }
    }

    public class TicketValidator
    {
        Dictionary<string, Expression<Func<int, bool>>> rules = new Dictionary<string, Expression<Func<int, bool>>>();
        List<int> ticketScanningError = new List<int>();
        public int TicketScanningErrorRate => ticketScanningError.Sum();
        List<int[]> validTickets = new List<int[]>();
        public TicketValidator()
        {
        }
        public bool IsTicketValid(int[] ticketValues)
        {
            bool totalResult = true;
            for (int i = 0; i < ticketValues.Length; i++)
            {
                bool result = false;
                var t = new int[] { ticketValues[i] }.AsQueryable();
                foreach (var rule in rules)
                {
                    if (t.Where(rule.Value).Count() == 1) result = true;
                }

                if (!result)
                {
                    ticketScanningError.AddRange(t);
                    totalResult = false;
                }
            }

            if (totalResult) validTickets.Add(ticketValues);
            return totalResult;
        }
        public void AddRule(string field, int start1, int end1, int start2, int end2)
        {
            var predicate = PredicateBuilder.False<int>();
            predicate = predicate.Or(i => i >= start1 && i <= end1);
            predicate = predicate.Or(i => i >= start2 && i <= end2);
            rules.Add(field, predicate);
        }

        public bool TestRule(string field, int pos)
        {
            var rule = rules[field];
            var positionValues = validTickets.Select(t => t[pos]).AsQueryable();
            return positionValues.Where(rule).Count() == positionValues.Count();
        }

        public Dictionary<string, int[]> GetFieldMatrix()
        {
            Dictionary<string, int[]> fieldMatrix = new Dictionary<string, int[]>();
            foreach (var rule in rules)
            {
                int[] testresult = new int[rules.Count];
                for (int pos = 0; pos < validTickets[0].Count(); pos++)
                {
                    testresult[pos] = TestRule(rule.Key, pos) ? 1 : 0;
                }
                fieldMatrix.Add(rule.Key, testresult);
            }
            return fieldMatrix;
        }

        public Dictionary<string, int> GetResolvedFields()
        {
            var resolvedFields = new Dictionary<string, int>();
            var matrix = GetFieldMatrix();
            var fieldCount = matrix.First().Value.Length;

            // Loop positions, look for 1
            for (int pos = 0; pos < fieldCount; pos++)
            {
                var sumOfPositions = matrix.Select(field => field.Value[pos]).Sum();
                var field = matrix.First(field => field.Value.Sum() == (fieldCount + 1 - sumOfPositions));
                resolvedFields.Add(field.Key, pos);
            }

            return resolvedFields;
        }
    }
    // http://www.albahari.com/nutshell/predicatebuilder.aspx
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

}
