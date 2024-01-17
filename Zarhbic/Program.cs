using System;
using System.Collections.Generic;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Bienvenue dans le programme Zarhbic !");
        Console.WriteLine("Entrez la chaîne de calcul en utilisant la notation polonaise inversée (RPN) :");

        // Saisie de la chaîne de calcul depuis la console
        string input = Console.ReadLine();

        // Appel du service de gestion des erreurs et d'évaluation
        string resultat = GestionErreursEtEvaluation(input);

        // Affichage du résultat ou du message d'erreur
        Console.WriteLine($"Résultat : {resultat}");
    }

    static string GestionErreursEtEvaluation(string expression)
    {
        try
        {
            // Logique d'évaluation de l'expression RPN
            double resultat = EvaluerExpressionRPN(expression);
            return resultat.ToString();
        }
        catch (Exception ex)
        {
            // Gestion des erreurs
            return $"Erreur : {ex.Message}";
        }
    }

    static double EvaluerExpressionRPN(string expression)
    {
        // Utilisation d'une pile pour évaluer l'expression RPN
        Stack<double> pile = new Stack<double>();

        // Séparation de la chaîne en tokens
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double nombre))
            {
                // Si le token est un nombre, le placer sur la pile
                pile.Push(nombre);
            }
            else
            {
                // Si le token est un opérateur, effectuer l'opération correspondante
                double operand2 = pile.Pop();
                double operand1 = pile.Pop();

                switch (token)
                {
                    case "+":
                        pile.Push(operand1 + operand2);
                        break;
                    case "-":
                        pile.Push(operand1 - operand2);
                        break;
                    case "*":
                        pile.Push(operand1 * operand2);
                        break;
                    case "/":
                        pile.Push(operand1 / operand2);
                        break;
                    default:
                        throw new ArgumentException($"Opérateur non pris en charge : {token}");
                }
            }
        }

        // Le résultat final doit être le seul élément restant dans la pile
        return pile.Pop();
    }
}