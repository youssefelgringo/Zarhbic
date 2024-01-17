using System;
using System.Collections.Generic;

class ZarhbicCalculator
{
    static void Main()
    {
        Console.WriteLine("Bienvenue dans le programme Zarhbic !");
        Console.WriteLine("Entrez la chaîne de calcul en utilisant la notation polonaise inversée (RPN) :");

        // Saisie de la chaîne de calcul depuis la console
        string input = Console.ReadLine();

        // Vérification si l'entrée n'est pas nulle ou vide
        if (!string.IsNullOrEmpty(input))
        {
            // Appel du service de gestion des erreurs et d'évaluation
            string resultat = GestionErreursEtEvaluation(input);

            // Affichage du résultat ou du message d'erreur
            Console.WriteLine($"Résultat : {resultat}");
        }
        else
        {
            Console.WriteLine("Erreur : La chaîne de calcul est nulle ou vide.");
        }
    }

    static string GestionErreursEtEvaluation(string expression)
    {
        try
        {
            // Vérification si l'expression n'est pas nulle ou vide
            if (!string.IsNullOrEmpty(expression))
            {
                // Logique d'évaluation de l'expression RPN
                double resultat = EvaluerExpressionRPN(expression);
                return resultat.ToString();
            }
            else
            {
                return "Erreur : L'expression de calcul est nulle ou vide.";
            }
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
                if (pile.Count >= 2)
                {
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
                            // Vérification pour éviter la division par zéro
                            if (operand2 != 0)
                            {
                                pile.Push(operand1 / operand2);
                            }
                            else
                            {
                                throw new ArgumentException("Division par zéro.");
                            }
                            break;
                        default:
                            throw new ArgumentException($"Opérateur non pris en charge : {token}");
                    }
                }
                else
                {
                    throw new ArgumentException("Nombre insuffisant d'opérandes pour l'opération.");
                }
            }
        }

        // Vérification si la pile contient un seul élément
        if (pile.Count == 1)
        {
            // Le résultat final doit être le seul élément restant dans la pile
            return pile.Pop();
        }
        else
        {
            throw new ArgumentException("Expression invalide : nombre incorrect d'opérandes.");
        }
    }
}
