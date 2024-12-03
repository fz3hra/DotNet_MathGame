// See https://aka.ms/new-console-template for more information

/*
 * math operation
 * dividend only 0 to 100
 * present with a menu to choose operation
 * 
 * You should record previous games in a List and there should be an option
 * in the menu for the user to visualize a history of previous games.
 *
 * You don't need to record results on a database. Once the program is closed the results will be deleted.
 */

// Console.WriteLine("Hello, World!");

using System;
using System.ComponentModel;
using MathMenu;

namespace MathMenu
{
    public class Game
    {
        private static readonly Random random = new Random();
        int result;

        public enum MenuChoices
        {
            [Description("Addition")]
            Addition,
            [Description("Substraction")]
            Substraction,
            [Description("Multiplication")]
            Multiplication,
            [Description("Division")]
            Division,
            
            [Description("Unknown")]
            Unknown,
        
            [Description("Exit")]
            Exit
        }

        public static MenuChoices GetUserChoice()
        {
            var input = Console.ReadLine();
            return Enum.TryParse(input, out MenuChoices choice) ? choice : MenuChoices.Unknown;
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Welcome to Math Game!\n Please Choose an option for calcutation");
            var menuItemNumber = 1;
            foreach (MenuChoices choice in Enum.GetValues(typeof(MenuChoices)))
            {
                if (choice != MenuChoices.Unknown)
                {
                    Console.WriteLine($"{choice}");
                    menuItemNumber++;
                }
                
            }
        }

        public void StartGame()
        {
            DisplayMenu();
            var choice = GetUserChoice();
            
            switch(choice)
            {
                case MenuChoices.Addition:
                case MenuChoices.Substraction:
                case MenuChoices.Division:
                case MenuChoices.Multiplication:
                    PlayGame(choice);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        
        private (int num1, int num2) GenerateNumbers(MenuChoices choice)
        {
            // int num1 = random.Next(0, 101);
            // int num2 = random.Next(1, 101); // For division, avoid 0
            // return (num1, num2);
            if (choice == MenuChoices.Division)
            {
                int num1, num2;
                do
                {
                    num1 = random.Next(0, 101);
                    num2 = random.Next(1, 101); // Avoid 0 for division
                } while (num1 % num2 != 0 || num1 < num2); // Keep generating until we get clean division
        
                return (num1, num2);
            }
            else
            {
                int num1 = random.Next(0, 101);
                int num2 = random.Next(1, 101);
                return (num1, num2);
            }
        }

        private void PlayGame(MenuChoices choice)
        {
            var (num1, num2) = GenerateNumbers(choice);
            
            Console.WriteLine($"{num1} {choice} {num2}");

            if (choice is MenuChoices.Addition)
            {
                result = num1 + num2;
            }
            else if (choice is MenuChoices.Division)
            {
                result = num1 / num2;

            }
            else if (choice is MenuChoices.Multiplication)
            {
                result = num1 * num2;
            }
            else if (choice is MenuChoices.Substraction)
            {
                result = num1 - num2;
            }
            else
            {
                Console.WriteLine("Boom");
            }
            
            // result = choice switch
            // {
            //     MenuChoices.Addition => num1 + num2,
            //     MenuChoices.Substraction => num1 - num2,
            //     MenuChoices.Multiplication => num1 * num2,
            //     MenuChoices.Division => num1 / num2,
            //     _ => 0
            // };
            
            if (int.TryParse(Console.ReadLine(), out int userAnswer))
            {
                bool isCorrect = userAnswer == result;
                Console.WriteLine($"{userAnswer} {isCorrect}");
            }
        }
    }
    
}

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartGame();
        }
    }
}