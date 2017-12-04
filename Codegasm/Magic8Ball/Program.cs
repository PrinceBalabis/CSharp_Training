using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Magic8Ball
{

    //abstract class Jerry
    //{
    //    private static string name = "Jerry";
    //    public static string alias = "Barnacules";
    //    private static int age = 34;
    //}

    /// <summary>
    /// Entry point for magic 8 Ball program (By
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Preserve current console text color
            ConsoleColor oldColor = Console.ForegroundColor;

            TellPeopleWhatProgramThisIs();

            // Create a randomizer object
            Random randomObject = new Random();

            //Returns a number between 1 and 10
            //Console.WriteLine("{0}", randomObject.Next(10) + 1);

            // LOOP FOREVER!
            while (true)
            {
                string questionString = getQuestionFromUser();

                // Delay to simulate thinking about answer
                int numberOfSecondsToSleep = randomObject.Next(5) + 1;
                Console.WriteLine("Thinking about your answer, stand by...");
                Thread.Sleep(numberOfSecondsToSleep * 1000);

                if(questionString.Length == 0)
                {
                    Console.WriteLine("You need to type a question fool!");
                    continue; // Don't continue the rest of the current 'while loop iteration', restart from the beginning
                }

                // See if the user typed 'quit'as the question
                if (questionString.ToLower() == "quit")
                {
                    break; // exit out of while loop
                }

                if (questionString.ToLower() == "you suck")
                {
                    Console.WriteLine("You suck even more! Bye");
                    break;
                }

                // Get a random #
                int randomNumber = randomObject.Next(4);

                // convert the randomly generated number to a color of the value in ColorCosole and set the text color with it
                Console.ForegroundColor = (ConsoleColor)randomObject.Next(15);

                // Use random number to determine response
                switch (randomNumber)
                {
                    case 0:
                        {
                            Console.WriteLine("YES");
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("NO");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("HELL NO");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("OMG YES");
                            break;
                        }
                }
            } // End of the while loop

            // Cleaning up
            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// This will print the name of the program and who created it
        /// to the console
        /// </summary>
        static void TellPeopleWhatProgramThisIs()
        {
            // Change console text color
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("M");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("agic 8 Ball (by: Barnacules)");
        }

        /// <summary>
        /// This function will return the text the user types
        /// </summary>
        /// <returns></returns>
        static string getQuestionFromUser()
        {
            // This block of code will ask the user for a question
            // and store the question text in questionString variable
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Ask a question?: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string questionString = Console.ReadLine();

            return questionString;
        }
    }
}
