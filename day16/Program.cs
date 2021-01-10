using System;
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
        }
    }

    public class TicketValidator
    {
        public bool IsTicketValid(int[] ticketValues)
        {
            return false;
        }
        public void AddRule(string field, int start1, int end1, int start2, int end2)
        {

        }
    }
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
