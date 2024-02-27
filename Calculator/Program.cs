using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Vítejte v kalkulačce!");

        while (true)
        {
            Console.Write("Zadejte výraz (nebo 'exit' pro ukončení): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                break;
            }

            try
            {
                double result = EvaluateExpression(input);
                Console.WriteLine($"Výsledek: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba: {ex.Message}");
            }
        }
    }

    static double EvaluateExpression(string expression)
    {
        // Regulární výraz pro extrakci čísel a operandu
        string pattern = @"([-+]?\d*\.\d+|[-+]?\d+)([-+*/])([-+]?\d*\.\d+|[-+]?\d+)";

        // Seznam čísel a operandu z výrazu
        Match match = Regex.Match(expression, pattern);

        if (!match.Success)
        {
            throw new InvalidOperationException("Chybný výraz: neplatný formát.");
        }

        // Extrahování čísel a operandu
        double operand1 = double.Parse(match.Groups[1].Value);
        char operation = match.Groups[2].Value[0];
        double operand2 = double.Parse(match.Groups[3].Value);

        // Vykonání operace
        switch (operation)
        {
            case '+':
                return operand1 + operand2;
            case '-':
                return operand1 - operand2;
            case '*':
                return operand1 * operand2;
            case '/':
                if (operand2 == 0)
                {
                    throw new InvalidOperationException("Chybný výraz: nelze dělit nulou.");
                }
                return operand1 / operand2;
            default:
                throw new InvalidOperationException($"Neznámý operand: {operation}");
        }
    }
}
